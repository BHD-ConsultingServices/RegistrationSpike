﻿
namespace Registration.Providers
{
    using Registration.Adapters;
    using Registration.Adapters.Contracts;
    using Registration.Adapters.Stubs;  

    public class AdapterFactory
    {

        public static IRegistrationAdapter CreateRegistrationAdapter(bool useStubs = false)
        {
            if (useStubs)
            {
                return new RegistrationAdapterStubs();
            }

            return new RegistrationAdapter();
        }

        public static ICommunicationAdapter CreateComminucationAdapterInstance()
        {
            return new CommunicationAdapter();
        }

        public static ICommunicationTemplates CreateComminucationtemplatesInstance()
        {
            return new CommunicationTemplates();
        }
    }
}
