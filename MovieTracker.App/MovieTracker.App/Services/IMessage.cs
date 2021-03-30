namespace MovieTracker.App.Services
{
    public interface IMessage
    {
        void ShortAlertMessage(string message);
        void LongAlertMessage(string message);
    }
}
