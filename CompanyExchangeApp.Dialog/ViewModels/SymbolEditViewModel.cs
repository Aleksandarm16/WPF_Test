using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyExchangeApp.Dialog.ViewModels
{
    public class SymbolEditViewModel : BindableBase, IDialogAware
    {
        public DelegateCommand CloseDialogCommand { get; }
        public SymbolEditViewModel()
        {
            CloseDialogCommand = new DelegateCommand(Close);
        }

        public string Title => "Symbol Edit";

        public event Action<IDialogResult> RequestClose;

        public void Close()
        {
            RequestClose?.Invoke(null);
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
