using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace Desymbolizer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FolderData.searchLevel = SearchOption.AllDirectories;

            if (File.Exists("ReplacedFiles.json"))
            {
                FolderData.linkDest = JsonConvert.DeserializeObject<Dictionary<string, string>>
                    (File.ReadAllText("ReplacedFiles.json"));
            }

            Application.Run(new Form1());
        }
    }
}
