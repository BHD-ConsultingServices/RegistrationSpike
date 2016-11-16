using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using System.Web;

[assembly: OwinStartup(typeof(Registration.API.Startup))]

namespace Registration.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            ConfigureApplication();

        }

        private static void ConfigureApplication()
        {
            var config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            if (ConfigurationManager.AppSettings["simulated"] == null)
            {
                config.AppSettings.Settings.Add("simulated", "true");
                config.Save();

            }
        }
    }
}
