
namespace Registration.Testing.Data.Builders
{
    using System;
    using Contracts.Registration;
    
    public class RegistrationBuilder : Contracts.Registration.Registration
    {
        public RegistrationBuilder(Guid? id = null)
        {
            this.Id = id ?? Guid.NewGuid();
        }

        public RegistrationBuilder JohnDoe()
        {
            this.Name = "John Doe";
            this.IdentityNumber = "8504065149076";
            this.BirthDay = new DateTime().AddYears(-31);
            this.Subscribed = true;

            return this;
        }

        public RegistrationBuilder JaneDoe()
        {
            this.Name = "Jane Doe";
            this.IdentityNumber = "9002045149077";
            this.BirthDay = new DateTime().AddYears(-29);
            this.Subscribed = true;

            return this;
        }


        public Registration Build()
        {
            return this;
        }

        public RegistrationRequest BuildRequest()
        {
            return this.MapRequest();
        }
    }
}
