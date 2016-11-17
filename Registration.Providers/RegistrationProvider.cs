
namespace Registration.Providers
{
    using System;
    using Contracts;
    using Contracts.Providers;
    using Contracts.Registration;
    using Adapters.Contracts;
    using Adapters;
    using Adapters.Stubs;

    public class RegistrationProvider : IRegistrationProvider
    {
        /// <summary>
        /// The adapter
        /// </summary>
        private readonly IRegistrationAdapter adapter;

        /// <summary>
        /// The communucation adapter
        /// </summary>
        private readonly ICommunicationAdapter communucationAdapter;

        /// <summary>
        /// The communication templates
        /// </summary>
        private readonly ICommunicationTemplates communicationTemplates;

        public RegistrationProvider()
        {
            this.adapter = AdapterFactory.CreateRegistrationAdapter();
            this.communucationAdapter = AdapterFactory.CreateComminucationAdapterInstance();
            this.communicationTemplates = AdapterFactory.CreateComminucationtemplatesInstance();
        }

        public Result<Registration> Register(RegistrationRequest request)
        {
            var registrationStatusOutcome = this.adapter.FetchRegistrationStatus(request.IdentityNumber);

            if (registrationStatusOutcome.ResultCode == ResultCode.Failure)
            {
                return new Result<Registration> { ResultCode = registrationStatusOutcome.ResultCode };
            }
            
            if (registrationStatusOutcome.Data == Status.Registered)
            {
                return new Result<Registration> { ResultCode =  ResultCode.Failure };
            }

            var registerOutcome = this.adapter.RegisterUser(request);

            if (registerOutcome.ResultCode == ResultCode.Failure)
            {
                return new Result<Registration> { ResultCode = ResultCode.Failure };
            }

            var communicationTemplateOutcome = this.communicationTemplates.BuildSuccessfulRegistrationTemplate(request.Name);

            var sendEmailOutcome = this.communucationAdapter.SendEmail(request.Email, "Registration", communicationTemplateOutcome);

            if (sendEmailOutcome == ResultCode.Failure)
            {
                return new Result<Registration> { ResultCode = ResultCode.PartialSuccess, Data = registerOutcome.Data };
            }


            return registerOutcome;
        }

        public Result<Status> GetRegisteredStatus(string identityNumber)
        {
            var statusOutcome = this.adapter.FetchRegistrationStatus(identityNumber);

            if (statusOutcome.ResultCode == ResultCode.Failure)
            {
                return new Result<Status> { ResultCode = ResultCode.Failure };
            }

            return statusOutcome;
        }

        public Result<Registration> Unsubscribe(string identityNumber)
        {
            var statusOutcome = this.adapter.Unsubscribe(identityNumber);

            if (statusOutcome.ResultCode != ResultCode.Success)
            {
                return statusOutcome;
            }

            var communicationTemplateOutcome = this.communicationTemplates.BuildUnSubscribeEmail(statusOutcome.Data.Name, statusOutcome.Data.Email);

            var sendEmailOutcome = this.communucationAdapter.SendEmail(statusOutcome.Data.Email, "Unsubscribe", communicationTemplateOutcome);

            if (sendEmailOutcome != ResultCode.Success)
            {
                return new Result<Registration> { ResultCode = ResultCode.PartialSuccess, Data = statusOutcome.Data };
            }

            return statusOutcome;
        }

        public Result<Registration> UnRegister(string identityNumber)
        {
            var unRegisteredOutcome = this.adapter.UnRegister(identityNumber);

            if (unRegisteredOutcome.ResultCode != ResultCode.Success)
            {
                return unRegisteredOutcome;
            }

            var communicationTemplateOutcome = this.communicationTemplates.BuildUnRegisterTemplate(unRegisteredOutcome.Data.Name);

            var sendEmailOutcome = this.communucationAdapter.SendEmail(unRegisteredOutcome.Data.Email, "De-Registered", communicationTemplateOutcome);

            if (sendEmailOutcome != ResultCode.Success)
            {
                return new Result<Registration> { ResultCode = ResultCode.PartialSuccess, Data = unRegisteredOutcome.Data };
            }

            return unRegisteredOutcome;
        }
    }
}
