using DZRMDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DZRMDesktopUI.Library.API
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}