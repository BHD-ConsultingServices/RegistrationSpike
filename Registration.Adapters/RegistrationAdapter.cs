
namespace Registration.Adapters
{
    using System;
    using System.Linq;
    using Registration.Adapters.Contracts;
    using Registration.Contracts;
    using Registration.Contracts.Registration;
    using DataContext;

    public class RegistrationAdapter : IRegistrationAdapter
    {
        public Result<Registration> GetRegisteredUser(string identityNumber)
        {
            var context = new RegistrationContext();
            var user = context.Registrations.Where(x => x.IdentityNumber == identityNumber).SingleOrDefault();

            if (user == null)
            {
                return new Result<Registration> { ResultCode = ResultCode.Undefined };
            }

            return new Result<Registration> { ResultCode = ResultCode.Success, Data = user };
        }

        public Result<Registration> RegisterUser(RegistrationRequest request)
        {
            var context = new RegistrationContext();
            var mappedRequest = request.Map();

            context.Registrations.Add(mappedRequest);
            context.SaveChanges();

            return new Result<Registration> { ResultCode = ResultCode.Success, Data = mappedRequest };
        }

        public Result<Status> FetchRegistrationStatus(string identityNumber)
        {
            var context = new RegistrationContext();
            var user = context.Registrations.Where(x => x.IdentityNumber == identityNumber).SingleOrDefault();

            if (user == null)
            {
                return new Result<Status> { ResultCode = ResultCode.Undefined };
            }

            if (user.RegistrationStatus == Status.Registered)
            {
                return new Result<Status> { ResultCode = ResultCode.Success, Data = Status.Registered };
            }

            return new Result<Status> { ResultCode = ResultCode.Success, Data = Status.UnRegistered };
        }

        public Result<Registration> Unsubscribe(string identityNumber)
        {
            var context = new RegistrationContext();
            var user = context.Registrations.Where(x => x.IdentityNumber == identityNumber).SingleOrDefault();

            if (user == null)
            {
                return new Result<Registration> { ResultCode = ResultCode.Undefined };
            }

            user.Subscribed = false;
            context.SaveChanges();

            return new Result<Registration> { ResultCode = ResultCode.Success, Data = user };
        }

        public Result<Registration> UnRegister(string identityNumber)
        {
            var context = new RegistrationContext();
            var user = context.Registrations.Where(x => x.IdentityNumber == identityNumber).SingleOrDefault();

            if (user == null)
            {
                return new Result<Registration> { ResultCode = ResultCode.Undefined };
            }

            user.RegistrationStatus = Status.UnRegistered;
            context.SaveChanges();

            return new Result<Registration> { ResultCode = ResultCode.Success, Data = user };
        }
    }
}
