using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBanderaBot.Model.TelegramBot
{
    public class TelegramBotMenu
    {


        private const string _textButtonWater = "Water";
        private const string _textButtonWeather = "Weather";
        private const string _textButtonOrki = "Orki";
        private const string _textButtonRate = "Rate";
        private const string _textButtonHoroscope = "Horoscope";

        private string[]? _textInLineButtons = { "aries", "taurus", "gemini", "cancer", "leo", "virgo", "libra", "scorpio", "sagittarius", "capricorn", "aquarius", "pisces", };

        public ReplyKeyboardMarkup GetButtonsMenu() {

            ReplyKeyboardMarkup markup = new(new[] {
                new KeyboardButton[] { _textButtonWater, _textButtonWeather },
                new KeyboardButton[] { _textButtonOrki, _textButtonRate },
                new KeyboardButton[] { _textButtonHoroscope }
            }) { ResizeKeyboard=true };
            return markup;
        }

        public InlineKeyboardMarkup GetButtonsHoroscope() {
            InlineKeyboardMarkup markup =
                new(new[] {
                new InlineKeyboardButton[] { _textInLineButtons[0], _textInLineButtons[1] },
                new InlineKeyboardButton[] { _textInLineButtons[2], _textInLineButtons[3] },
                new InlineKeyboardButton[] { _textInLineButtons[4], _textInLineButtons[5] },
                new InlineKeyboardButton[] { _textInLineButtons[6], _textInLineButtons[7] },
                new InlineKeyboardButton[] { _textInLineButtons[8], _textInLineButtons[9] },
                new InlineKeyboardButton[] { _textInLineButtons[10], _textInLineButtons[11] },
            }) {
                };

            return markup;
        }


        public ReplyKeyboardMarkup GetButtonsConverterPdf() {
            ReplyKeyboardMarkup markup = new(new[] {
                new KeyboardButton[] { "Convert Word to PDF", "Convert PDF to Word" },
            }) { ResizeKeyboard=true };
            return markup;
        }
    }
}
