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
            var templateBuilder = new StringBuilder();

            templateBuilder.Append($"Hello {name}");
            templateBuilder.Append("\n\n");
            templateBuilder.Append("Your registration was successful. You are now subscribed with us and you will be receiving regular news letters.");
            templateBuilder.Append("\n\n");
            templateBuilder.Append("Enjoy!!!");
            templateBuilder.Append("\n\n");
            templateBuilder.Append("Regards");
            templateBuilder.Append("Buddy");

            return templateBuilder.ToString();
        }

        public string BuildUnRegisterTemplate(string name)
        {
            var templateBuilder = new StringBuilder();

            templateBuilder.Append($"Hello {name}");
            templateBuilder.Append("\n\n");
            templateBuilder.Append("You have unregistered successfully. How said. ");
            templateBuilder.Append("\n\n");
            templateBuilder.Append("Regards");
            templateBuilder.Append("Buddy");

            return templateBuilder.ToString();
        }

        public string BuildUnSubscribeEmail(string name, string emailAddress)
        {
            var templateBuilder = new StringBuilder();

            templateBuilder.Append($"Hello {name}");
            templateBuilder.Append("\n\n");
            templateBuilder.Append("You have unsubscribed successfully. How said. You will no longer receive regular news letters.");
            templateBuilder.Append("\n\n");
            templateBuilder.Append("Regards");
            templateBuilder.Append("Buddy");

            return templateBuilder.ToString();
        }
    }
}
