using TelegramBanderaBot.Parser.ModelParseWebSite.Interface;

namespace TelegramBanderaBot.Parser.ModelParseWebSite.Clasess
{
    internal class ExchangeRate: IUrlSite
    {
        public string UrlSite { get; set; } = "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5";
    }
}
