using Registration.Adapters.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Contracts;
using System.Net.Configuration;
using System.Configuration;

namespace Registration.Adapters
{
    public class CommunicationAdapter : ICommunicationAdapter
    {
        public ResultCode SendEmail(string toAddress, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
