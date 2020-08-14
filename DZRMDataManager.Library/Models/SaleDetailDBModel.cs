using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZRMDataManager.Library.Models
{
    public class SaleDetailDBModel
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasedPrice { get; set; }
        public decimal Tax { get; set; }

    }
}
