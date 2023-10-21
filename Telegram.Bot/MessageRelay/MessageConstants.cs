namespace MessageRelay;

public static class  MessageConstants
{
    internal const string GotIt = "got it";
    internal const string ChatIdsNotAllowed = "Error: ChatId is not the same as the intendet id";
    internal const string NoResponseText = "Error: No Response Text";
    internal const string ExceptionSendingMessage = "Error: Exception while sending message";

    internal static string BotClientErrorHandleException(Exception exception, string para)
    {
        return string.Format("Exception error: {0}; {1}", exception, para);
    }
}