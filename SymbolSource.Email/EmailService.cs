using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using SymbolSource.Contract;
using SymbolSource.Contract.Email;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace SymbolSource.Email
{
    public class EmailService : IEmailService, IDisposable
    {
        private readonly IEmailConfiguration configuration;
        private readonly SmtpClient smtpClient;

        public EmailService(IEmailConfiguration configuration)
        {
            this.configuration = configuration;
            smtpClient = new SmtpClient();
        }


        public void Dispose()
        {
            if (!(smtpClient == null) && smtpClient.IsConnected)
            {
                smtpClient.Disconnect(true);
                smtpClient.Dispose();
            }
        }

        private void CatchAndTrack(string message, Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Trace.TraceWarning("{0}:\n{1}", message, ex);
            }
        }

        public void Send(UserInfo userInfo, string action, string format, params object[] args)
        {
            if (userInfo.UserHandle == null || !EmailValidator.IsValidEmailAddress(userInfo.UserHandle))
                return;

            CatchAndTrack("Failed to send email",
                async () =>
                {
                    smtpClient.Connect(configuration.SmtpServer, configuration.SmtpPort, configuration.UseSSL);

                    if (!string.IsNullOrEmpty(configuration.Username))
                        smtpClient.Authenticate(configuration.Username, configuration.Password);

                    if (smtpClient.IsConnected)
                    {
                        var message = new MimeMessage();
                        message.From.Add(new MailboxAddress(configuration.FromName, configuration.FromAddress));
                        message.To.Add(new MailboxAddress(userInfo.UserHandle));
                        message.Subject = string.Format("Symbols update - {0}", action);

                        message.Body = new TextPart("plain")
                        {
                            Text = string.Format(format, args)
                        };

                        await smtpClient.SendAsync(message);

                    }

                    smtpClient.Disconnect(true);
                });
        }
    }
}