using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Nec.Utilities.IoC;
using Owin;

[assembly: OwinStartup(typeof(Nec.API.Startup))]

namespace Nec.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
