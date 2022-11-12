using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBanderaBot.Model.TelegramBot
{
    public class DirectoryTelegramBot
    {
        private DirectoryInfo Create(string path, string subpath) {
            return new DirectoryInfo(path);
        }

        public void CreateFolderTelegramBot() {

            DirectoryInfo dirInfo = Create(path: $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}", subpath: @"TelegramBotFiles");
            string subpath = @"TelegramBotFiles";
            if(!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(subpath);
        }

        public string CreateFolderDownLoad() {
            string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\TelegramBotFiles";
            string subpath = @"Download";
            DirectoryInfo dirInfo = Create(path: $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\TelegramBotFiles", subpath: subpath);
            if(!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(subpath);
            return path;
        }

    }
}
