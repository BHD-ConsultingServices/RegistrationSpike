// <copyright file="RegisterController.cs">
// </copyright>
// <summary>Implements the register controller class</summary>
using Registration.Contracts;
using Registration.Providers;
using Registration.Providers.Stubs;
using System;
using System.Web.Http;

namespace Registration.API.Areas.v1.Controllers
{
    /// <summary>
    /// A controller for handling new registrations.
    /// </summary>
    [RoutePrefix("api/v1/registration")]
    public class RegisterController : ApiController
    {
        /// <summary>
        /// Creates a new registration request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// A registration result.
        /// </returns>
        [HttpPost]
        [Route("register")]
        public Result<Contracts.Registration.Registration> Register(Contracts.Registration.RegistrationRequest request)
        {
            var provider = ProviderFactory.GetInstance();
            var result = provider.Register(request);

            return result;
        }

        /// <summary>
        /// Retrieves the registration status of a specific identity number.
        /// </summary>
        /// <param name="identityNumber">The identity number.</param>
        /// <returns>
        /// The registered status.
        /// </returns>
        [HttpGet]
        [Route("status/{identityNumber}")]
        public Result<Contracts.Registration.Status> GetRegisteredStatus(string identityNumber)
        {
            var provider = ProviderFactory.GetInstance();
            var result = provider.GetRegisteredStatus(identityNumber);
            return result;
        }

        /// <summary>
        /// Unsubscribes a registered identity number.
        /// </summary>
        /// <param name="identityNumber">The identity number.</param>
        /// <returns>
        /// A registration result.
        /// </returns>
        [HttpPost]
        [Route("unsubscribe/{identityNumber}")]
        public Result<Contracts.Registration.Registration> Unsubscribe(string identityNumber)
        {
            var provider = ProviderFactory.GetInstance();
            var result = provider.Unsubscribe(identityNumber);
            return result;
        }

        /// <summary>
        /// Un-registers a specific identity number.
        /// </summary>
        /// <param name="identityNumber">The identity number.</param>
        /// <returns>
        ///A registration result.
        /// </returns>
        [HttpPost]
        [Route("unregister/{identityNumber}")]
        public Result<Contracts.Registration.Registration> UnRegister(string identityNumber)
        {
            var provider = ProviderFactory.GetInstance();
            var result = provider.UnRegister(identityNumber);
            return result;
        }
    }
}
