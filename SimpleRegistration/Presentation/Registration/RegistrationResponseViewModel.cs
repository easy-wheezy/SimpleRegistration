using System.Collections.Generic;

namespace SimpleRegistration.Presentation.Registration
{
    public class RegistrationResponseViewModel
    {
        public bool Success { get; private set; }
        public string ResultMessage { get; private set; }
        public List<string> Errors { get; private set; }

        public RegistrationResponseViewModel(bool success, string resultMessage, List<string> errors = null)
        {
            Success = success;
            ResultMessage = resultMessage;
            Errors = errors;
        }
    }
}
