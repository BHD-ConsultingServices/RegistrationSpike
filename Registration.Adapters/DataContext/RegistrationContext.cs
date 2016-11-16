using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Adapters.DataContext
{
    using System.Data.Entity;
    using Registration.Contracts.Registration;

    public class RegistrationContext : DbContext  
    {
        public RegistrationContext() : base("name=dbConnection")
        {

        }

        public DbSet<Registration> Registrations { get; set; }
    }
}
