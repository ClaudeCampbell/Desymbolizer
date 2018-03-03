using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
namespace Desymbolizer
{
    class Logic
    {
        static Thread th;

        public static void getLnkInfo()
        {
            ProgressBar bar = FolderData.bar;
            Label fileHappen = FolderData.label;

            string directory = FolderData.folder;

            string[] files = Directory.GetFiles(directory, "*", FolderData.searchLevel);
            bar.Invoke(new Action(() => { bar.Minimum = 1; }));
            bar.Invoke(new Action(() => { bar.Maximum = files.Length; }));
            bar.Invoke(new Action(() => { bar.Value = 1; }));
            bar.Invoke(new Action(() => { bar.Step = 1; }));

            /**
            bar.Minimum = 1;
            bar.Maximum = files.Length;
            bar.Value = 1;
            bar.Step = 1;
            */

            foreach(string file in files)
            {
                //fileHappen.Text = file;

                FileInfo info = new FileInfo(file);

                if (info.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    long targetBytes = getBytes(info);

                    FileMem link = new FileMem();
                    link.path = file;
                    link.bytes = targetBytes;

                    FolderData.lnkinfo.Add(link);

                    FolderData.TotalBytes += targetBytes;
                    FolderData.linkCount++;
                }

                bar.Invoke(new Action(() => { bar.PerformStep(); }));
                //bar.PerformStep();
            }
            //bar.Value = 1;
            bar.Invoke(new Action(() => { bar.Value = 1; }));
            fileHappen.Invoke(new Action(() => { fileHappen.Text = FolderData.linkCount.ToString() + " files. Total link file size: " + FolderData.TotalBytes.ToString() + " bytes / " + (FolderData.TotalBytes / 1024 / 1024).ToString() + " MB"; }));
            //fileHappen.Text = FolderData.linkCount.ToString() + " files. Total link file size: " + FolderData.TotalBytes.ToString() + " bytes / " + (FolderData.TotalBytes / 1024 / 1024).ToString() + " MB";
            unlockButtons();
            FolderData.TotalBytes = 0;
            FolderData.linkCount = 0;
        }
        public static void unlockButtons()
        {
            FolderData.lockButton1.Invoke(new Action(() => { FolderData.lockButton1.Enabled = true; }));
            FolderData.lockButton2.Invoke(new Action(() => { FolderData.lockButton2.Enabled = true; }));
            FolderData.lockButton3.Invoke(new Action(() => { FolderData.lockButton3.Enabled = true; }));
        }
        public static void getLnkInfoThread()
        {
            th = new Thread(getLnkInfo);
            th.Start();
        }

        public static void desymbolizeFilesThread()
        {
            th = new Thread(desymbolizeFiles);
            th.Start();
        }

        public static void symbolizeFilesThread()
        {
            th = new Thread(symbolizeFiles);
            th.Start();
        }

        public static void desymbolizeFiles()
        {
            ProgressBar bar = FolderData.bar;
            Label fileHappen = FolderData.label;

            string directory = FolderData.folder;

            string[] files = Directory.GetFiles(directory, "*", FolderData.searchLevel);

            bar.Invoke(new Action(() => { bar.Minimum = 1; }));
            bar.Invoke(new Action(() => { bar.Maximum = files.Length; }));
            bar.Invoke(new Action(() => { bar.Value = 1; }));
            bar.Invoke(new Action(() => { bar.Step = 1; }));

            fileHappen.Invoke(new Action(() => { fileHappen.Text = "Building restore dictionary"; }));

            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);

                if (info.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    createDictionaryEntry(file);
                    
                }
                bar.Invoke(new Action(() => { bar.PerformStep(); }));

            }
            File.WriteAllText("ReplacedFiles.json", JsonConvert.SerializeObject(FolderData.linkDest));

            bar.Invoke(new Action(() => { bar.Value = 1; }));

            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);

                if (info.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    fileHappen.Invoke(new Action(() => { fileHappen.Text = "Desymbolizing " + file; }));
                    Desymbolize(file);
                    
                }
                bar.Invoke(new Action(() => { bar.PerformStep(); }));
            }
            bar.Invoke(new Action(() => { bar.Value = 1; }));
            fileHappen.Invoke(new Action(() => { fileHappen.Text = "Done"; }));
            MessageBox.Show("All files desymbolized!");
            unlockButtons();
        }

        public static void symbolizeFiles()
        {
            ProgressBar bar = FolderData.bar;
            Label fileHappen = FolderData.label;
            string directory = FolderData.folder;

            string[] files = Directory.GetFiles(directory, "*", FolderData.searchLevel);

            bar.Invoke(new Action(() => { bar.Minimum = 1; }));
            bar.Invoke(new Action(() => { bar.Maximum = files.Length; }));
            bar.Invoke(new Action(() => { bar.Value = 1; }));
            bar.Invoke(new Action(() => { bar.Step = 1; }));

            foreach (string file in files)
            {
                if (FolderData.linkDest.ContainsKey(file))
                {
                    if (File.Exists(file) && File.Exists(FolderData.linkDest[file]))
                    {
                        FileInfo info = new FileInfo(file);

                        if (!info.Attributes.HasFlag(FileAttributes.ReparsePoint))
                        {
                            fileHappen.Invoke(new Action(() => { fileHappen.Text = "Symbolizing " + file; }));

                            Symbolize(file, FolderData.linkDest[file]);
                            
                            
                        }
                        else
                        {
                            FolderData.linkDest.Remove(file);
                        }
                    }
                    else
                    {
                        FolderData.linkDest.Remove(file); //removing anything that may have been deleted
                    }

                }
                bar.Invoke(new Action(() => { bar.PerformStep(); }));
            }
            File.WriteAllText("ReplacedFiles.json", JsonConvert.SerializeObject(FolderData.linkDest));
            MessageBox.Show("Reverted symlinks!");
            fileHappen.Invoke(new Action(() => { fileHappen.Text = "Done"; }));
            bar.Invoke(new Action(() => { bar.Value = 1; }));
            unlockButtons();
        }
        static long getBytes(FileInfo file)
        {
            long length = 0;
            FileStream stream = null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                length = stream.Length;
            }
            catch (IOException)
            {
                return 0;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return length;
        }
        static void createDictionaryEntry(string file)
        {
            //FileStream stream = null;
            try
            {
                //stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                string finalPath = NativeMethods.GetFinalPathName(file);
                if (!FolderData.linkDest.ContainsKey(file))
                {
                    FolderData.linkDest.Add(file, finalPath);
                }
                else
                {
                    FolderData.linkDest[file] = finalPath;
                }
            }
            catch (IOException)
            {
                return;
            }
        }
        static void Desymbolize(string file)
        {
            string finalPath = NativeMethods.GetFinalPathName(file);
            File.Delete(file); //not sure what the behavior will be if i attempt to overwrite-copy to a symlink so im deleting the link for now
            File.Copy(FolderData.linkDest[file], file);
        }

        static void Symbolize(string fileDest, string fileLink)
        {
            File.Delete(fileDest); //removing the file so there is no conflict

            NativeMethods.CreateSymbolicLink(fileDest, fileLink, NativeMethods.SymbolicLink.File);

            FolderData.linkDest.Remove(fileDest);
        }

    }

    public class VirtualModList
    {

    }
    public class VirtualMod
    {

    }
    public class filelink
    {

    }

    public static class NativeMethods
    {
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        private const uint FILE_READ_EA = 0x0008;
        private const uint FILE_FLAG_BACKUP_SEMANTICS = 0x2000000;

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint GetFinalPathNameByHandle(IntPtr hFile, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpszFilePath, uint cchFilePath, uint dwFlags);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateFile(
                [MarshalAs(UnmanagedType.LPTStr)] string filename,
                [MarshalAs(UnmanagedType.U4)] uint access,
                [MarshalAs(UnmanagedType.U4)] FileShare share,
                IntPtr securityAttributes, // optional SECURITY_ATTRIBUTES struct or IntPtr.Zero
                [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
                [MarshalAs(UnmanagedType.U4)] uint flagsAndAttributes,
                IntPtr templateFile);

        [DllImport("kernel32.dll")]
        public static extern bool CreateSymbolicLink(
        string lpSymlinkFileName, string lpTargetFileName, SymbolicLink dwFlags);

        public enum SymbolicLink
        {
            File = 0,
            Directory = 1
        }

        public static string GetFinalPathName(string path)
        {
            var h = CreateFile(path,
                FILE_READ_EA,
                FileShare.ReadWrite | FileShare.Delete,
                IntPtr.Zero,
                FileMode.Open,
                FILE_FLAG_BACKUP_SEMANTICS,
                IntPtr.Zero);
            if (h == INVALID_HANDLE_VALUE)
                throw new Win32Exception();

            try
            {
                var sb = new StringBuilder(1024);
                var res = GetFinalPathNameByHandle(h, sb, 1024, 0);
                if (res == 0)
                    throw new Win32Exception();

                string p = sb.ToString();
                if (p.Contains("\\\\?\\")) //apparently this is returning \\?\ at the begining of the path, I think its happening when links are on a different drive?
                {
                    p = p.Substring(4);
                }

                return p;

            }
            finally
            {
                CloseHandle(h);
            }
        }
    }
}
