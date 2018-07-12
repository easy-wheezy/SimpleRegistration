using SimpleRegistration.Core.Contracts.Base;

namespace SimpleRegistration.Core.Dto.Account
{
    public class RegistrationRequest : IRequest<RegistrationResponse>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public RegistrationRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
