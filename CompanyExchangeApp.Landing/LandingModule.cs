using CompanyExchangeApp.Landing.ViewModels;
using CompanyExchangeApp.Landing.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CompanyExchangeApp.Landing
{
    public class LandingModule : IModule
    {
        private readonly IRegionManager _regionManager;
        public LandingModule(IRegionManager regionManager)
        {
                _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(LandingView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LandingView,LandingViewModel>();
        }
    }
}