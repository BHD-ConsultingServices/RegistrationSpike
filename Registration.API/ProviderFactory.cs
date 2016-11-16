using Registration.Contracts.Providers;
using Registration.Providers;
using Registration.Providers.Stubs;
using System;
using System.Configuration;

namespace Registration.API
{
    /// <summary>
    /// A provider factory.
    /// </summary>
    public static class ProviderFactory
    {
        /// <summary>
        /// Gets a provider instance.
        /// </summary>
        /// <param name="actual">true to actual.</param>
        /// <returns>
        /// The instance.
        /// </returns>
        public static IRegistrationProvider GetInstance()
        {
            var simulated = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("simulated"));
            if (simulated)
                return new RegistrationProviderStub();
            return new RegistrationProvider();
        }
    }
}