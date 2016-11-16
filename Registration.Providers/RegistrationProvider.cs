
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
        private readonly IRegistrationAdapter adapter;

        public RegistrationProvider()
        {
            this.adapter = new RegistrationAdapter();
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
                // var userOutcome = this.adapter.GetRegisteredUser(request.IdentityNumber);

                //if (userOutcome.ResultCode == ResultCode.Failure)
                //{
                //    return new Result<Registration> { ResultCode = ResultCode.Failure };
                //}

                return new Result<Registration> { ResultCode =  ResultCode.Failure };
            }

            var registerOutcome = this.adapter.RegisterUser(request);

            if (registerOutcome.ResultCode == ResultCode.Failure)
            {
                return new Result<Registration> { ResultCode = ResultCode.Failure };
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
            return statusOutcome;
        }

        public Result<Registration> UnRegister(string identityNumber)
        {
            throw new NotImplementedException();
        }
    }
}
