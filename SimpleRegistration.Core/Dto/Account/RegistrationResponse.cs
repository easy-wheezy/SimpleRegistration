using SimpleRegistration.Core.Contracts.Base;
using System.Collections.Generic;

namespace SimpleRegistration.Core.Dto.Account
{
    public class RegistrationResponse : ResponseMessage
    {
        public List<string> Errors { get; private set; }
        public RegistrationResponse(bool success, List<string> errors, string message = null) : base(success, message)
        {
            Errors = errors;
        }
    }
}
