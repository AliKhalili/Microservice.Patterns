using BuildingBlocks.Domain;

namespace FileManagement.Tests.Utils
{
    public class FakeValidationNotificationHandler:IValidatorNotificationHandler
    {
        public void HandleError(string error)
        {
           
        }
        public void HandleInfo(string error)
        {
        }

        public void HandleWarning(string error)
        {
        }
    }
}