
using LovePdf.Core;
using LovePdf.Model.Task;

using LovePdf.Model.TaskParams;
using System.Text.RegularExpressions;
using TelegramBanderaBot.Model.TelegramBot;

namespace TelegramBanderaBot.Model.Translater
{
    public class LovePdfConverter
    {
        private string _publicKey = "project_public_edb2828586f50c19744cb0477a1169e1_90img5cfbddc72622bcbd7e1a92e24659690d";
        private string _privatKey = "secret_key_91c2c9ba7298f5fc83a4160285945dc9_inVMIb8cd70805726029c527288d2c4db4fde";

        private const string subpath = @"Download";
        private string _downloadPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\TelegramBotFiles\Download";

        private DirectoryTelegramBot dirBot = new();

        private LovePdfApi ConnectToLovePdf() {
            return new LovePdfApi(_publicKey, _privatKey);

        }

        private T CreateTask<T>() where T : LovePdfTask {
            return ConnectToLovePdf().CreateTask<T>();
        }

        public string GetOfficeToPdf(string pathToDoc, string filewordName) {
            try
            {
                var taskConvertOffice = CreateTask<OfficeToPdfTask>();
                taskConvertOffice.AddFile(pathToDoc);
                taskConvertOffice.Process();

                string regexPath = new Regex(@"\.((doc)|(xls))(x)?", RegexOptions.IgnoreCase).Replace(filewordName, ".pdf");
                taskConvertOffice.DownloadFile($@"{dirBot.CreateFolderDownLoad()}\{subpath}");
                return @$"{_downloadPath}\{regexPath}";
            }
            catch(Exception e)
            {
                return $"error {e.Message}";
            }
        }

        public string GetPdfToJpg(string pathToDoc, string filewordName) {
            try
            {
                var taskConvertOffice = CreateTask<PdfToJpgTask>();
                taskConvertOffice.AddFile(pathToDoc);
                taskConvertOffice.Process();

                string regexPath = new Regex(@"\.(pdf)?", RegexOptions.IgnoreCase).Replace(filewordName, ".jpg");
                taskConvertOffice.DownloadFile($@"{dirBot.CreateFolderDownLoad()}\{subpath}");
                return @$"{_downloadPath}\{regexPath}";
            }
            catch(Exception e)
            {
                return $"error {e.Message}";
            }
        }
    }
}
