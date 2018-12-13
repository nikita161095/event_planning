using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(event_planning.Startup))]
namespace event_planning
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
