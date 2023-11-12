using CompanyExchangeApp.Business.Dtos;
using CompanyExchangeApp.Business.Interface;
using CompanyExchangeApp.Business.Models;
using CompanyExchangeApp.Landing;
using CompanyExchangeApp.Landing.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using Type = CompanyExchangeApp.Business.Models.Type;

namespace CompanyExchangeApp.Dialog.ViewModels
{
    public class SymbolEditViewModel : BindableBase, IDialogAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISymbolService _symbolService;

        private SymbolDto  _symbol;
        public SymbolDto Symbol
        {
            get { return _symbol; }
            set { SetProperty(ref _symbol, value); }
        }

        private IList<TypeDto> _types;
        public IList<TypeDto> Types
        {
            get { return _types; }
            set { SetProperty(ref _types, value); }
        }
        private TypeDto _selectedType;
        public TypeDto SelectedType
        {
            get { return _selectedType; }
            set { SetProperty(ref _selectedType, value); }
        }

        private IList<ExchangeDto> _exchanges;
        public IList<ExchangeDto> Exchanges
        {
            get { return _exchanges; }
            set { SetProperty(ref _exchanges, value); }
        }

        private ExchangeDto _selectedExchange;
        public ExchangeDto SelectedExchange
        {
            get { return _selectedExchange; }
            set { SetProperty(ref _selectedExchange, value); }
        }

        private bool _isNewData;
        public bool IsNewData
        {
            get { return _isNewData; }
            set { SetProperty(ref _isNewData, value); }
        }


        public DelegateCommand CloseDialogCommand { get; }
        public DelegateCommand SaveDialogCommand { get; }
        
        public SymbolEditViewModel(IEventAggregator eventAggregator, ISymbolService symbolService)
        {
            _eventAggregator = eventAggregator;
            _symbolService = symbolService;
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
            IsNewData = parameters.GetValue<bool>(LandingPageParameters.IsNewData);
            Symbol = parameters.GetValue<SymbolDto>(LandingPageParameters.Symbol);
            Exchanges = parameters.GetValue<IList<ExchangeDto>>(LandingPageParameters.Exchange);
            Types = parameters.GetValue<IList<TypeDto>>(LandingPageParameters.Type);
            if (IsNewData)
            {
                Symbol = new SymbolDto() { DateAdded = DateTime.Now };
            }
        }
    }
}
