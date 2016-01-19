using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace theOblang_Global.PageHelper.Comm
{
    public class PathToDownloads
    {
        public PathToDownloads()
        {
            // TODO: Complete member initialization
        }
        public bool pathToFile(String Filename)
        {
            bool result = false;
            string downloadsPath = TestKnownFolders.GetPathToX(TestKnownFolder.Downloads);
            DirectoryInfo di = new DirectoryInfo(downloadsPath);
            FileInfo[] TXTFiles = di.GetFiles(Filename);
            if (TXTFiles.Length == 0)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        public int fileCount(String Filename)
        {
            string downloadsPath = TestKnownFolders.GetPathToX(TestKnownFolder.Downloads);
            DirectoryInfo di = new DirectoryInfo(downloadsPath);
            FileInfo[] TXTFiles = di.GetFiles(Filename);
            return TXTFiles.Length;
        }
    }

     public static class TestKnownFolders
    {
        private static string[] _knownFolderGuids = new string[]
    {
        "{374DE290-123F-4565-9164-39C4925E467B}", // Downloads
    };
      
        public static string GetPathToX(TestKnownFolder knownFolder)
        {
            return GetPathToX(knownFolder, false);
        }

        public static string GetPathToX(TestKnownFolder knownFolder, bool defaultUser)
        {
            return GetPathToX(knownFolder, KnownFolderFlags.DontVerify, defaultUser);
        }

        private static string GetPathToX(TestKnownFolder knownFolder, KnownFolderFlags flags,
            bool defaultUser)
        {
            IntPtr outPath;
            int result = SHGetKnownFolderPath(new Guid(_knownFolderGuids[(int)knownFolder]),
                (uint)flags, new IntPtr(defaultUser ? -1 : 0), out outPath);
            if (result >= 0)
            {
                return Marshal.PtrToStringUni(outPath);
            }
            else
            {
                throw new ExternalException("Unable to retrieve the known folder path. It may not "
                    + "be available on this system.", result);
            }
        }

        [DllImport("Shell32.dll")]
        private static extern int SHGetKnownFolderPath(
            [MarshalAs(UnmanagedType.LPStruct)]Guid rfid, uint dwFlags, IntPtr hToken,
            out IntPtr ppszPath);

        [Flags]
        private enum KnownFolderFlags : uint
        {
            DontVerify = 0x00004000,
        }
    }

    public enum TestKnownFolder
    {
        Downloads,
    }
     
}