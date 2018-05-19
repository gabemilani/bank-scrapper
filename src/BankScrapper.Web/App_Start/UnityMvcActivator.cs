using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Linq;
using System.Web.Mvc;

using Unity.AspNet.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BankScrapper.Web.UnityMvcActivator), nameof(BankScrapper.Web.UnityMvcActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(BankScrapper.Web.UnityMvcActivator), nameof(BankScrapper.Web.UnityMvcActivator.Shutdown))]

namespace BankScrapper.Web
{
    public static class UnityMvcActivator
    {
        public static void Start() 
        {
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(UnityConfig.Container));
            DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        public static void Shutdown()
        {
            UnityConfig.Container.Dispose();
        }
    }
}