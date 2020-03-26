namespace Infonet.CStoreCommander.Utils
{
    public class AlertMessageFormatter
    {
        public MessageStyle CreateMessage(string messageString)
        {
            if (messageString == null)
            {
                return new MessageStyle
                {
                    Message = string.Empty,
                    Title = string.Empty
                };
            }

            var title = string.Empty;
            if (messageString.IndexOf('|') > -1)
            {
                var msgLines = messageString.Split('|');
                messageString = msgLines.ToString();
            }

            var titleIndex = messageString.IndexOf('~');
            if (titleIndex >= 0)
            {
                title = messageString.Substring(titleIndex + 1);
                messageString = messageString.Substring(0, titleIndex);
            }
            else
            {
                title = "";
            }

            messageString = messageString.Replace("_", "\n");

            return new MessageStyle
            {
                Message = messageString.Trim(),
                Title = title.Trim()
            };
        }
    }

    public class MessageStyle
    {
        public string Message { get; set; }

        public string Title { get; set; }
    }
}
