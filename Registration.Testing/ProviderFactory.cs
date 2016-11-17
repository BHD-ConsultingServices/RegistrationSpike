
namespace Registration.Testing
{
    using Adapters.Contracts;
    using Contracts.Providers;
    using Providers.Stubs;
    using Providers;
    using Adapters;
    using Adapters.Stubs;

    public class ProviderFactory
    {
        public static bool ShouldUseStubs = false;

        public static IRegistrationProvider CreateRegistrationProvider()
        {
            if (ShouldUseStubs)
            {
                return new RegistrationProviderStub();
            }

            return new RegistrationProvider();
        }

        public static IRegistrationAdapter CreateRegistrationAdapter()
        {
            if (ShouldUseStubs)
            {
                return new RegistrationAdapterStubs();
            }

            return new RegistrationAdapter();
        }
    }
}
