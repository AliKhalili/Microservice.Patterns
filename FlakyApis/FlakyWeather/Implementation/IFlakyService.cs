using System.Threading.Tasks;

namespace FlakyApi.Implementation
{
    public interface IFlakyService
    {
        Task<bool> RetrieveSystemStatus();
    }
}