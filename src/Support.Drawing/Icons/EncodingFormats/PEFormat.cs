using Platform.Support.Drawing.Icons.Exceptions;
using Platform.Support.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Drawing.Icons.EncodingFormats
{
    internal class PEFormat : ILibraryFormat
    {
        public bool IsRecognizedFormat(Stream stream)
        {
            stream.Position = 0L;
            try
            {
                IMAGE_DOS_HEADER image_DOS_HEADER = new IMAGE_DOS_HEADER(stream);
                if (image_DOS_HEADER.e_magic != 23117)
                {
                    return false;
                }
                stream.Seek((long)((ulong)image_DOS_HEADER.e_lfanew), SeekOrigin.Begin);
                IMAGE_NT_HEADERS image_NT_HEADERS = new IMAGE_NT_HEADERS(stream);
                if (image_NT_HEADERS.Signature != 17744u)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        public unsafe MultiIcon Load(Stream stream)
        {
            string text = null;
            IntPtr intPtr = IntPtr.Zero;
            MultiIcon result;
            try
            {
                stream.Position = 0L;
                text = Path.GetTempFileName();
                FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write);
                byte[] array = new byte[stream.Length];
                stream.Read(array, 0, array.Length);
                fileStream.Write(array, 0, array.Length);
                fileStream.Close();
                intPtr = Kernel32.LoadLibraryEx(text, IntPtr.Zero, LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE);
                if (intPtr == IntPtr.Zero)
                {
                    throw new InvalidFileException();
                }
                List<string> list;
                lock (typeof(PEFormat))
                {
                    PEFormat.mIconsIDs = new List<string>();
                    bool flag = Kernel32.EnumResourceNames(intPtr, (IntPtr)14L, new Kernel32.EnumResNameProc(PEFormat.EnumResNameProc), IntPtr.Zero);
                    list = new List<string>(PEFormat.mIconsIDs);
                }
                MultiIcon multiIcon = new MultiIcon();
                for (int i = 0; i < list.Count; i++)
                {
                    string text2 = list[i];
                    IntPtr intPtr2 = IntPtr.Zero;
                    if (Kernel32.IS_INTRESOURCE(text2))
                    {
                        intPtr2 = Kernel32.FindResource(intPtr, int.Parse(text2), (IntPtr)14L);
                    }
                    else
                    {
                        intPtr2 = Kernel32.FindResource(intPtr, text2, (IntPtr)14L);
                    }
                    if (intPtr2 == IntPtr.Zero)
                    {
                        throw new InvalidFileException();
                    }
                    IntPtr intPtr3 = Kernel32.LoadResource(intPtr, intPtr2);
                    if (intPtr3 == IntPtr.Zero)
                    {
                        throw new InvalidFileException();
                    }
                    MEMICONDIR* ptr = (MEMICONDIR*)((void*)Kernel32.LockResource(intPtr3));
                    if (ptr->wCount != 0)
                    {
                        MEMICONDIRENTRY* ptr2 = &ptr->arEntries;
                        SingleIcon singleIcon = new SingleIcon(text2);
                        for (int j = 0; j < (int)ptr->wCount; j++)
                        {
                            IntPtr intPtr4 = Kernel32.FindResource(intPtr, (IntPtr)((int)ptr2[j].wId), (IntPtr)3L);
                            if (intPtr4 == IntPtr.Zero)
                            {
                                throw new InvalidFileException();
                            }
                            IntPtr intPtr5 = Kernel32.LoadResource(intPtr, intPtr4);
                            if (intPtr5 == IntPtr.Zero)
                            {
                                throw new InvalidFileException();
                            }
                            IntPtr intPtr6 = Kernel32.LockResource(intPtr5);
                            if (intPtr6 == IntPtr.Zero)
                            {
                                throw new InvalidFileException();
                            }
                            array = new byte[Kernel32.SizeofResource(intPtr, intPtr4)];
                            Marshal.Copy(intPtr6, array, 0, array.Length);
                            MemoryStream stream2 = new MemoryStream(array);
                            IconImage iconImage = new IconImage(stream2, array.Length);
                            singleIcon.Add(iconImage);
                        }
                        multiIcon.Add(singleIcon);
                    }
                }
                result = multiIcon;
            }
            catch (Exception)
            {
                throw new InvalidFileException();
            }
            finally
            {
                Kernel32.FreeLibrary(intPtr);
                if (text != null)
                {
                    File.Delete(text);
                }
            }
            return result;
        }

        public void Save(MultiIcon multiIcon, Stream stream)
        {
            string text = null;
            IntPtr zero = IntPtr.Zero;
            try
            {
                stream.Position = 0L;
                text = Path.GetTempFileName();
                FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write);
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                //TODO: Validation
                Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("EmptyDll.dll");
                byte[] array = new byte[manifestResourceStream.Length];
                manifestResourceStream.Read(array, 0, (int)manifestResourceStream.Length);
                stream.Read(array, 0, array.Length);
                fileStream.Write(array, 0, array.Length);
                fileStream.Close();
                IntPtr intPtr = Kernel32.BeginUpdateResource(text, false);
                if (intPtr == IntPtr.Zero)
                {
                    throw new InvalidFileException();
                }
                ushort num = 1;
                foreach (SingleIcon singleIcon in multiIcon)
                {
                    GRPICONDIR initalizated = GRPICONDIR.Initalizated;
                    initalizated.idCount = (ushort)singleIcon.Count;
                    initalizated.idEntries = new GRPICONDIRENTRY[(int)initalizated.idCount];
                    MemoryStream memoryStream;
                    for (int i = 0; i < singleIcon.Count; i++)
                    {
                        IconImage iconImage = singleIcon[i];
                        initalizated.idEntries[i] = iconImage.GRPICONDIRENTRY;
                        initalizated.idEntries[i].nID = num;
                        memoryStream = new MemoryStream((int)initalizated.idEntries[i].dwBytesInRes);
                        iconImage.Write(memoryStream);
                        array = memoryStream.GetBuffer();
                        Kernel32.UpdateResource(intPtr, 3u, (uint)num, 0, array, (uint)memoryStream.Length);
                        num += 1;
                        if (num % 70 == 0)
                        {
                            Kernel32.EndUpdateResource(intPtr, false);
                            intPtr = Kernel32.BeginUpdateResource(text, false);
                            if (intPtr == IntPtr.Zero)
                            {
                                throw new InvalidFileException();
                            }
                        }
                    }
                    memoryStream = new MemoryStream(initalizated.GroupDirSize);
                    initalizated.Write(memoryStream);
                    array = memoryStream.GetBuffer();
                    int value;
                    if (int.TryParse(singleIcon.Name, out value))
                    {
                        Kernel32.UpdateResource(intPtr, 14u, (IntPtr)value, 0, array, (uint)memoryStream.Length);
                    }
                    else
                    {
                        IntPtr intPtr2 = Marshal.StringToHGlobalAnsi(singleIcon.Name.ToUpper());
                        Kernel32.UpdateResource(intPtr, 14u, intPtr2, 0, array, (uint)memoryStream.Length);
                        Marshal.FreeHGlobal(intPtr2);
                    }
                }
                Kernel32.EndUpdateResource(intPtr, false);
                fileStream = new FileStream(text, FileMode.Open, FileAccess.Read);
                array = new byte[fileStream.Length];
                fileStream.Read(array, 0, array.Length);
                stream.Write(array, 0, array.Length);
                fileStream.Close();
            }
            catch (Exception)
            {
                throw new InvalidFileException();
            }
            finally
            {
                Kernel32.FreeLibrary(zero);
                if (text != null)
                {
                    File.Delete(text);
                }
            }
        }

        private static bool EnumResNameProc(IntPtr hModule, IntPtr pType, IntPtr pName, IntPtr param)
        {
            if (Kernel32.IS_INTRESOURCE(pName))
            {
                PEFormat.mIconsIDs.Add(pName.ToString());
            }
            else
            {
                PEFormat.mIconsIDs.Add(Marshal.PtrToStringUni(pName));
            }
            return true;
        }

        private static List<string> mIconsIDs;
    }
}