using System;
using SymbolSource.Contract;

namespace SymbolSource.Email
{
    public interface IEmailConfiguration
    {
        string SmtpServer { get; }
        int SmtpPort { get; }
        bool UseSSL { get; }
        string Username { get; }
        string Password { get; }
        string FromAddress { get; }
        string FromName { get; }
    }

    public class EmailConfiguration : IEmailConfiguration
    {
        private readonly IConfigurationService configuration;
        private readonly IInstanceConfiguration instanceConfiguration;

        public EmailConfiguration(
            IConfigurationService configuration,
            IInstanceConfiguration instanceConfiguration)
        {
            this.configuration = configuration;
            this.instanceConfiguration = instanceConfiguration;
        }

        public string SmtpServer
        {
            get { return configuration["Email.SmtpServer"]; }
        }

        public int SmtpPort
        {
            get {
                int smtpPort = 25;
                int.TryParse(configuration["Email.SmtpPort"], out smtpPort);
                return smtpPort;
            }
        }

        public bool UseSSL
        {
            get {
                bool useSSL = false;
                bool.TryParse(configuration["Email.UseSsl"], out useSSL);
                return useSSL;
            } 
        }

        public string Username
        {
            get { return configuration["Email.Username"]; }
        }

        public string Password
        {
            get { return configuration["Email.Password"]; }
        }

        public string FromAddress
        {
            get {
                string fromAddress = configuration["Email.FromAddress"] ?? "noreplay@me.com";
                return fromAddress;
            }
        }
        public string FromName
        {
            get { return instanceConfiguration.InstanceName; }
        }

    }
}
