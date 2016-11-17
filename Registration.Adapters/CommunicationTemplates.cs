using Registration.Adapters.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Adapters
{
    public class CommunicationTemplates : ICommunicationTemplates
    {
        public string BuildSuccessfulRegistrationTemplate(string name)
        {
            return $"<strong>Hello {name} </strong><br/><p>Your registration was successful. You are now subscribed with us and you will be receiving regular news letters.<p><strong>Regards</strong></p>Buddy";
        }

        public string BuildUnRegisterTemplate(string name)
        {
            return $"<strong>Hello {name} </strong><br/><p>You have successfully unregistered. <i>How sad</i>.<p><strong>Regards</strong></p>Buddy";
        }

        public string BuildUnSubscribeEmail(string name, string emailAddress)
        {
            return $"<strong>Hello {name} </strong><br/><p>You have successfully unsubscribed. <i>How sad.</i>.<p><strong>Regards</strong></p>Buddy";
        }
    }
}
