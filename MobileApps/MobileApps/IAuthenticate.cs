using System.Threading.Tasks;

namespace MobileApps
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();
    }
}
