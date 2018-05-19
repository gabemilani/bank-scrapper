using BankScrapper.Data;
using BankScrapper.Domain.Interfaces;
using System;
using Unity;
using Unity.Injection;

namespace BankScrapper.Web
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        public static IUnityContainer Container => container.Value;
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterSingleton<BankScrapperDbContext>(
                new [] { new InjectionConstructor(new object[] { "BankScrapperDb" }) });

            container.RegisterSingleton<IContext, BankScrapperContext>();
        }
    }
}