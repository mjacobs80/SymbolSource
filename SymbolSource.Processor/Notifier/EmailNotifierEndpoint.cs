using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SymbolSource.Contract;
using System.Diagnostics;
using SymbolSource.Contract.Email;

namespace SymbolSource.Processor.Notifier
{
    public class EmailNotifierEndpoint : INotifierEndpoint
    {

        private readonly IEmailService email;

        public EmailNotifierEndpoint(IEmailService email)
        {
            this.email = email;
        }

        private Task Send(UserInfo userInfo, string action, string format, params object[] args)
        {
            email.Send(userInfo, action, format, args);
            return Task.Delay(0);
        }

        public async Task Starting(UserInfo userInfo, PackageName packageName)
        {
            await Send(userInfo, "Starting", "Thanks for submitting {0} - I just started processing", packageName);
        }

        public async Task Damaged(UserInfo userInfo, PackageName packageName)
        {
            await Send(userInfo, "Damaged", "I can't open {0}, if that's the right name of the package - @SymbolSource please help!", packageName);
        }

        public async Task Indexed(UserInfo userInfo, PackageName packageName)
        {
            await Send(userInfo, "Indexed", "Good news - I just finished processing {0}", packageName);
        }

        public async Task Deleted(UserInfo userInfo, PackageName packageName)
        {
            await Send(userInfo, "Deleted", "Good news - I just finished deleting {0}", packageName);
        }

        public async Task PartiallyIndexed(UserInfo userInfo, PackageName packageName)
        {
            await Send(userInfo, "Partially Indexed", "Something went wrong while I was indexing {0} - please review.", packageName);
        }

        public async Task PartiallyDeleted(UserInfo userInfo, PackageName packageName)
        {
            await Send(userInfo, "Partially Deleted", "Something went wrong while I was deleting {0} - please review.", packageName);
        }
    }
}
