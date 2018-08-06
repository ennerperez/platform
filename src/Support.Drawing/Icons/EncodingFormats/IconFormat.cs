using Platform.Support.Drawing.Icons.Exceptions;
using Platform.Support.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing.Icons.EncodingFormats
{
    internal unsafe class IconFormat : ILibraryFormat
    {
        public bool IsRecognizedFormat(Stream stream)
        {
            stream.Position = 0L;
            try
            {
                ICONDIR icondir = new ICONDIR(stream);
                if (icondir.idReserved != 0)
                {
                    return false;
                }
                if (icondir.idType != 1)
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

        public MultiIcon Load(Stream stream)
        {
            stream.Position = 0L;
            SingleIcon singleIcon = new SingleIcon("Untitled");
            ICONDIR icondir = new ICONDIR(stream);
            if (icondir.idReserved != 0)
            {
                throw new InvalidMultiIconFileException();
            }
            if (icondir.idType != 1)
            {
                throw new InvalidMultiIconFileException();
            }
            int num = sizeof(ICONDIR);
            for (int i = 0; i < (int)icondir.idCount; i++)
            {
                stream.Seek((long)num, SeekOrigin.Begin);
                ICONDIRENTRY entry = new ICONDIRENTRY(stream);
                entry = IconFormat.CheckAndRepairEntry(entry);
                stream.Seek((long)((ulong)entry.dwImageOffset), SeekOrigin.Begin);
                singleIcon.Add(new IconImage(stream, (int)(stream.Length - stream.Position)));
                num += sizeof(ICONDIRENTRY);
            }
            return new MultiIcon(singleIcon);
        }

        public void Save(MultiIcon multiIcon, Stream stream)
        {
            if (multiIcon.SelectedIndex == -1)
            {
                return;
            }
            SingleIcon singleIcon = multiIcon[multiIcon.SelectedIndex];
            ICONDIR initalizated = ICONDIR.Initalizated;
            initalizated.idCount = (ushort)singleIcon.Count;
            initalizated.Write(stream);
            int num = sizeof(ICONDIR);
            int num2 = sizeof(ICONDIR) + (int)initalizated.idCount * sizeof(ICONDIRENTRY);
            foreach (IconImage iconImage in singleIcon)
            {
                stream.Seek((long)num2, SeekOrigin.Begin);
                iconImage.Write(stream);
                long num3 = stream.Position - (long)num2;
                stream.Seek((long)num, SeekOrigin.Begin);
                ICONDIRENTRY icondirentry = iconImage.ICONDIRENTRY;
                stream.Seek((long)num, SeekOrigin.Begin);
                icondirentry.dwImageOffset = (uint)num2;
                icondirentry.dwBytesInRes = (uint)num3;
                icondirentry.Write(stream);
                num += sizeof(ICONDIRENTRY);
                num2 += (int)num3;
            }
        }

        private static ICONDIRENTRY CheckAndRepairEntry(ICONDIRENTRY entry)
        {
            if (entry.wBitCount == 0)
            {
                int num = (int)((ushort)entry.dwBytesInRes) - sizeof(BITMAPINFOHEADER);
                int num2 = ((int)(entry.bWidth + 31) & -32) >> 3;
                int num3 = num2 * (int)entry.bHeight;
                num -= num3;
                byte[] array = new byte[]
                {
                    1,
                    4,
                    8,
                    16,
                    24,
                    32
                };
                for (int i = 0; i <= 5; i++)
                {
                    int num4 = ((int)(entry.bWidth * array[i] + 31) & -32) >> 3;
                    int num5 = (int)entry.bHeight * num4;
                    int num6 = (array[i] <= 8) ? ((1 << (int)array[i]) * 4) : 0;
                    if (num6 + num5 == num)
                    {
                        entry.wBitCount = (ushort)array[i];
                        break;
                    }
                }
            }
            if (entry.wBitCount < 8 && entry.bColorCount == 0)
            {
                entry.bColorCount = (byte)(1 << (int)entry.wBitCount);
            }
            if (entry.wPlanes == 0)
            {
                entry.wPlanes = 1;
            }
            return entry;
        }
    }
}