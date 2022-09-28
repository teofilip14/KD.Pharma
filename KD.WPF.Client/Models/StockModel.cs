using Prism.Mvvm;
using System;
using System.Linq;

namespace KD.WPF.Client.Models
{
    public class StockModel : BindableBase
    {
        #region Constructor
        public StockModel()
        {
        }
        #endregion Constructor

        #region Properties
        private Guid stockId;
        public Guid StockId
        {
            get { return stockId; }
            set { SetProperty(ref stockId, value); }
        }

        private Guid productId;
        public Guid ProductId
        {
            get { return productId; }
            set { SetProperty(ref productId, value); }
        }

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }

        private ProductModel product;
        public ProductModel Product
        {
            get { return product; }
            set { SetProperty(ref product, value); }
        }
        #endregion Properties
    }
}
