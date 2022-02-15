using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LodhranStore.Startup))]
namespace LodhranStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
