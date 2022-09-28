using KD.Core.DomainModels;
using KD.WPF.Client.APIClient.RestServices;
using KD.WPF.Client.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KD.WPF.Client.ViewModels
{
    public class StockViewModel : BindableBase
    {
        #region Properties
        private readonly StockRestService stockRestService;
        private ObservableCollection<StockModel> stocks;
        public ObservableCollection<StockModel> Stocks
        {
            get { return stocks; }
            set { SetProperty(ref stocks, value); }
        }

        private StockModel selectedStock;
        public StockModel SelectedStock 
        { 
            get { return selectedStock; } 
            set { SetProperty(ref selectedStock, value); } 
        }
        public DelegateCommand DeleteStockCommand { get; private set; }
        public DelegateCommand AddStockCommand { get; private set; }
        public DelegateCommand UpdateStockCommand { get; private set; }
        #endregion Properties

        #region Constructor
        public StockViewModel(StockRestService stockRestService)
        {
            this.stockRestService = stockRestService;
            DeleteStockCommand = new DelegateCommand(DeleteStock);
            AddStockCommand = new DelegateCommand(AddStock);
            UpdateStockCommand = new DelegateCommand(UpdateStock);
            Task.Run(() => this.Initialize()).Wait();
        }
        #endregion Constructor

        #region Methods
        private async Task Initialize()
        {
            await GetStocks();
        }

        private async void DeleteStock()
        {
            if (selectedStock == null)
            {
                MessageBox.Show("Please select the stock you want to delete !");
                return;
            }
            Guid stockId = SelectedStock.StockId;
            await stockRestService.DeleteStockAsync(stockId);
            await GetStocks();
        }

        private async void AddStock()
        {
            StockModel newStock = Stocks.FirstOrDefault(s => s.StockId == Guid.Empty);
            if (newStock == null)
            {
                MessageBox.Show("Please enter a stock into the grid !");
            }
            await stockRestService.CreateStockAsync(newStock);
            await GetStocks();
        }

        private async void UpdateStock()
        {
            if (selectedStock == null)
            {
                MessageBox.Show("Please select the stock you want to update !");
                return;
            }
            await stockRestService.UpdateStockAsync(selectedStock.StockId, selectedStock);
            await GetStocks();
        }

        private async Task GetStocks()
        {
            Stocks = new ObservableCollection<StockModel>(await stockRestService.GetAllStocksAsync());
        }

            #endregion Methods
        }
    }