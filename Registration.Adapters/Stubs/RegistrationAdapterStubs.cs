using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Adapters.Stubs
{
    using Registration.Adapters.Contracts;
    using System;
    using Registration.Contracts;
    using Registration.Contracts.Registration;

    public class RegistrationAdapterStubs : IRegistrationAdapter
    {
        public Result<Registration> GetRegisteredUser(string identityNumber)
        {
            if (identityNumber.ToLower().Trim() == "error")
            {
                return new Result<Registration> { ResultCode = ResultCode.Failure };
            }

            return new Result<Registration>
            {
                ResultCode = ResultCode.Success,
                Data = new Registration
                {
                    Id = Guid.NewGuid(),
                    IdentityNumber = "123456",
                    Name = "John Doe",
                    BirthDay = new DateTime(1991, 5, 6),
                    Subscribed = true
                }
            };
        }
        
        public Result<bool> IsIDNumberValid(string identityNumber)
        {
            if (identityNumber.ToLower().Trim() == "error")
            {
                return new Result<bool> { ResultCode = ResultCode.Failure };
            }

            if (identityNumber.ToLower().Trim() == "invalid")
            {
                return new Result<bool> { ResultCode = ResultCode.Success, Data = false };
            }

            return new Result<bool> { ResultCode = ResultCode.Success, Data = true };
        }

        public Result<Registration> RegisterUser(RegistrationRequest request)
        {
            if (request.IdentityNumber.ToLower().Trim() == "error")
            {
                return new Result<Registration> { ResultCode = ResultCode.Failure };
            }

            return new Result<Registration>
            {
                ResultCode = ResultCode.Success,
                Data = new Registration
                {
                    Id = Guid.NewGuid(),
                    IdentityNumber = "123456",
                    Name = "John Doe",
                    BirthDay = new DateTime(1991, 5, 6),
                    Subscribed = true
                }
            };
        }

        public Result<Status> FetchRegistrationStatus(string identityNumber)
        {
            if (identityNumber.ToLower().Trim() == "error")
            {
                return new Result<Status> { ResultCode = ResultCode.Failure };
            }

            if (identityNumber.ToLower().Trim() == "registered")
            {
                return new Result<Status> { ResultCode = ResultCode.Success, Data = Status.Registered };
            }

            return new Result<Status> { ResultCode = ResultCode.Success, Data = Status.UnRegistered };
        }

        public Result<Registration> Unsubscribe(string identityNumber)
        {
            if (identityNumber.ToLower().Trim() == "error")
            {
                return new Result<Registration> { ResultCode = ResultCode.Failure };
            }

            if (identityNumber.ToLower().Trim() == "notfound")
            {
                return new Result<Registration> { ResultCode = ResultCode.Undefined };
            }

            return new Result<Registration> {
                ResultCode = ResultCode.Success,
                Data = new Registration 
                {
                    Id = Guid.NewGuid(),
                    IdentityNumber = "123456",
                    Name = "John Doe",
                    BirthDay = new DateTime(1991, 5, 6),
                    Subscribed = true
                }
            };
        }
    }
}
