
namespace BuildingBlocks.Domain
{
    public interface IValidatorNotificationHandler
    {
        public void HandleError(string error);
        public void HandleInfo(string error);
        public void HandleWarning(string error);
    }
}
