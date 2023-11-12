using CompanyExchangeApp.Business.Dtos;
using CompanyExchangeApp.Business.Interface;
using CompanyExchangeApp.Business.Models;
using CompanyExchangeApp.Landing.Events;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Type = CompanyExchangeApp.Business.Models.Type;

namespace CompanyExchangeApp.Landing.ViewModels
{
    public class LandingViewModel : BindableBase, IDisposable
    {

        #region Properties
        private bool _isDbPathGood;
        public bool IsDbPathGood
        {
            get { return _isDbPathGood; }
            set { SetProperty(ref _isDbPathGood, value); }
        }
        private IList<SymbolDto> _symbols;
        public IList<SymbolDto> Symbols
        {
            get { return _symbols; }
            set { SetProperty(ref _symbols, value); }
        }
        private IList<TypeDto> _types;
        public IList<TypeDto> Types
        {
            get { return _types; }
            set { SetProperty(ref _types, value); }
        }
        private string _selectedType;
        public string SelectedType
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

        private string _selectedExchange;
        public string SelectedExchange
        {
            get { return _selectedExchange; }
            set { SetProperty(ref _selectedExchange, value); }
        }


        private SymbolDto _selectedSymbol;
        public SymbolDto SelectedSymbol
        {
            get { return _selectedSymbol; }
            set { SetProperty(ref _selectedSymbol, value); }
        }
        #endregion

        #region readonly properties
        private readonly ISymbolService _symbolService;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        #endregion

        #region Command properties

        public DelegateCommand BrowseFileCommand { get; set; }
        public DelegateCommand AddSymbolCommand { get; set; }
        public DelegateCommand EditSymbolCommand { get; set; }
        public DelegateCommand DeleteSymbolCommand { get; set; }
        public DelegateCommand FilterCommand { get; set; }
        #endregion

        public LandingViewModel(ISymbolService symbolService, IDialogService dialogService, IEventAggregator eventAggregator)
        {
            _symbolService = symbolService;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OnDialogClosedEvent>().Subscribe(ReloadData);
            BrowseFileCommand = new DelegateCommand(OnBrowseFileCommand);
            AddSymbolCommand = new DelegateCommand(OnAddSymbol,CanAddSymbol);
            EditSymbolCommand = new DelegateCommand(OnEditSymbol, CanEditSymbol);
            DeleteSymbolCommand = new DelegateCommand(OnDeleteSymbol, CanDeleteSymbol);
            FilterCommand = new DelegateCommand(OnFilterCommand);
            PropertyChanged += OnPropertyChanged;
        }

        #region Command methods
        private void OnBrowseFileCommand()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select a file",
                Filter = "SQLite Database Files(*.s3db) | *.s3db",
                CheckFileExists = true,
                CheckPathExists = true
            };

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                LoadSymbols(selectedFilePath);
            }          
        }

        private async void OnFilterCommand()
        {
            TypeDto selectedType = Types.FirstOrDefault(t => t.Name.Equals(SelectedType));
            ExchangeDto selectedExchange = Exchanges.FirstOrDefault(e => e.Name.Equals(SelectedExchange));
            Symbols = await _symbolService.GetAllSymbolsAsync(selectedType,selectedExchange);
        }

        private void OnEditSymbol()
        {
            var parameters = new DialogParameters
            {
                {LandingPageParameters.IsNewData, false },
                {LandingPageParameters.Type, Types },
                {LandingPageParameters.Exchange, Exchanges},
                {LandingPageParameters.Symbol, SelectedSymbol},
            };
            _dialogService.ShowDialog("SymbolEditView", parameters, null);
        }
        private bool CanEditSymbol()
        {
            if (SelectedSymbol != null)
            {
                return true;
            }
            return false;
        }
        private void OnAddSymbol()
        {
            var parameters = new DialogParameters
            {
                {LandingPageParameters.IsNewData, true },
                {LandingPageParameters.Type, Types },
                {LandingPageParameters.Exchange, Exchanges}
            };
            _dialogService.ShowDialog("SymbolEditView", parameters, null);
        }
        private bool CanAddSymbol()
        {
            if(IsDbPathGood)
            {
                return true;
            }
            return false;
        }
        private void OnDeleteSymbol()
        {

        }
        private bool CanDeleteSymbol()
        {
            if (SelectedSymbol != null)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Load methods
        private async Task LoadData()
        {
            try
            {
                Symbols = await _symbolService.GetAllSymbolsAsync();
                Exchanges = await _symbolService.GetExchangesAsync();
                Types = await _symbolService.GetTypesAsync();
                SelectedExchange = "ALL";
                SelectedType = "ALL";
                IsDbPathGood = true;
            }
            catch (Exception)
            {
                IsDbPathGood = false;
            }
        }
        private async void LoadSymbols(string connectionPath)
        {
            _symbolService.SetDbConnectionString(connectionPath);
            await LoadData();
        }

        private async void ReloadData ()
        {
            await LoadData();
        }

        #endregion

        #region Helper Methods
        public void Dispose()
        {
            _eventAggregator.GetEvent<OnDialogClosedEvent>().Unsubscribe(ReloadData);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsDbPathGood))
            {
                // Symbol property changed, invoke CanAddSymbol
                AddSymbolCommand.RaiseCanExecuteChanged();
            }
            if (e.PropertyName == nameof(SelectedSymbol))
            {
                // Symbol property changed, invoke CanEditSymbol and CanDeleteSymbol
                DeleteSymbolCommand.RaiseCanExecuteChanged();
                EditSymbolCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion
    }
}
