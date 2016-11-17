
using Registration.Contracts.Registration;
using Registration.Testing.DataGenerators;

namespace Registration.Testing
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Contracts;
    using Data.Builders;
    using System;
    [TestClass]
    public class RegistrationTests
    {
        [TestMethod]
        public void UserCanRegisterSucccessfullyTest()
        {
            var provider = ProviderFactory.CreateRegistrationProvider();
            var request = new RegistrationBuilder().JaneDoe().BuildRequest();

            var response = provider.Register(request);
            Assert.AreEqual(ResultCode.Success, response.ResultCode, "John Doe did not successfully register for event");
        }

        [TestMethod]
        public void UserCanUnRegisterSucccessfullyTest()
        {
            RegistrationWaldo.RegisterJohnDoe();
            var provider = ProviderFactory.CreateRegistrationProvider();

            var response = provider.UnRegister(new RegistrationBuilder().JohnDoe().IdentityNumber);
            Assert.AreEqual(ResultCode.Success, response.ResultCode, "John Doe did not successfully un-register for event");
        }

        [TestMethod]
        public void UserCanUnSubscribeSucccessfullyTest()
        {
            RegistrationWaldo.RegisterJohnDoe();
            var provider = ProviderFactory.CreateRegistrationProvider();

            var response = provider.Unsubscribe(new RegistrationBuilder().RegisteredJohnDoe().IdentityNumber);
            Assert.AreEqual(ResultCode.Success, response.ResultCode, "John Doe did not successfully un-subscribe for event news");
        }

        [TestMethod]
        public void UserCannotUnSubscribeSucccessfullyTest()
        {
            RegistrationWaldo.RegisterJohnDoe();
            var provider = ProviderFactory.CreateRegistrationProvider();

            var response = provider.Unsubscribe(new RegistrationBuilder().CreateAnError().IdentityNumber);
            Assert.AreEqual(ResultCode.Failure, response.ResultCode, "John Doe did not successfully un-subscribe for event news");
        }

        [TestMethod]
        public void UnableVerifyUserRegisteredStatusSucccessfullyTest()
        {
            RegistrationWaldo.RegisterJohnDoe();
            var provider = ProviderFactory.CreateRegistrationProvider();

            var response = provider.GetRegisteredStatus(new RegistrationBuilder().JohnDoe().CreateAnError().IdentityNumber);
            Assert.AreEqual(ResultCode.Failure, response.ResultCode, "Did not manage to retrieve John Doe's registered status");
            Assert.AreEqual(Status.Undefined, response.Data, "Could not find any data");
        }

        [TestMethod]
        public void VerifyUserRegisteredStatusSucccessfullyTest()
        {
            RegistrationWaldo.RegisterJohnDoe();
            var provider = ProviderFactory.CreateRegistrationProvider();

            var response = provider.GetRegisteredStatus(new RegistrationBuilder().RegisteredJohnDoe().IdentityNumber);
            Assert.AreEqual(ResultCode.Success, response.ResultCode, "Did not manage to retrieve John Doe's registered status");
            Assert.AreEqual(Status.Registered, response.Data, "John Doe should be registered");
        }

        [TestMethod]
        public void RegisterUserSendEmailSuccessfullyTest()
        {
            // Ensure that the unit test passed for sending an email after registration.
            throw new NotImplementedException();
        }

        [TestMethod]
        public void UnSubscribeSendEmailSuccessfullyTest()
        {
            // ensure that the unit test passes for sending an email after a successful unsubscription. 
            throw new NotImplementedException();
        }
    }
}
