using CompanyExchangeApp.Dialog.ViewModels;
using CompanyExchangeApp.Dialog.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CompanyExchangeApp.Dialog
{
    public class DialogModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<SymbolEditView, SymbolEditViewModel>();
        }
    }
}