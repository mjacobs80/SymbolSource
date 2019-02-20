using System;

namespace SymbolSource.Contract.Email
{
    public interface IEmailService
    {
        void Send(UserInfo userInfo, string action, string format, params object[] args);
    }
}