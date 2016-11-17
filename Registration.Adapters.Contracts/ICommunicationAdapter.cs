using Registration.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Adapters.Contracts
{
    public interface ICommunicationAdapter
    {
        ResultCode SendEmail(string toAddress, string subject, string body);
    }
}
