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
    public class ProductViewModel : BindableBase
    {
        #region Properties
        private readonly ProductRestService productRestService;
        private ObservableCollection<ProductModel> products;
        public ObservableCollection<ProductModel> Products
        {
            get { return products; }
            set { SetProperty(ref products, value); }
        }

        private ProductModel selectedProduct;
        public ProductModel SelectedProduct
        {
            get { return selectedProduct; }
            set { SetProperty(ref selectedProduct, value); }
        }

        public DelegateCommand DeleteProductCommand { get; private set; }
        public DelegateCommand AddProductCommand { get; private set; }
        public DelegateCommand UpdateStudentCommand { get; private set; }
        #endregion Properties

        #region Constructor
        public ProductViewModel(ProductRestService productRestService)
        {
            this.productRestService = productRestService;
            DeleteProductCommand = new DelegateCommand(DeleteProduct);
            AddProductCommand = new DelegateCommand(AddProduct);
            UpdateStudentCommand = new DelegateCommand(UpdateProduct);
            Task.Run(() => this.Initialize()).Wait();
        }
        #endregion Constructor

        #region Methods
        private async Task Initialize()
        {
            try
            {
                await GetProducts();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private async void DeleteProduct()
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Please select the product you want to delete !");
                return;
            }
            Guid productId = SelectedProduct.ProductId;
            await productRestService.DeleteProductAsync(productId);
            await GetProducts();
        }

        private async void AddProduct()
        {
            ProductModel newProduct = Products.FirstOrDefault(s => s.ProductId == Guid.Empty);
            if (newProduct == null)
            {
                MessageBox.Show("Please enter a product into the grid !");
            }
            await productRestService.CreateProductAsync(newProduct);
            await GetProducts();
        }

        private async void UpdateProduct()
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Please select the product you want to update !");
                return;
            }
            await productRestService.UpdateProductAsync(selectedProduct.ProductId, selectedProduct);
            await GetProducts();
        }

        private async Task GetProducts()
        {
            Products = new ObservableCollection<ProductModel>(await productRestService.GetAllProductsAsync());
        }

        #endregion Methods
    }
}