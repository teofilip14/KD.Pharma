using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace KD.WPF.Client.Models
{
    public class ProductModel : BindableBase
    {
        #region Constructor
        public ProductModel()
        {
        }
        #endregion Constructor

        #region Properties
        private Guid productId;
        public Guid ProductId
        {
            get { return productId; }
            set { SetProperty(ref productId, value); }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }
        private int price;
        public int Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }
        private string producer;
        public string Producer
        {
            get { return producer; }
            set { SetProperty(ref producer, value); }
        }
        private ObservableCollection<StockModel> stocks;
        public ObservableCollection<StockModel> Stocks
        {
            get { return stocks; }
            set { SetProperty(ref stocks, value); }
        }
        #endregion Properties               
    }
}
