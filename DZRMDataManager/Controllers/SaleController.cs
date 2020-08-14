using DZRMDataManager.Library.DataAccess;
using DZRMDataManager.Library.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DZRMDataManager.Controllers
{

    [Authorize]
    public class SaleController : ApiController
    {

        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();
            string userId = RequestContext.Principal.Identity.GetUserId();
            data.SaveSale(sale, userId);
        }

        //[HttpGet]
        //public List<ProductModel> Get()
        //{
        //    ProductData data = new ProductData();
        //    return data.GetAllProducts();
        //}
    }
}