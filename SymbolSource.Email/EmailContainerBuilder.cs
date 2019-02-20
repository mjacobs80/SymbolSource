using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SymbolSource.Contract.Email;

namespace SymbolSource.Email
{
    public class EmailContainerBuilder
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<EmailConfiguration>()
                .As<IEmailConfiguration>();

            builder.RegisterType<EmailService>()
                .As<IEmailService>();
        }
    }
}
