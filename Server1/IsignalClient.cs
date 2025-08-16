namespace Server1
{
    public interface IsignalClient
    {
        Task ReceiveMessage(string user, string message, MessageData messageData);
    }
}
