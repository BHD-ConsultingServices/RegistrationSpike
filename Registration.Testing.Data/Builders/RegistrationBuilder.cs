
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
            this.IdentityNumber = "123456";
            return this;
        }
        
        public RegistrationBuilder JaneDoe()
        {
            this.IdentityNumber = "123";
            this.BirthDay = new DateTime(1987, 01, 01);
            this.Name = "Jane Doe";
            this.Subscribed = true;
            return this;
        }


        public Contracts.Registration.Registration Build()
        {
            return this;
        }

        public RegistrationRequest BuildRequest()
        {
            return this.MapRequest();
        }

        public RegistrationRequest CreateAnError()
        {
            var mapped = this.MapRequest();
            mapped.IdentityNumber = "error";
            return mapped;
        }

        public RegistrationRequest RegisteredJohnDoe()
        {
            var mapped = this.MapRequest();
            mapped.IdentityNumber = "registered";
            return mapped;
        }
    }
}
