
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

            this.IdentityNumber = "8504065149076";
            this.EmailAddress = "johndoe@test.com";
            this.BirthDay = DateTime.Now.AddYears(-31);
            this.Subscribed = true;

            return this;
        }
        
        public RegistrationBuilder JaneDoe()
        {

            this.IdentityNumber = "9002045149077";
            this.EmailAddress = "janedoe@test.com";
            this.BirthDay = DateTime.Now.AddYears(-29);
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
