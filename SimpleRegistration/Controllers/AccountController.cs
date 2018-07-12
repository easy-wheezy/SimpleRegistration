using Microsoft.AspNetCore.Mvc;
using SimpleRegistration.Core.Contracts.Account;
using SimpleRegistration.Core.Dto.Account;
using SimpleRegistration.Core.Interactors;
using SimpleRegistration.Presentation.Registration;

namespace SimpleRegistration.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterAcount(RegistrationRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            RegistrationInteractor registrationRequestInteractor = new RegistrationInteractor(_accountRepository);
            var requestMessage = new RegistrationRequest(model.Email, model.Password);
            var responseMessage = registrationRequestInteractor.Handle(requestMessage);

            var presenter = new RegistrationPresenter();

            var viewModel = presenter.Handle(responseMessage);


            return PartialView("RegistrationNotification", viewModel);
        }
    }
}