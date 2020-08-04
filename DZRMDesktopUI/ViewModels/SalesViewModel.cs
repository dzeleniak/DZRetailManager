using Caliburn.Micro;
using DZRMDesktopUI.Library.Models;
using DZRMDesktopUI.Library.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZRMDesktopUI.Library.Helpers;

namespace DZRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        IProductEndpoint _productEndpoint;
        IConfigHelper _configHelper;
        public SalesViewModel(IProductEndpoint ProductEndpoint, IConfigHelper configHelper)
        {
            _productEndpoint = ProductEndpoint;
            _configHelper = configHelper;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var productList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }

        private BindingList<ProductModel> _products;

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set 
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            { 
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }


        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();

        public BindingList<CartItemModel> Cart
        {
            get { return _cart; }
            set 
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }


        private int _itemQuantity = 1;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set 
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public string SubTotal
        {
            get
            {                
                return CalculateSubTotal().ToString("C");                
            }


        }

        private decimal CalculateSubTotal()
        {
            return Cart.ToList()
                    .Sum(x => x.Product.RetailPrice * x.QuantityInCart);
        }
        public string Tax
        {
            get
            {
                return CalculateTax().ToString("C");

            }
        }

        private decimal CalculateTax()
        {
            decimal taxRate = _configHelper.GetTaxRate();
            decimal taxAmt = 0;

            taxAmt = Cart
                .Where(x => x.Product.IsTaxable)
                .Sum(x => x.Product.RetailPrice * x.QuantityInCart * taxRate);

            return taxAmt;

            //foreach(var item in Cart)
            //{
            //    if (item.Product.IsTaxable)
            //    {
            //        taxAmt += ((item.Product.RetailPrice * item.QuantityInCart) * taxRate);
            //    }
            //}
        }

        public string Total
        {
            get
            {
                var total = CalculateSubTotal() + CalculateTax();
                return total.ToString("C");
            }
        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                if (SelectedProduct?.QuantityInStock >= ItemQuantity && ItemQuantity > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public void AddToCart()
        {
            CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);
            if(existingItem != null)
            {
                existingItem.QuantityInCart += ItemQuantity;
                Cart.Remove(existingItem);
                Cart.Add(existingItem);
            }
            else
            {
                CartItemModel item = new CartItemModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };

                Cart.Add(item);
            }
            
            
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => Cart);
            NotifyOfPropertyChange(() => SubTotal); 
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);



        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                //Make sure something is selected

                return output;
            }
        }

        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
        }

        public bool CanCheckout
        {
            get
            {
                bool output = false;

                //Make sure something is in the cart

                return output;
            }
        }

        public void Checkout()
        {

        }

    }
}
