using DZRMDataManager.Library.Internal.DataAccess;
using DZRMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DZRMDataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            //TODO: Make this SOLID/DRY/Better
            //Fill in models we will save to database
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            ProductData products = new ProductData();
            var taxRate = ConfigHelper.GetTaxRate();

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                //Get information about this product
                var productInfo = products.GetProductById(detail.ProductId);

                if(productInfo == null)
                {
                    throw new Exception($"The product ID of {item.ProductId} could not be found in the database.");
                }

                detail.PurchasedPrice = (productInfo.RetailPrice * detail.Quantity);

                if (productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasedPrice * taxRate);
                }

                details.Add(detail);
            }

            //Create the sale model
            SaleDBModel sale = new SaleDBModel
            {
                Subtotal = details.Sum(x => x.PurchasedPrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };

            sale.Total = sale.Subtotal + sale.Tax;

            // save the sale model
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData("dbo.spSale_Insert", sale, "DZRMData");

            //Get id from sale model
            sale.Id = sql.LoadData<int, dynamic>("spSale_Lookup", 
                new { CashierId = sale.CashierId, 
                    SaleDate = sale.SaleDate },
                    "DZRMData").FirstOrDefault();


            //Finish filling in sale detail models

            //save sale detail models
            foreach (var item in details)
            {
                item.SaleId = sale.Id;
                sql.SaveData("dbo.spSaleDetail_Insert", item, "DZRMData");
            }


        }

        //public List<ProductModel> GetAllProducts()
        //{
        //    SqlDataAccess sql = new SqlDataAccess();

        //    var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "DZRMData");

        //    return output;
        //}
    }
}
