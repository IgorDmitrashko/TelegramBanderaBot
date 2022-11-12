using TelegramBanderaBot.Parser.ModelParseWebSite.Interface;

namespace TelegramBanderaBot.Parser.ModelParseWebSite.Clasess
{
    public class Horoscope: IUrlSite
    {
        public string UrlSite { get; set; } = "https://ignio.com/r/export/utf/xml/daily/com.xml";
    }
}
