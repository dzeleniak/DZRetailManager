using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DZRMDesktopUI.Library.Models
{
    public class ProductModel
    {
        /// <summary>
        /// Unique ID for given product
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of a product
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Information about the product
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Price that the item is sold for to a customer
        /// </summary>
        public decimal RetailPrice { get; set; }

        /// <summary>
        /// Number of items contained in stock
        /// </summary>
        public int QuantityInStock { get; set; }
    }
}
