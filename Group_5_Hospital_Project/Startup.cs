using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Group_5_Hospital_Project.Startup))]
namespace Group_5_Hospital_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
