using System;

namespace SymbolSource.Contract.Email
{
    public class NullEmailService : IEmailService
    {
        public void Send(UserInfo userInfo, string action, string format, params object[] args)
        {
            
        }

    }
}