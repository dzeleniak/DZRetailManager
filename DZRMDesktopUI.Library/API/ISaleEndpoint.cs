using DZRMDesktopUI.Library.Models;
using System.Threading.Tasks;

namespace DZRMDesktopUI.Library.API
{
    public interface ISaleEndpoint
    {
        Task PostSale(SaleModel sale);
    }
}