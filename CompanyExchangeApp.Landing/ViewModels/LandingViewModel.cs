using CompanyExchangeApp.Business.Interface;
using CompanyExchangeApp.Business.Models;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Type = CompanyExchangeApp.Business.Models.Type;

namespace CompanyExchangeApp.Landing.ViewModels
{
    public class LandingViewModel : BindableBase
    {
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
        public DelegateCommand BrowseFileCommand { get; }
        public DelegateCommand AddSymbolCommand { get; }
        public DelegateCommand FilterCommand { get; }

        public LandingViewModel(ISymbolService symbolService, IDialogService dialogService)
        {
            _symbolService = symbolService;
            _dialogService = dialogService;
            BrowseFileCommand = new DelegateCommand(OnBrowseFileCommand);
            AddSymbolCommand = new DelegateCommand(OnAddSymbol);
            FilterCommand = new DelegateCommand(OnFilterCommand);
        }

        private async void OnBrowseFileCommand()
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
               
                await GetData(selectedFilePath);
            }
        }

        private async void OnFilterCommand()
        {
            Type selectedType = Types.FirstOrDefault(t => t.Name.Equals(SelectedType));
            Exchange selectedExchange = Exchanges.FirstOrDefault(e => e.Name.Equals(SelectedExchange));
            Symbols = await _symbolService.GetAllSymbolsAsync(selectedType,selectedExchange);
        }
        private void OnAddSymbol()
        {
            var parameters = new DialogParameters
            {
                {"IsNewData", true }
            };
            _dialogService.ShowDialog("SymbolEditView", parameters, null);
        }
        private async Task GetData(string connectionPath)
        {
            _symbolService.SetDbConnectionString(connectionPath);
            Symbols = await _symbolService.GetAllSymbolsAsync();
            Exchanges = await _symbolService.GetExchangesAsync();
            Types = await _symbolService.GetTypesAsync();
            SelectedExchange = "ALL";
            SelectedType = "ALL";
        }
    }
}
