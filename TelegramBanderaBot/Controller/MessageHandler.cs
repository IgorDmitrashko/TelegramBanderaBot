using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using TelegramBanderaBot.Model.TelegramBot;
using TelegramBanderaBot.Model.Translater;
using TelegramBanderaBot.Parser.ModelParseWebSite.Clasess;
using TelegramBanderaBot.Weather;
using TelegramBot_Bandera228Bot.Parser;
using File = System.IO.File;

namespace TelegramBanderaBot.TelegramBot
{
    public class MessageHandler
    {
        private readonly ControlWeather _weather;
        private readonly ParseWebSite _webSite;
        private readonly TelegramBotMenu _telegramBotMenu;

        private readonly MinFin _minFin = new();
        private readonly ExchangeRate _exchangeRate = new();
        private readonly WhatIWantToDo _whatIWantToDo = new();
        private readonly ReservoirsKyivRegion _waterKyivRegion = new();
        private readonly LovePdfConverter lovePdf = new();
        private readonly DirectoryTelegramBot dirTelegramBot = new();


        private string _theNameOfDownloadedFileFromTelegram;
        string destinationFilePath;

        public static Action<ITelegramBotClient, Message, string>? OnSendMassage;

        public MessageHandler() {
            _telegramBotMenu=new TelegramBotMenu();
            _webSite=new ParseWebSite();
            _weather=new ControlWeather();
        }
        public async void ProcessMessage(ITelegramBotClient botClient, Update update, CancellationToken token) {
            var message = update.Message;
            if(message!=null)
            {
                if(message.Text is not null&&message.Text.ToLower().Contains("hi"))
                {
                    OnSendMassage?.Invoke(botClient, message, $"Привіт");
                }
                else if(message.Text is not null&&message.Text.ToLower().Contains("weather"))
                {
                    OnSendMassage?.Invoke(botClient, message, $"Погода в {_weather.GetWeather().Name}\nТемпература {_weather.GetWeather().Main.Temp}\n");
                    return;
                }
                else if(message.Text is not null&&message.Text.ToLower().Contains("orki"))
                {
                    OnSendMassage?.Invoke(botClient, message, $"{_webSite.GetContentMinFin(_minFin)}");
                }
                else if(message.Text is not null&&message.Text.ToLower().Contains("water"))
                {
                    OnSendMassage?.Invoke(botClient, message, $"Место*Вода*Воздух\n{_webSite.GetContentWaterTemp(_waterKyivRegion)}");
                }
                else if(message.Text is not null&&message.Text.ToLower().Contains("button"))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "У тебя появились кнопки", replyMarkup: _telegramBotMenu.GetButtonsMenu());
                    return;
                }
                else if(message.Text is not null&&message.Text.ToLower().Contains("rate"))
                {
                    if(_webSite.GetContentExchangeRate(_exchangeRate).Count>=2)
                    {
                        OnSendMassage?.Invoke(botClient, message, $"USD {_webSite.GetContentExchangeRate(_exchangeRate)[0].Buy}\nEUR {_webSite.GetContentExchangeRate(_exchangeRate)[1].Buy}");
                    }
                    else
                    {
                        OnSendMassage?.Invoke(botClient, message, $"error");
                    }
                }
                else if(message.Text is not null&&message.Text.ToLower().Contains("horoscope"))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Выбери свой знак зодиака", replyMarkup: _telegramBotMenu.GetButtonsHoroscope());
                }
                else if(message.Text is not null&&message.Text.ToLower().Contains("what"))
                {
                    OnSendMassage?.Invoke(botClient, message, $"{_webSite.GetContentWhatWantToDo(_whatIWantToDo)}");
                }
                else if(message.Document is not null)
                {
                    try
                    {
                        var fileId = update.Message.Document.FileId;
                        var fileInfo = await botClient.GetFileAsync(fileId);
                        var filePath = fileInfo.FilePath;

                        dirTelegramBot.CreateFolderTelegramBot();

                        destinationFilePath=$@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\TelegramBotFiles\{message.Document.FileName}";
                        _theNameOfDownloadedFileFromTelegram=message.Document.FileName;
                        await using FileStream fileStream = File.OpenWrite(destinationFilePath);
                        await botClient.DownloadFileAsync(filePath, fileStream);
                        fileStream.Close();

                        var messageTelegram = await botClient.SendTextMessageAsync(message.Chat.Id, "Работаю над доком", replyToMessageId: message.MessageId);

                        if(message.Document.FileName.ToLower().Contains(".pdf"))
                        {
                            string pathNamePdfToJpg = lovePdf.GetPdfToJpg(destinationFilePath, _theNameOfDownloadedFileFromTelegram);
                            await using Stream streamPdfToJpg = File.OpenRead(pathNamePdfToJpg);
                            await botClient.SendDocumentAsync(message.Chat.Id, new InputOnlineFile(streamPdfToJpg, pathNamePdfToJpg), replyToMessageId: message.MessageId);
                           await botClient.DeleteMessageAsync(messageTelegram.Chat.Id, messageTelegram.MessageId);
                        }
                        else if(message.Document.FileName.ToLower().Contains(".pdf")||message.Document.FileName.ToLower().Contains(".xls"))
                        {
                            string pathName = lovePdf.GetOfficeToPdf(destinationFilePath, _theNameOfDownloadedFileFromTelegram);
                            await using Stream stream = File.OpenRead(pathName);
                            await botClient.SendDocumentAsync(message.Chat.Id, new InputOnlineFile(stream, pathName), replyToMessageId: message.MessageId);
                            await botClient.DeleteMessageAsync(messageTelegram.Chat.Id, messageTelegram.MessageId);
                        }
                    }
                    catch(Exception e)
                    {
                        OnSendMassage?.Invoke(botClient, message, $"error {e.Message}");
                    }
                }
                else if(message.Text is not null&&message.Text=="Convert Word to PDF")
                {
                    try
                    {

                        await using Stream stream = File.OpenRead(lovePdf.GetOfficeToPdf(destinationFilePath, _theNameOfDownloadedFileFromTelegram));
                        await botClient.SendDocumentAsync(message.Chat.Id, new InputOnlineFile(stream, _theNameOfDownloadedFileFromTelegram));
                    }

                    catch(Exception e)
                    {
                        OnSendMassage?.Invoke(botClient, message, $"error {e.Message}");
                    }
                }
            }
        }
    }
}
