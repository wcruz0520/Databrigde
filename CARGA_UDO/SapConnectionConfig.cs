using System;
using System.Configuration;

namespace CARGA_UDO
{
    public static class SapConnectionConfig
    {
        public const string ServerKey = "SapServer";
        public const string LicenseServerKey = "SapLicenseServer";
        public const string CompanyDbKey = "SapCompanyDb";
        public const string DbServerTypeKey = "SapDbServerType";
        public const string DbUserKey = "SapDbUser";
        public const string DbPasswordKey = "SapDbPassword";
        public const string SapUserKey = "SapUser";
        public const string SapPasswordKey = "SapPassword";

        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }

        public static bool IsConfigured()
        {
            return !string.IsNullOrWhiteSpace(GetValue(ServerKey))
                && !string.IsNullOrWhiteSpace(GetValue(LicenseServerKey))
                && !string.IsNullOrWhiteSpace(GetValue(CompanyDbKey))
                && !string.IsNullOrWhiteSpace(GetValue(DbServerTypeKey))
                && !string.IsNullOrWhiteSpace(GetValue(DbUserKey))
                && !string.IsNullOrWhiteSpace(GetValue(SapUserKey));
        }

        public static SAPbobsCOM.BoDataServerTypes GetDbServerType()
        {
            string value = GetValue(DbServerTypeKey);
            if (Enum.TryParse(value, out SAPbobsCOM.BoDataServerTypes type))
                return type;

            return SAPbobsCOM.BoDataServerTypes.dst_MSSQL2019;
        }

        public static void ApplyToCompany(SAPbobsCOM.Company company)
        {
            company.Server = GetValue(ServerKey);
            company.LicenseServer = GetValue(LicenseServerKey);
            company.CompanyDB = GetValue(CompanyDbKey);
            company.DbServerType = GetDbServerType();
            company.DbUserName = GetValue(DbUserKey);
            company.DbPassword = GetValue(DbPasswordKey);
            company.UserName = GetValue(SapUserKey);
            company.Password = GetValue(SapPasswordKey);
            company.language = SAPbobsCOM.BoSuppLangs.ln_Spanish_La;
        }

        public static void Save(string server, string licenseServer, string companyDb, string dbServerType,
            string dbUser, string dbPassword, string sapUser, string sapPassword)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Set(config, ServerKey, server);
            Set(config, LicenseServerKey, licenseServer);
            Set(config, CompanyDbKey, companyDb);
            Set(config, DbServerTypeKey, dbServerType);
            Set(config, DbUserKey, dbUser);
            Set(config, DbPasswordKey, dbPassword);
            Set(config, SapUserKey, sapUser);
            Set(config, SapPasswordKey, sapPassword);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
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
