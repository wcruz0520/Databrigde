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
        public string Server { get; set; }
        public string LicenseServer { get; set; }
        public string SldServer { get; set; }
        public int SapVersion { get; set; }
        public bool UseTrusted { get; set; }
        public string CompanyDb { get; set; }
        public string DbServerType { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public string SapUser { get; set; }
        public string SapPassword { get; set; }

        public string DisplayName => string.IsNullOrWhiteSpace(Name) ? $"{Server} - {CompanyDb}" : Name;

        public bool IsConfigured()
        {
            return !string.IsNullOrWhiteSpace(Server)
                && HasVersionServerConfigured()
                && !string.IsNullOrWhiteSpace(CompanyDb)
                && !string.IsNullOrWhiteSpace(DbServerType)
                && (UseTrusted || SapVersion >= 10 || !string.IsNullOrWhiteSpace(DbUser))
                && !string.IsNullOrWhiteSpace(SapUser);
        }

        public bool IsSap10OrNewer()
        {
            return SapVersion >= 10;
        }

        private bool HasVersionServerConfigured()
        {
            return IsSap10OrNewer()
                ? !string.IsNullOrWhiteSpace(Server)
                : !string.IsNullOrWhiteSpace(Server) && !string.IsNullOrWhiteSpace(LicenseServer);
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }

    public static class SapConnectionConfig
    {
        public const string ServerKey = "SapServer";
        public const string LicenseServerKey = "SapLicenseServer";
        public const string CompanyDbKey = "SapCompanyDb";
        public const string SldServerKey = "SapSldServer";
        public const string SapVersionKey = "SapVersion";
        public const string UseTrustedKey = "SapUseTrusted";
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

        public static int GetSapVersion(SapConnectionProfile profile)
        {
            return profile?.SapVersion > 0 ? profile.SapVersion : 9;
        }

        public static bool GetUseTrusted(SapConnectionProfile profile)
        {
            return profile?.UseTrusted == true;
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
            company.UseTrusted = GetUseTrusted(profile);
            company.CompanyDB = profile.CompanyDb;
            company.UserName = profile.SapUser;
            company.Password = profile.SapPassword;
            company.Server = profile.Server;

            if (GetSapVersion(profile) < 10)
            {
                company.LicenseServer = profile.LicenseServer;
                if (!company.UseTrusted)
                {
                    company.DbUserName = profile.DbUser;
                    company.DbPassword = profile.DbPassword;
                }
            }
            else if (!string.IsNullOrWhiteSpace(profile.SldServer))
            {
                company.SLDServer = profile.SldServer;
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

        public static void Save(string server, string licenseServer, string sldServer, int sapVersion, bool useTrusted, string companyDb, string dbServerType,
            string dbUser, string dbPassword, string sapUser, string sapPassword)
        {
            SapConnectionProfile active = GetActiveProfile() ?? CreateNewProfile("Conexión SAP");
            active.Server = server;
            active.LicenseServer = licenseServer;
            active.SldServer = sldServer;
            active.SapVersion = sapVersion;
            active.UseTrusted = useTrusted;
            active.CompanyDb = companyDb;
            active.DbServerType = dbServerType;
            active.DbUser = dbUser;
            active.DbPassword = dbPassword;
            active.SapUser = sapUser;
            active.SapPassword = sapPassword;
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
            return new SapConnectionProfile { Id = Guid.NewGuid().ToString("N"), Name = name, SapVersion = 9 };
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
                LicenseServer = GetValue(ProfileKey(id, LicenseServerKey)),
                SldServer = GetValue(ProfileKey(id, SldServerKey)),
                SapVersion = ParseSapVersion(GetValue(ProfileKey(id, SapVersionKey))),
                UseTrusted = ParseBool(GetValue(ProfileKey(id, UseTrustedKey))),
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
            Set(config, ProfileKey(profile.Id, LicenseServerKey), profile.LicenseServer);
            Set(config, ProfileKey(profile.Id, SldServerKey), profile.SldServer);
            Set(config, ProfileKey(profile.Id, SapVersionKey), GetSapVersion(profile).ToString());
            Set(config, ProfileKey(profile.Id, UseTrustedKey), profile.UseTrusted.ToString());
            Set(config, ProfileKey(profile.Id, CompanyDbKey), profile.CompanyDb);
            Set(config, ProfileKey(profile.Id, DbServerTypeKey), profile.DbServerType);
            Set(config, ProfileKey(profile.Id, DbUserKey), profile.DbUser);
            Set(config, ProfileKey(profile.Id, DbPasswordKey), profile.DbPassword);
            Set(config, ProfileKey(profile.Id, SapUserKey), profile.SapUser);
            Set(config, ProfileKey(profile.Id, SapPasswordKey), profile.SapPassword);
        }

        private static void RemoveProfileSettings(Configuration config, string id)
        {
            foreach (string key in new[] { "Name", ServerKey, LicenseServerKey, SldServerKey, SapVersionKey, UseTrustedKey, CompanyDbKey, DbServerTypeKey, DbUserKey, DbPasswordKey, SapUserKey, SapPasswordKey })
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
                LicenseServer = GetValue(LicenseServerKey),
                SldServer = GetValue(SldServerKey),
                SapVersion = ParseSapVersion(GetValue(SapVersionKey)),
                UseTrusted = ParseBool(GetValue(UseTrustedKey)),
                CompanyDb = GetValue(CompanyDbKey),
                DbServerType = GetValue(DbServerTypeKey),
                DbUser = GetValue(DbUserKey),
                DbPassword = GetValue(DbPasswordKey),
                SapUser = GetValue(SapUserKey),
                SapPassword = GetValue(SapPasswordKey)
            };
        }

        private static int ParseSapVersion(string value)
        {
            return int.TryParse(value, out int version) && version > 0 ? version : 9;
        }

        private static bool ParseBool(string value)
        {
            return bool.TryParse(value, out bool result) && result;
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
