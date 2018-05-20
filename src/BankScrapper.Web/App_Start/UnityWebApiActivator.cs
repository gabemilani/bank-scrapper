using System.Web.Http;

using Unity.AspNet.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BankScrapper.Web.UnityWebApiActivator), nameof(BankScrapper.Web.UnityWebApiActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(BankScrapper.Web.UnityWebApiActivator), nameof(BankScrapper.Web.UnityWebApiActivator.Shutdown))]

namespace BankScrapper.Web
{
    public static class UnityWebApiActivator
    {
        public static void Start() 
        {
            var resolver = new UnityDependencyResolver(UnityConfig.Container);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        public static void Shutdown()
        {
            UnityConfig.Container.Dispose();
        }
    }
}