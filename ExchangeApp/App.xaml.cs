using CompanyExchangeApp.Business;
using CompanyExchangeApp.Business.Interface;
using CompanyExchangeApp.Business.Services;
using CompanyExchangeApp.Dialog;
using CompanyExchangeApp.Landing;
using ExchangeApp.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace ExchangeApp
{
     public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AutoMapperConfig.Configure();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISymbolService, SymbolServices>();
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<LandingModule>();
            moduleCatalog.AddModule<DialogModule>();
        }
    }
}
