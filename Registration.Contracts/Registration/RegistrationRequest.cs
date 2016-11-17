using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Contracts.Registration
{
    public class RegistrationRequest
    {
        public string IdentityNumber { get; set; }

        public string Name { get; set; }

        public DateTime BirthDay { get; set; }

        public bool Subscribed { get; set; }

        public string EmailAddress { get; set; }
    }
}
