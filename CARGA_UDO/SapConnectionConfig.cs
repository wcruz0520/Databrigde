using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CARGA_UDO
{
    public class SapConnectionProfile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public string Server { get; set; }
        public string LicenseServer { get; set; }
        public bool UseTrusted { get; set; }
        public string SLDServer { get; set; }
        public string CompanyDb { get; set; }
        public string DbServerType { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public string SapUser { get; set; }
        public string SapPassword { get; set; }

        public string DisplayName => string.IsNullOrWhiteSpace(Name) ? $"{Server} - {CompanyDb}" : Name;

        public bool IsConfigured()
        {
            if (Version <= 0
                || string.IsNullOrWhiteSpace(Server)
                || string.IsNullOrWhiteSpace(CompanyDb)
                || string.IsNullOrWhiteSpace(DbServerType)
                || string.IsNullOrWhiteSpace(SapUser))
            {
                return false;
            }

            if (Version < 10)
            {
                return !string.IsNullOrWhiteSpace(LicenseServer)
                    && (UseTrusted || !string.IsNullOrWhiteSpace(DbUser));
            }

            return !string.IsNullOrWhiteSpace(SLDServer);
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }

    public static class SapConnectionConfig
    {
        public const string ServerKey = "SapServer";
        public const string Version = "Version";
        public const string LicenseServerKey = "SapLicenseServer";
        public const string UseTrusted = "UseTrusted";
        public const string SLDServer = "SLDServer";
        public const string CompanyDbKey = "SapCompanyDb";
        public const string DbServerTypeKey = "SapDbServerType";
        public const string DbUserKey = "SapDbUser";
        public const string DbPasswordKey = "SapDbPassword";
        public const string SapUserKey = "SapUser";
        public const string SapPasswordKey = "SapPassword";
        public const string ActiveConnectionIdKey = "SapActiveConnectionId";
        private const string ConnectionIdsKey = "SapConnectionIds";
        private const string ProfilePrefix = "SapConnection.";

        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }

        public static List<SapConnectionProfile> GetProfiles()
        {
            List<SapConnectionProfile> profiles = GetProfileIds()
                .Select(GetProfile)
                .Where(profile => profile != null)
                .ToList();

            if (profiles.Count == 0 && HasLegacyConfiguration())
            {
                SapConnectionProfile legacyProfile = GetLegacyProfile();
                profiles.Add(legacyProfile);
                SaveProfiles(profiles, legacyProfile.Id);
            }

            return profiles;
        }

        public static SapConnectionProfile GetActiveProfile()
        {
            List<SapConnectionProfile> profiles = GetProfiles();
            string activeId = GetValue(ActiveConnectionIdKey);
            return profiles.FirstOrDefault(profile => profile.Id == activeId) ?? profiles.FirstOrDefault();
        }

        public static void SetActiveProfile(string id)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Set(config, ActiveConnectionIdKey, id);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static bool IsConfigured()
        {
            return GetActiveProfile()?.IsConfigured() == true;
        }

        public static SAPbobsCOM.BoDataServerTypes GetDbServerType()
        {
            return GetDbServerType(GetActiveProfile());
        }

        public static SAPbobsCOM.BoDataServerTypes GetDbServerType(SapConnectionProfile profile)
        {
            string value = profile?.DbServerType ?? string.Empty;
            if (Enum.TryParse(value, out SAPbobsCOM.BoDataServerTypes type))
                return type;

            return SAPbobsCOM.BoDataServerTypes.dst_MSSQL2019;
        }

        public static void ApplyToCompany(SAPbobsCOM.Company company)
        {
            ApplyToCompany(company, GetActiveProfile());
        }

        public static void ApplyToCompany(SAPbobsCOM.Company company, SapConnectionProfile profile)
        {
            if (profile == null)
                return;

            company.DbServerType = GetDbServerType(profile);
            company.UseTrusted = profile.UseTrusted;
            company.CompanyDB = profile.CompanyDb;
            company.UserName = profile.SapUser;
            company.Password = profile.SapPassword;
            company.Server = profile.Server;

            if (profile.Version < 10)
            {
                company.LicenseServer = profile.LicenseServer;

                if (!profile.UseTrusted)
                {
                    company.DbUserName = profile.DbUser;
                    company.DbPassword = profile.DbPassword;
                }
            }
            else if (!string.IsNullOrWhiteSpace(profile.SLDServer))
            {
                company.SLDServer = profile.SLDServer;
            }
        }

        public static void SaveProfiles(IEnumerable<SapConnectionProfile> profiles, string activeId)
        {
            List<SapConnectionProfile> list = profiles.Where(profile => profile != null).ToList();
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            foreach (string id in GetProfileIds())
                RemoveProfileSettings(config, id);

            Set(config, ConnectionIdsKey, string.Join("|", list.Select(profile => profile.Id)));
            Set(config, ActiveConnectionIdKey, activeId ?? list.FirstOrDefault()?.Id ?? string.Empty);

            foreach (SapConnectionProfile profile in list)
                SaveProfile(config, profile);

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void Save(string server, int version, string licenseServer, bool usetrusted, string sldserver, string companyDb, string dbServerType,
            string dbUser, string dbPassword, string sapUser, string sapPassword)
        {
            SapConnectionProfile active = GetActiveProfile() ?? CreateNewProfile("Conexión SAP");
            active.Server = server;
            active.LicenseServer = licenseServer;
            active.CompanyDb = companyDb;
            active.DbServerType = dbServerType;
            active.DbUser = dbUser;
            active.DbPassword = dbPassword;
            active.SapUser = sapUser;
            active.SapPassword = sapPassword;
            active.Version = version;
            active.UseTrusted = usetrusted;
            active.SLDServer = sldserver;
            List<SapConnectionProfile> profiles = GetProfiles();
            int index = profiles.FindIndex(profile => profile.Id == active.Id);
            if (index >= 0)
                profiles[index] = active;
            else
                profiles.Add(active);
            SaveProfiles(profiles, active.Id);
        }

        public static SapConnectionProfile CreateNewProfile(string name)
        {
            return new SapConnectionProfile { Id = Guid.NewGuid().ToString("N"), Name = name };
        }

        private static IEnumerable<string> GetProfileIds()
        {
            return GetValue(ConnectionIdsKey)
                .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(id => id.Trim())
                .Where(id => id.Length > 0);
        }

        private static SapConnectionProfile GetProfile(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            return new SapConnectionProfile
            {
                Id = id,
                Name = GetValue(ProfileKey(id, "Name")),
                Server = GetValue(ProfileKey(id, ServerKey)),
                Version = GetIntValue(ProfileKey(id, Version), 9),
                LicenseServer = GetValue(ProfileKey(id, LicenseServerKey)),
                UseTrusted = GetBoolValue(ProfileKey(id, UseTrusted)),
                SLDServer = GetValue(ProfileKey(id, SLDServer)),
                CompanyDb = GetValue(ProfileKey(id, CompanyDbKey)),
                DbServerType = GetValue(ProfileKey(id, DbServerTypeKey)),
                DbUser = GetValue(ProfileKey(id, DbUserKey)),
                DbPassword = GetValue(ProfileKey(id, DbPasswordKey)),
                SapUser = GetValue(ProfileKey(id, SapUserKey)),
                SapPassword = GetValue(ProfileKey(id, SapPasswordKey))
            };
        }

        private static void SaveProfile(Configuration config, SapConnectionProfile profile)
        {
            Set(config, ProfileKey(profile.Id, "Name"), profile.Name);
            Set(config, ProfileKey(profile.Id, ServerKey), profile.Server);
            Set(config, ProfileKey(profile.Id, Version), profile.Version.ToString());
            Set(config, ProfileKey(profile.Id, LicenseServerKey), profile.LicenseServer);
            Set(config, ProfileKey(profile.Id, UseTrusted), profile.UseTrusted.ToString());
            Set(config, ProfileKey(profile.Id, SLDServer), profile.SLDServer);
            Set(config, ProfileKey(profile.Id, CompanyDbKey), profile.CompanyDb);
            Set(config, ProfileKey(profile.Id, DbServerTypeKey), profile.DbServerType);
            Set(config, ProfileKey(profile.Id, DbUserKey), profile.DbUser);
            Set(config, ProfileKey(profile.Id, DbPasswordKey), profile.DbPassword);
            Set(config, ProfileKey(profile.Id, SapUserKey), profile.SapUser);
            Set(config, ProfileKey(profile.Id, SapPasswordKey), profile.SapPassword);
        }

        private static void RemoveProfileSettings(Configuration config, string id)
        {
            foreach (string key in new[] { "Name", ServerKey, Version, LicenseServerKey, UseTrusted, SLDServer, CompanyDbKey, DbServerTypeKey, DbUserKey, DbPasswordKey, SapUserKey, SapPasswordKey })
                config.AppSettings.Settings.Remove(ProfileKey(id, key));
        }

        private static string ProfileKey(string id, string key)
        {
            return ProfilePrefix + id + "." + key;
        }

        private static bool HasLegacyConfiguration()
        {
            return !string.IsNullOrWhiteSpace(GetValue(ServerKey))
                || !string.IsNullOrWhiteSpace(GetValue(CompanyDbKey));
        }

        private static SapConnectionProfile GetLegacyProfile()
        {
            return new SapConnectionProfile
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = "Conexión SAP",
                Server = GetValue(ServerKey),
                Version = GetIntValue(Version, 9),
                UseTrusted = GetBoolValue(UseTrusted),
                SLDServer = GetValue(SLDServer),
                LicenseServer = GetValue(LicenseServerKey),
                CompanyDb = GetValue(CompanyDbKey),
                DbServerType = GetValue(DbServerTypeKey),
                DbUser = GetValue(DbUserKey),
                DbPassword = GetValue(DbPasswordKey),
                SapUser = GetValue(SapUserKey),
                SapPassword = GetValue(SapPasswordKey)
            };
        }

        private static int GetIntValue(string key, int defaultValue)
        {
            return int.TryParse(GetValue(key), out int value) ? value : defaultValue;
        }

        private static bool GetBoolValue(string key)
        {
            return bool.TryParse(GetValue(key), out bool value) && value;
        }

        private static void Set(Configuration config, string key, string value)
        {
            if (config.AppSettings.Settings[key] == null)
                config.AppSettings.Settings.Add(key, value ?? string.Empty);
            else
                config.AppSettings.Settings[key].Value = value ?? string.Empty;
        }
    }
}
