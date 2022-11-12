using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBanderaBot.Model.Translater;
using TelegramBot_Bandera228Bot.Parser;

namespace TelegramBanderaBot.TelegramBot
{
    public class InlineButtonHandler
    {
        public bool _isHoroscope;
        private ParseWebSite _webSite;
        private LovePdfConverter lovePdf;
        public InlineButtonHandler() {
            _webSite=new ParseWebSite();
            lovePdf=new LovePdfConverter();
        }

        public void ProcessButtonCallbackQuery(ITelegramBotClient botClient, Update update, CancellationToken token) {
            try
            {
                var callQData = update.CallbackQuery.Data;
                if(callQData=="ries"||callQData=="taurus"
                    ||callQData=="gemini"||callQData=="cancer"
                    ||callQData=="leo"||callQData=="virgo"
                    ||callQData=="libra"||callQData=="scorpio"
                    ||callQData=="sagittarius"||callQData=="capricorn"
                    ||callQData=="aquarius"||callQData=="pisces")
                {
                    MessageHandler.OnSendMassage?.Invoke(botClient, update.CallbackQuery.Message, $"Гороскоп: {update.CallbackQuery.Data}\n{_webSite.GetContentHoroscope(update.CallbackQuery.Data)}");
                }
                else
                {
                    MessageHandler.OnSendMassage?.Invoke(botClient, update.CallbackQuery.Message, $"error:");
                }
            }
            catch(Exception)
            {

                throw;
            }
        }
    }
}
