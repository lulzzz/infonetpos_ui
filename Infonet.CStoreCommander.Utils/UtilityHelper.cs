namespace Infonet.CStoreCommander.Utils
{
    public class UtilityHelper
    {
        public string LanguageToLanguageTag(string language)
        {
            switch (language)
            {
                case "English": return "en-US";
                case "French": return "fr-CA";
                case "Arabic": return "ar";
                default:
                    return string.Empty;
            }
        }
    }
}
