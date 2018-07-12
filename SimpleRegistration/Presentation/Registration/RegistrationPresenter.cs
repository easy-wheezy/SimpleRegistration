using SimpleRegistration.Core.Dto.Account;
using System.Text;

namespace SimpleRegistration.Presentation.Registration
{
    public class RegistrationPresenter
    {
        public RegistrationResponseViewModel Handle(RegistrationResponse responseMessage)
        {
            if (responseMessage.Success)
            {
                return new RegistrationResponseViewModel(true, "Registration successful!");
            }            

            return new RegistrationResponseViewModel(false, "Registration unsuccessful!", responseMessage.Errors);
        }
    }
}
