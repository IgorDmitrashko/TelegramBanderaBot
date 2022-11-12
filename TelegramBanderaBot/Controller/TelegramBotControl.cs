using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBanderaBot.Model.TelegramBot;
using TelegramBanderaBot.TelegramBot;

namespace TelegramBot_Bandera228Bot.Telegram_Bot
{
    internal class TelegramBotControl
    {
        private const string token = "5611167462:AAG1tlonjISrqhW3VUyV0jZ9EBN3bhWJkH8";
        private readonly TelegramBotMenu _telegramBotMenu;
        private readonly MessageHandler _messageHandler;
        private readonly InlineButtonHandler _buttoHnandler;
        private readonly TelegramBotClient client;

        private readonly ReceiverOptions receiver = new() { AllowedUpdates={ } };

        public TelegramBotControl() {
            _telegramBotMenu=new TelegramBotMenu();
            _messageHandler=new MessageHandler();
            client=new TelegramBotClient(token);
            _buttoHnandler=new InlineButtonHandler();

            MessageHandler.OnSendMassage+=SendMessage;
            client.StartReceiving(Update, Error, receiver, cancellationToken: new CancellationToken());
        }

        private async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token) {
            switch(update.Type)
            {
                case UpdateType.Unknown:
                    break;
                case UpdateType.Message:
                    _messageHandler.ProcessMessage(botClient, update, token);
                    break;
                case UpdateType.InlineQuery:
                    break;
                case UpdateType.ChosenInlineResult:
                    break;
                case UpdateType.CallbackQuery:
                    _buttoHnandler.ProcessButtonCallbackQuery(botClient, update, token);
                    break;
                case UpdateType.EditedMessage:
                    break;
                case UpdateType.ChannelPost:
                    break;
                case UpdateType.EditedChannelPost:
                    break;
                case UpdateType.ShippingQuery:
                    break;
                case UpdateType.PreCheckoutQuery:
                    break;
                case UpdateType.Poll:
                    break;
                case UpdateType.PollAnswer:
                    break;
                case UpdateType.MyChatMember:
                    break;
                case UpdateType.ChatMember:
                    break;
                case UpdateType.ChatJoinRequest:
                    break;
                default:
                    break;
            }
        }

        private Task Error(ITelegramBotClient botClient, Exception ex, CancellationToken token) {

            string erorrMessage = ex switch {
                ApiRequestException api
                => $"Ошибка телеграмм АПИ:\n{api.ErrorCode}\n{api.Message}",
                _ => ex.ToString()
            };
            Console.WriteLine(erorrMessage);
            client.StartReceiving(Update, Error, receiver, cancellationToken: new CancellationToken());
            return Task.CompletedTask;
        }

        private async void SendMessage(ITelegramBotClient botClient, Message message, string report) {
            if(botClient!=null&&message!=null&&report!=null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"@{message.From.FirstName} {message.From.LastName}\n{report}");
            }
        }

        async private Task SendMessageAnimation(ITelegramBotClient botClient, Update? chatId, CancellationToken cancellationToken) {
            _=await botClient.SendDocumentAsync(
                      chatId: chatId.Message.Chat.Id,
                      document: "https://index.minfin.com.ua/minfin/russian-invading/casualties/img/24.jpg",
                      caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
                      parseMode: ParseMode.Html,
                      replyMarkup: _telegramBotMenu.GetButtonsMenu(),
                      cancellationToken: cancellationToken);
        }
    }
}