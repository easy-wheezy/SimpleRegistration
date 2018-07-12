using SimpleRegistration.Core.Contracts.Account;
using SimpleRegistration.Core.Contracts.Base;
using SimpleRegistration.Core.Dto.Account;
using SimpleRegistration.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleRegistration.Core.Interactors
{
    public class RegistrationInteractor : IRequestHandler<RegistrationRequest, RegistrationResponse>
    {
        private readonly IAccountRepository _accountRepository;
        public RegistrationInteractor(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public RegistrationResponse Handle(RegistrationRequest request)
        {
            List<string> errors = new List<string>();

            var user = new User()
            {
                Email = request.Email,
                CreatedDate = DateTimeOffset.Now
                
            };

            errors.AddRange(_accountRepository.ValidateAccount(user));

            bool success = false;
            if (!errors.Any())
                success = _accountRepository.CreateAcount(user, _accountRepository.HashPassword(request.Password));

            if (!success && !errors.Any()) errors.Add($"Failed to create acount");

            return new RegistrationResponse(!errors.Any(), errors);
        }
    }
}
