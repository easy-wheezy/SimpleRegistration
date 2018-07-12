using SimpleRegistration.Core.Contracts.Base;
using SimpleRegistration.Core.Entities;
using System.Collections.Generic;

namespace SimpleRegistration.Core.Contracts.Account
{
    public interface IAccountRepository 
    {
        bool CreateAcount(User user, string password);
        List<string> ValidateAccount(User user);
        bool CheckEmailAvailable(string email);
        string HashPassword(string password);
    }
}
