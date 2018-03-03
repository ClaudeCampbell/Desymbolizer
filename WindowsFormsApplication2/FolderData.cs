using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Desymbolizer
{
    public static class FolderData
    {
        public static ProgressBar bar {get; set;}

        public static Label label { get; set; }

        public static string folder { get; set; }

        public static long TotalBytes { get; set; }

        public static int linkCount { get; set; }
        
        public static Button lockButton1 { get; set; }

        public static Button lockButton2 { get; set; }

        public static Button lockButton3 { get; set; }

        public static SearchOption searchLevel { get; set; }

        public static List<FileMem> lnkinfo = new List<FileMem>();

        public static Dictionary<string, string> linkDest = new Dictionary<string, string>();
    }

    public class FileMem
    {
        public string path { get; set; }

        public long bytes { get; set; }
    }
}
