using DZRMDesktopUI.Models;
using System.Threading.Tasks;

namespace DZRMDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}