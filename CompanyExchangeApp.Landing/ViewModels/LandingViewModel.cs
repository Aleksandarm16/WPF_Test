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
        private bool _isDbPathGood;
        public bool IsDbPathGood
        {
            get { return _isDbPathGood; }
            set { SetProperty(ref _isDbPathGood, value); }
        }
        private IList<Symbol> _symbols;
        public IList<Symbol> Symbols
        {
            get { return _symbols; }
            set { SetProperty(ref _symbols, value); }
        }
        private IList<Type> _types;
        public IList<Type> Types
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

        private IList<Exchange> _exchanges;
        public IList<Exchange> Exchanges
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


        private Symbol _selectedSymbol;
        public Symbol SelectedSymbol
        {
            get { return _selectedSymbol; }
            set { SetProperty(ref _selectedSymbol, value); }
        }
        private readonly ISymbolService _symbolService;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        public DelegateCommand BrowseFileCommand { get; set; }
        public DelegateCommand AddSymbolCommand { get; set; }
        public DelegateCommand EditSymbolCommand { get; set; }
        public DelegateCommand DeleteSymbolCommand { get; set; }
        public DelegateCommand FilterCommand { get; set; }

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
            Type selectedType = Types.FirstOrDefault(t => t.Name.Equals(SelectedType));
            Exchange selectedExchange = Exchanges.FirstOrDefault(e => e.Name.Equals(SelectedExchange));
            Symbols = await _symbolService.GetAllSymbolsAsync(selectedType,selectedExchange);
        }

        private void OnEditSymbol()
        {
            var parameters = new DialogParameters
            {
                {"IsNewData", false }
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
                {"IsNewData", true }
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
    }
}
