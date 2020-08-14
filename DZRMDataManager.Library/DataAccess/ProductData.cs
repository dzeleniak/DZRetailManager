using DZRMDataManager.Library.Internal.DataAccess;
using DZRMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZRMDataManager.Library.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetAllProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "DZRMData");

            return output;
        }

        public ProductModel GetProductById(int productId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = productId }, "DZRMData").FirstOrDefault();

            return output;
        }
    }
}
