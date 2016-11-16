
namespace Registration.Providers
{
    using Registration.Adapters;
    using Registration.Adapters.Contracts;
    using Registration.Adapters.Stubs;  

    public class AdapterFactory
    {
        private static bool useStubs = false;

        public static IRegistrationAdapter CreateRegistrationAdapter()
        {
            if (useStubs)
            {
                return new RegistrationAdapterStubs();
            }

            return new RegistrationAdapter();
        }
    }
}
