using TelegramBanderaBot.Parser.ModelParseWebSite.Interface;

namespace TelegramBanderaBot.Parser.ModelParseWebSite.Clasess
{
    public class MinFin: IParseWebSite
    {
        public string UrlSite { get; set; } = "https://index.minfin.com.ua/russian-invading/casualties/";
        public string PropForSearch { get; } = "div.casualties";
    }
}
