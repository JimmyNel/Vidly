using Microsoft.Owin;
using Owin;
using VidlyExercice1;

[assembly: OwinStartup(typeof(Startup))]
namespace VidlyExercice1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
