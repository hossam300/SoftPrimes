
namespace IHelperServices.Models
{
    public class ConnectionStrings
    {
        public string MainConnectionString { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string System { get; set; }
        public string Microsoft { get; set; }
    }

    public class Logging
    {
        public bool IncludeScopes { get; set; }
        public LogLevel LogLevel { get; set; }
    }

    public class SeedData
    {
        public bool SeedDataOnStart { get; set; }
        public string RelativeDirectory { get; set; }
    }

    public class SwaggerToTypeScriptClientGeneratorSettings
    {
        public string SourceDocumentAbsoluteUrl { get; set; }
        public string OutputDocumentRelativePath { get; set; }
    }

    public class FileSettings
    {
        public string RelativeDirectory { get; set; }
    }

    public class EmailSetting
    {
        public string SMTPServer { get; set; }
        public string AuthEmailFrom { get; set; }
        public string AuthDomain { get; set; }
        public string EmailFrom { get; set; }
        public int EmailPort { get; set; }
        public string EmailPassword { get; set; }
        public bool EnableSSL { get; set; }
        public string SenderAPIKey { get; set; }
        public string x_uqu_auth { get; set; }
        public string EmailPostActivate { get; set; }
        public string EmailXmlPath { get; set; }
        public string TransNumber { get; set; }
        public string NotificationXsltPath { get; set; }
        public string SendEmailDelegationWithURL { get; set; }
    }
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
        public SeedData SeedData { get; set; }
        public SwaggerToTypeScriptClientGeneratorSettings SwaggerToTypeScriptClientGeneratorSettings { get; set; }
        public FileSettings FileSettings { get; set; }
        public EmailSetting EmailSetting { get; set; }
    }
}