using CompanyExchangeApp.Landing.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyExchangeApp.Dialog.ViewModels
{
    public class SymbolEditViewModel : BindableBase, IDialogAware
    {
        private readonly IEventAggregator _eventAggregator;
        public DelegateCommand CloseDialogCommand { get; }
        public DelegateCommand SaveDialogCommand { get; }
        
        public SymbolEditViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            CloseDialogCommand = new DelegateCommand(Close);
            SaveDialogCommand = new DelegateCommand(Save);
        }

        public string Title => "Symbol Edit";

        public event Action<IDialogResult> RequestClose;

        public void Close()
        {
            RequestClose?.Invoke(null);
        }
        public void Save()
        {
            _eventAggregator.GetEvent<OnDialogClosedEvent>().Publish();
        }
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            bool isNewData = parameters.GetValue<bool>("IsNewData");
        }
    }
}
