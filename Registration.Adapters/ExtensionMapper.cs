using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Adapters
{
    using Registration.Contracts.Registration;

    public static class ExtensionMapper
    {
        public static Registration Map(this RegistrationRequest original)
        {
            return new Registration
            {
                Id = Guid.NewGuid(),
                IdentityNumber = original.IdentityNumber,
                Name = original.Name,
                BirthDay = original.BirthDay,
                Subscribed = original.Subscribed
            };
        }
    }
}
