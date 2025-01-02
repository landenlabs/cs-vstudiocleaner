using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace VStudioCleaner_ns
{
    /// <summary>
    /// Delete file or directory by placing it in recycle bin so it can undone.
    /// 
    /// A deleted code example can be found:
    /// http://social.msdn.microsoft.com/Forums/hu-HU/netfxbcl/thread/ce1e8a4a-dd6b-4add-84d1-95faa3d13404
    /// </summary>
    class RecycleFile
    {
        public enum FO_Func : uint
        {
            FO_MOVE = 0x0001,
            FO_COPY = 0x0002,
            FO_DELETE = 0x0003,
            FO_RENAME = 0x0004,
        }

#if WIN64
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
#else
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
#endif
        struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public FO_Func wFunc;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pFrom;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pTo;
            public UInt16 fFlags;
            // [MarshalAs(UnmanagedType.Bool)]
            public Int32 fAnyOperationsAborted;
            public IntPtr hNameMappings;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszProgressTitle;

        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        static extern int SHFileOperation([In, Out] ref SHFILEOPSTRUCT lpFileOp);

        const int FOF_ALLOWUNDO = 0x40; 
        const int FOF_NOCONFIRMATION = 0x10;    //Don't prompt the user.; 
        const int FOF_NOERRORUI = 0x0400;
        const int FOF_NORECURSEREPARSE = 0x8000;
        const int FOF_SILENT = 0x0004;


        /// <summary>
        ///     Delete file or directoy by moving it to the recycle bin.
        ///     return true if successful.
        ///     THROW - can throw exception.
        /// </summary>
        static public void DeleteFile(string path)
        {
            SHFILEOPSTRUCT shf = new SHFILEOPSTRUCT();
            shf.wFunc = FO_Func.FO_DELETE;
            shf.hwnd = IntPtr.Zero;
            shf.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION | FOF_SILENT | FOF_NORECURSEREPARSE | FOF_NOERRORUI;
            shf.pFrom = path + '\0' + '\0';
            shf.pTo = "";
            shf.fAnyOperationsAborted = 0;  //  false;
            shf.hNameMappings = IntPtr.Zero;

            int zeroOk = SHFileOperation(ref shf);

            if (zeroOk != 0)
            {
                string errMsg = string.Format("Delete error {0}", zeroOk);
                switch (zeroOk)
                {
                case 32:
                    errMsg = "Delete failed, file is in use by another process";
                    break;
                case 124:   // Invalid path or is it really directory not empty.
                    break;
                default:
                    break;
                }

                throw new FileNotFoundException(errMsg, path);
            }
        }
    }
}
