
namespace Registration.Adapters.Contracts
{
    using Registration.Contracts.Registration;
    using Registration.Contracts;

    public interface IRegistrationAdapter
    {
        Result<Status> FetchRegistrationStatus(string identityNumber);

        Result<Registration> RegisterUser(RegistrationRequest request);

        Result<Registration> GetRegisteredUser(string identityNumber);

        Result<Registration> Unsubscribe(string identityNumber);

        Result<Registration> UnRegister(string identityNumber);
    }
}
