using System.Threading.Tasks;

namespace FlakyApi.Implementations
{
    public interface IService
    {
        Task<DefaultResponse> DoSomething();
    }
}