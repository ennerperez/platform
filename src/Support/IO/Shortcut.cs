﻿#if !PORTABLE

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Platform.Support.IO
{
    [ComImport]
    [Guid("00021401-0000-0000-C000-000000000046")]
    public class ShellLink
    {
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("000214F9-0000-0000-C000-000000000046")]
    public interface IShellLink
    {
        void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);

        void GetIDList(out IntPtr ppidl);

        void SetIDList(IntPtr pidl);

        void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);

        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

        void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);

        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

        void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);

        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

        void GetHotkey(out short pwHotkey);

        void SetHotkey(short wHotkey);

        void GetShowCmd(out int piShowCmd);

        void SetShowCmd(int iShowCmd);

        void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);

        void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

        void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);

        void Resolve(IntPtr hwnd, int fFlags);

        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
    }

    public class Shortcut : ShellLink
    {
        public static string Create(string fileName, string targetPath, string name = "", string description = "", string args = "", string workDir = "")
        {
            if (string.IsNullOrEmpty(name))
                name = Path.GetFileNameWithoutExtension(fileName);

            var link = (IShellLink)new ShellLink();

            link.SetPath(fileName);
            link.SetDescription(description ?? name);
            link.SetWorkingDirectory(workDir ?? new FileInfo(fileName).Directory.FullName);
            link.SetArguments(args);

#if NETFX_40
            var result = Path.Combine(targetPath, name + ".lnk");
#else
            var result = Path.Combine(targetPath, $"{name}.lnk");
#endif

            var file = (IPersistFile)link;
            file.Save(result, false);

            return result;
        }

        public static string CreateOnDesktop(string fileName, string name = "", string description = "", string args = "", string workDir = "")
        {
            var targetPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            return Create(fileName, targetPath, name, description, args, workDir);
        }

        public static string CreateOnPrograms(string fileName, string folder = "", string name = "", string description = "", string args = "", string workDir = "")
        {
            var targetPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), folder);
            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            return Create(fileName, targetPath, name, description, args, workDir);
        }
    }
}

#endif