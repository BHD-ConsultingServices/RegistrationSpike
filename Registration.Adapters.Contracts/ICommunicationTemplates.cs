using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Adapters.Contracts
{
    public interface ICommunicationTemplates
    {
        string BuildSuccessfulRegistrationTemplate(string name);

        string BuildUnSubscribeEmail(string name, string emailAddress);

        string BuildUnRegisterTemplate(string name);
    }
}
