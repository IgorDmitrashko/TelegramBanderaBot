using TelegramBanderaBot.Parser.ModelParseWebSite.Interface;

namespace TelegramBanderaBot.Parser.ModelParseWebSite.Clasess
{
    public class ReservoirsKyivRegion: IParseWebSite
    {
        public string UrlSite { get; set; } = "https://seatemperature.ru/country/ukraine/kievskaya-oblast";
        public string PropForSearch { get; } = "tr#trr1";
    }
}
