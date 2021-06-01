
namespace Infrastructures.Domain
{
    public interface IValidatorNotificationHandler
    {
        public void HandleError(string error);
    }
}
