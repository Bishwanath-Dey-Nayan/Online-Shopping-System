using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NayanTraders.Startup))]
namespace NayanTraders
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
