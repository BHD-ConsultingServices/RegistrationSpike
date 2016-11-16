// <copyright file="RegisterController.cs">
// </copyright>
// <summary>Implements the register controller class</summary>
using Contracts = Registration.Contracts.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Registration.Contracts;

namespace Registration.API.Areas.v1.Controllers
{
    /// <summary>
    /// A controller for handling new registrations.
    /// </summary>
    [RoutePrefix("api/v1/register")]
    public class RegisterController : ApiController
    {
        /// <summary>
        /// Creates a new registration record.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// A registration result.
        /// </returns>
        [HttpPost]
        [Route("addnew")]
        public Result<Contracts.Registration.Registration> Register(Contracts.Registration.RegistrationRequest request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the registration status using identification number.
        /// </summary>
        /// <param name="identityNumber">The identity number.</param>
        /// <returns>
        /// The registered status.
        /// </returns>
        [HttpGet]
        [Route("status/{identityNumber}")]
        public Result<Contracts.Registration.Status> GetRegisteredStatus(string identityNumber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Unsubscribes a registration from the service using its identification number.
        /// </summary>
        /// <param name="identityNumber">The identity number.</param>
        /// <returns>
        /// A registration result.
        /// </returns>
        [HttpPut]
        [Route("unsubscribe/{identityNumber}")]
        public Result<Contracts.Registration.Registration> Unsubscribe(string identityNumber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Unregisteres a registration from the service using its identification number.
        /// </summary>
        /// <param name="identityNumber">The identity number.</param>
        /// <returns>
        ///A registration result.
        /// </returns>
        [HttpPut]
        [Route("unregister/{identityNumber}")]
        public Result<Contracts.Registration.Registration> UnRegister(string identityNumber)
        {
            throw new NotImplementedException();
        }
    }
}
