using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleRegistration.Core.Contracts.Account;
using SimpleRegistration.Core.Dto.Account;
using SimpleRegistration.Core.Entities;
using SimpleRegistration.Core.Interactors;
using SimpleRegistration.Presentation.Registration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleRegistration.Test
{
    [TestClass]
    public class Validation
    {
        RegistrationRequest request;

        [TestInitialize]
        public void SetUp()
        {
            request = new RegistrationRequest("a@example.com", "Password");        
        }

        [TestMethod]
        public void EmailAvailable ()
        {            
            var AccountMock = new Mock<IAccountRepository>();
            AccountMock.Setup(m => m.CheckEmailAvailable(It.IsAny<string>())).Returns(true);
            AccountMock.Setup(m => m.ValidateAccount(It.IsAny<User>())).Returns(new List<string>());

            RegistrationInteractor interactor = new RegistrationInteractor(AccountMock.Object);

            interactor.Handle(request);
            AccountMock.Verify(m => m.CreateAcount(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }
        [TestMethod]
        public void EmailTaken()
        {
            var AccountMock = new Mock<IAccountRepository>();
            AccountMock.Setup(m => m.CheckEmailAvailable(It.IsAny<string>())).Returns(false);
            AccountMock.Setup(m => m.ValidateAccount(It.IsAny<User>())).Returns(new List<string>() { "Error" });
            RegistrationInteractor interactor = new RegistrationInteractor(AccountMock.Object);

            interactor.Handle(request);
            AccountMock.Verify(m => m.CreateAcount(It.IsAny<User>(), It.IsAny<string>()), Times.Never());
        }
        [TestMethod]
        public void EmailRequired()
        {
            RegistrationRequestViewModel model = new RegistrationRequestViewModel
            {
                Password = "asdfqaws1Q",
                Email = null
            };
            Assert.IsTrue(ValidateModel(model).Count == 1);
        }
        [TestMethod]
        public void PasswordRequired()
        {
            RegistrationRequestViewModel model = new RegistrationRequestViewModel
            {
                Password = null,
                Email = "R@gmail.com"
            };
            Assert.IsTrue(ValidateModel(model).Count == 1);
        }
        [TestMethod]
        public void InvalidEmail1()
        {
            RegistrationRequestViewModel model = new RegistrationRequestViewModel
            {
                Password = "asdfqaws1Q",
                Email = "Rgmailcom"
            };
            Assert.IsTrue(ValidateModel(model).Count == 1);
        }
        [TestMethod]
        public void InvalidEmail2()
        {
            RegistrationRequestViewModel model = new RegistrationRequestViewModel
            {
                Password = "asdfqaws1Q",
                Email = "Rgmail.com"
            };
            Assert.IsTrue(ValidateModel(model).Count == 1);
        }
        [TestMethod]
        public void InvalidEmail3()
        {
            RegistrationRequestViewModel model = new RegistrationRequestViewModel
            {
                Password = "asdfqaws1Q",
                Email = "(){}_23"
            };
            Assert.IsTrue(ValidateModel(model).Count == 1);
        }
        [TestMethod]
        public void InvalidPassword_no_number()
        {
            RegistrationRequestViewModel model = new RegistrationRequestViewModel
            {
                Password = "asdfqawsQ",
                Email = "r@gmail.com"
            };
            Assert.IsTrue(ValidateModel(model).Count == 1);
        }
        [TestMethod]
        public void InvalidPassword_no_upper()
        {
            RegistrationRequestViewModel model = new RegistrationRequestViewModel
            {
                Password = "asdfqaws1",
                Email = "r@gmail.com"
            };
            Assert.IsTrue(ValidateModel(model).Count == 1);
        }
        [TestMethod]
        public void InvalidPassword_no_lower()
        {
            RegistrationRequestViewModel model = new RegistrationRequestViewModel
            {
                Password = "QAWSEDREW1",
                Email = "r@gmail.com"
            };
            Assert.IsTrue(ValidateModel(model).Count == 1);
        }
        [TestMethod]
        public void InvalidPassword_short()
        {
            RegistrationRequestViewModel model = new RegistrationRequestViewModel
            {
                Password = "12aSa",
                Email = "r@gmail.com"
            };
            Assert.IsTrue(ValidateModel(model).Count == 1);
        }
        [TestMethod]
        public void InvalidPassword_long()
        {
            RegistrationRequestViewModel model = new RegistrationRequestViewModel
            {
                Password = "12345678901234567As01",
                Email = "r@gmail.com"
            };
            Assert.IsTrue(ValidateModel(model).Count == 1);
        }
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
