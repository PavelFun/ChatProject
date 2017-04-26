using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//The Open Web Interface for .NET
using Microsoft.Owin;
using Owin;

//Указание для сборки.
[assembly: OwinStartup(typeof(SignalRMvc.Start))]

namespace SignalRMvc
{
    public class Start
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
