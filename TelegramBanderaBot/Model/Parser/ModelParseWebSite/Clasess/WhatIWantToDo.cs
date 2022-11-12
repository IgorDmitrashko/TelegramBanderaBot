using TelegramBanderaBot.Parser.ModelParseWebSite.Interface;

namespace TelegramBanderaBot.Parser.ModelParseWebSite.Clasess
{
    public class WhatIWantToDo: INumberOFParticipants
    {
        public string UrlSite { get; set; } = $"http://www.boredapi.com/api/activity?participants=";
        public int NumberOfParticipants { get; set; } = 1;
    }
}
