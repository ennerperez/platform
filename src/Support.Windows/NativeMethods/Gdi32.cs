// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Windows
{
#pragma warning disable CS0649

    /// <summary>
    /// Imports from Gdi32.dll
    /// </summary>
#if !INTEROP

    internal class Gdi32
#else

    public class Gdi32
#endif
    {
        [DllImport(ExternDll.Gdi32)]
        public static extern uint GetBoundsRect(IntPtr hdc, out RECT lprcBounds, uint flags);

        [DllImport(ExternDll.Gdi32)]
        public static extern IntPtr CreateSolidBrush(uint crColor);

        [DllImport(ExternDll.Gdi32)]
        public static extern IntPtr CreatePatternBrush(IntPtr hbmp);

        [DllImport(ExternDll.Gdi32)]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport(ExternDll.Gdi32)]
        public static extern IntPtr CreateCompatibleBitmap(
            IntPtr hdc,        // handle to DC
            int nWidth,     // width of bitmap, in pixels
            int nHeight     // height of bitmap, in pixels
            );

        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(
            IntPtr hdc   // handle to DC
            );

        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern IntPtr SelectObject(
            IntPtr hdc,          // handle to DC
            IntPtr hgdiobj   // handle to object
            );

        [DllImport(ExternDll.Gdi32, CharSet = CharSet.Auto)]
        public static extern int GetObject(IntPtr hObject, int cbBuffer, IntPtr lpvObject);

        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern int SetBkMode(IntPtr hdc, int bkMode);

        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern int SetBkColor(IntPtr hdc, int crColor);

        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern int SetTextColor(IntPtr hdc, int crColor);

        public static int ToColorRef(Color color)
        {
            return (color.B << 16) | (color.G << 8) | color.R;
        }

        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern bool DeleteObject(IntPtr hObject);  // handle to object

        [DllImport(ExternDll.Gdi32)]
        public static extern IntPtr CreateDC(
            string lpszDriver,        // driver name
            IntPtr lpszDevice,        // device name
            IntPtr lpszOutput,        // not used; should be NULL
            IntPtr lpInitData  // optional printer data
                               );

        [DllImport(ExternDll.Gdi32)]
        public static extern int BitBlt(
            IntPtr hdcDest, // handle to destination DC
            int nXDest,  // x-coord of destination upper-left corner
            int nYDest,  // y-coord of destination upper-left corner
            int nWidth,  // width of destination rectangle
            int nHeight, // height of destination rectangle
            IntPtr hdcSrc,  // handle to source DC
            int nXSrc,   // x-coordinate of source upper-left corner
            int nYSrc,   // y-coordinate of source upper-left corner
            TernaryRasterOperations dwRop  // raster operation code
            );

        public enum TernaryRasterOperations : uint
        {
            SRCCOPY = 0x00CC0020,
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            SRCINVERT = 0x00660046,
            SRCERASE = 0x00440328,
            NOTSRCCOPY = 0x00330008,
            NOTSRCERASE = 0x001100A6,
            MERGECOPY = 0x00C000CA,
            MERGEPAINT = 0x00BB0226,
            PATCOPY = 0x00F00021,
            PATPAINT = 0x00FB0A09,
            PATINVERT = 0x005A0049,
            DSTINVERT = 0x00550009,
            BLACKNESS = 0x00000042,
            WHITENESS = 0x00FF0062
        }

        [DllImport(ExternDll.Gdi32)]
        public static extern GraphicsMode SetGraphicsMode(IntPtr hdc, GraphicsMode iMode);

        public enum GraphicsMode : int
        {
            Compatible = 1,
            Advanced = 2
        }

        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern bool SetWorldTransform(IntPtr hdc, [In] ref Gdi32.XFORM lpXform);

        /// <summary>
        /// The GetWorldTransform function retrieves the current world-space to page-space transformation.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="lpXform">A pointer to an XFORM structure that receives the current world-space to page-space transformation.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd144953(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.
        /// </returns>
        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern bool GetWorldTransform(IntPtr hdc, [Out] out Gdi32.XFORM lpXform);

        public struct XFORM
        {
            public float eM11;
            public float eM12;
            public float eM21;
            public float eM22;
            public float eDx;
            public float eDy;
        }

        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern uint GetLayout(IntPtr hdc);

        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern bool GetWindowOrgEx(IntPtr hdc, out POINT lpPoint);

        [DllImport(ExternDll.Gdi32)]
        public static extern bool RoundRect(IntPtr hdc, int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect, int nWidth, int nHeight);

        [DllImport(ExternDll.Gdi32)]
        public static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,      // x-coordinate of upper-left corner
            int nTopRect,       // y-coordinate of upper-left corner
            int nRightRect,     // x-coordinate of lower-right corner
            int nBottomRect,    // y-coordinate of lower-right corner
            int nWidthEllipse,  // height of ellipse
            int nHeightEllipse  // width of ellipse
            );

        [DllImport(ExternDll.Gdi32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool GetTextExtentPoint32(
            IntPtr hdc,
            [MarshalAs(UnmanagedType.LPTStr)] string lpString,
            int cbString,
            out SIZE lpSize
            );

        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern bool LPtoDP(
            IntPtr hdc,
            POINT[] lpPoints,
            int nCounts);
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct SIZEL
#else

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct SIZEL
#endif
    {
        public int cx;
        public int cy;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct POINTL
#else

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct POINTL
#endif

    {
        public int x;
        public int y;
    }

#if !INTEROP

    internal struct DEVICECAPS
#else

    public struct DEVICECAPS
#endif
    {
        public const int LOGPIXELSX = 88;
        public const int LOGPIXELSY = 90;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct BITMAP
#else

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct BITMAP
#endif

    {
        public int bmType;
        public int bmWidth;
        public int bmHeight;
        public int bmWidthBytes;
        public short bmPlanes;
        public short bmBitsPixel;
        public int bmBits;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct RGBQUAD
#else
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct RGBQUAD
#endif
    {
        public void Set(byte r, byte g, byte b)
        {
            this.rgbRed = r;
            this.rgbGreen = g;
            this.rgbBlue = b;
        }

        public byte rgbBlue;

        public byte rgbGreen;

        public byte rgbRed;

        public byte rgbReserved;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Unicode)]
    internal struct BITMAPINFOHEADER
#else

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Unicode)]
    public struct BITMAPINFOHEADER
#endif
    {
        public BITMAPINFOHEADER(Stream stream)
        {
            this = default(BITMAPINFOHEADER);
            this.Read(stream);
        }

        public unsafe void Read(Stream stream)
        {
            byte[] array = new byte[sizeof(BITMAPINFOHEADER)];
            stream.Read(array, 0, array.Length);
            fixed (byte* ptr = array)
            {
                this = *(BITMAPINFOHEADER*)ptr;
            }
        }

        public unsafe void Write(Stream stream)
        {
            byte[] array = new byte[sizeof(BITMAPINFOHEADER)];
            fixed (BITMAPINFOHEADER* ptr = &this)
            {
                Marshal.Copy((IntPtr)((void*)ptr), array, 0, sizeof(BITMAPINFOHEADER));
            }
            stream.Write(array, 0, sizeof(BITMAPINFOHEADER));
        }

        public uint biSize;

        public uint biWidth;

        public uint biHeight;

        public ushort biPlanes;

        public ushort biBitCount;

        public int biCompression;

        public uint biSizeImage;

        public int biXPelsPerMeter;

        public int biYPelsPerMeter;

        public uint biClrUsed;

        public uint biClrImportant;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Unicode)]
    internal struct BITMAPINFO
#else

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Unicode)]
    public struct BITMAPINFO
#endif
    {
        public BITMAPINFOHEADER icHeader;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public RGBQUAD[] icColors;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct ICONDIRENTRY
#else
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct ICONDIRENTRY
#endif
    {
        public ICONDIRENTRY(Stream stream)
        {
            this = default(ICONDIRENTRY);
            this.Read(stream);
        }

        public unsafe void Read(Stream stream)
        {
            BinaryReader binaryReader = new BinaryReader(stream);
            byte[] array = new byte[sizeof(ICONDIRENTRY)];
            binaryReader.Read(array, 0, sizeof(ICONDIRENTRY));
            fixed (byte* ptr = array)
            {
                this = *(ICONDIRENTRY*)ptr;
            }
        }

        public unsafe void Write(Stream stream)
        {
            byte[] array = new byte[sizeof(ICONDIRENTRY)];
            fixed (ICONDIRENTRY* ptr = &this)
            {
                Marshal.Copy((IntPtr)((void*)ptr), array, 0, sizeof(ICONDIRENTRY));
            }
            stream.Write(array, 0, sizeof(ICONDIRENTRY));
        }

        public GRPICONDIRENTRY ToGrpIconEntry()
        {
            return new GRPICONDIRENTRY
            {
                bColorCount = this.bColorCount,
                bHeight = this.bHeight,
                bReserved = this.bReserved,
                bWidth = this.bWidth,
                dwBytesInRes = this.dwBytesInRes,
                wBitCount = this.wBitCount,
                wPlanes = this.wPlanes
            };
        }

        public byte bWidth;

        public byte bHeight;

        public byte bColorCount;

        public byte bReserved;

        public ushort wPlanes;

        public ushort wBitCount;

        public uint dwBytesInRes;

        public uint dwImageOffset;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct ICONDIR
#else
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct ICONDIR
#endif
    {
        public ICONDIR(ushort reserved, ushort type, ushort count)
        {
            this.idReserved = reserved;
            this.idType = type;
            this.idCount = count;
        }

        public ICONDIR(Stream stream)
        {
            this = default(ICONDIR);
            this.Read(stream);
        }

        public static ICONDIR Initalizated
        {
            get
            {
                return new ICONDIR(0, 1, 0);
            }
        }

        public unsafe void Read(Stream stream)
        {
            byte[] array = new byte[sizeof(ICONDIR)];
            stream.Read(array, 0, sizeof(ICONDIR));
            fixed (byte* ptr = array)
            {
                this = *(ICONDIR*)ptr;
            }
        }

        public unsafe void Write(Stream stream)
        {
            byte[] array = new byte[sizeof(ICONDIR)];
            fixed (ICONDIR* ptr = &this)
            {
                Marshal.Copy((IntPtr)((void*)ptr), array, 0, sizeof(ICONDIR));
            }
            stream.Write(array, 0, sizeof(ICONDIR));
        }

        public ushort idReserved;

        public ushort idType;

        public ushort idCount;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal unsafe struct GRPICONDIR
#else
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public unsafe struct GRPICONDIR
#endif
    {
        public GRPICONDIR(ushort reserved, ushort type, ushort count)
        {
            this.idReserved = reserved;
            this.idType = type;
            this.idCount = count;
            this.idEntries = new GRPICONDIRENTRY[0];
        }

        public GRPICONDIR(Stream stream)
        {
            this = default(GRPICONDIR);
            this.Read(stream);
        }

        public static GRPICONDIR Initalizated
        {
            get
            {
                return new GRPICONDIR(0, 1, 0);
            }
        }

        public int GroupDirSize
        {
            get
            {
                return 6 + this.idEntries.Length * sizeof(GRPICONDIRENTRY);
            }
        }

        public void Read(Stream stream)
        {
            BinaryReader binaryReader = new BinaryReader(stream);
            this.idReserved = binaryReader.ReadUInt16();
            this.idType = binaryReader.ReadUInt16();
            this.idCount = binaryReader.ReadUInt16();
            this.idEntries = new GRPICONDIRENTRY[(int)this.idCount];
            for (int i = 0; i < (int)this.idCount; i++)
            {
                this.idEntries[i] = new GRPICONDIRENTRY(stream);
            }
        }

        public void Write(Stream stream)
        {
            BinaryWriter binaryWriter = new BinaryWriter(stream);
            binaryWriter.Write(this.idReserved);
            binaryWriter.Write(this.idType);
            binaryWriter.Write(this.idCount);
            for (int i = 0; i < (int)this.idCount; i++)
            {
                this.idEntries[i].Write(stream);
            }
        }

        public ushort idReserved;

        public ushort idType;

        public ushort idCount;

        public GRPICONDIRENTRY[] idEntries;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct GRPICONDIRENTRY
#else
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct GRPICONDIRENTRY
#endif
    {
        public GRPICONDIRENTRY(Stream stream)
        {
            this = default(GRPICONDIRENTRY);
            this.Read(stream);
        }

        public unsafe void Read(Stream stream)
        {
            byte[] array = new byte[sizeof(GRPICONDIRENTRY)];
            stream.Read(array, 0, sizeof(GRPICONDIRENTRY));
            fixed (byte* ptr = array)
            {
                this = *(GRPICONDIRENTRY*)ptr;
            }
        }

        public unsafe void Write(Stream stream)
        {
            byte[] array = new byte[sizeof(GRPICONDIRENTRY)];
            fixed (GRPICONDIRENTRY* ptr = &this)
            {
                Marshal.Copy((IntPtr)((void*)ptr), array, 0, sizeof(GRPICONDIRENTRY));
            }
            stream.Write(array, 0, sizeof(GRPICONDIRENTRY));
        }

        public ICONDIRENTRY ToIconDirEntry()
        {
            return new ICONDIRENTRY
            {
                bColorCount = this.bColorCount,
                bHeight = this.bHeight,
                bReserved = this.bReserved,
                bWidth = this.bWidth,
                dwBytesInRes = this.dwBytesInRes,
                wBitCount = this.wBitCount,
                wPlanes = this.wPlanes
            };
        }

        public byte bWidth;

        public byte bHeight;

        public byte bColorCount;

        public byte bReserved;

        public ushort wPlanes;

        public ushort wBitCount;

        public uint dwBytesInRes;

        public ushort nID;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct MEMICONDIR
#else
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct MEMICONDIR
#endif
    {
        public ushort wReserved;

        public ushort wType;

        public ushort wCount;

        public MEMICONDIRENTRY arEntries;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct MEMICONDIRENTRY
#else
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct MEMICONDIRENTRY
#endif
    {
        public byte bWidth;

        public byte bHeight;

        public byte bColorCount;

        public byte bReserved;

        public ushort wPlanes;

        public ushort wBitCount;

        public uint dwBytesInRes;

        public ushort wId;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct IMAGE_DOS_HEADER
#else
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public unsafe struct IMAGE_DOS_HEADER
#endif
    {
        public IMAGE_DOS_HEADER(Stream stream)
        {
            this = default(IMAGE_DOS_HEADER);
            this.Read(stream);
        }

        public unsafe void Read(Stream stream)
        {
            byte[] array = new byte[sizeof(IMAGE_DOS_HEADER)];
            stream.Read(array, 0, array.Length);
            fixed (byte* ptr = array)
            {
                this = *(IMAGE_DOS_HEADER*)ptr;
            }
        }

        public unsafe void Write(Stream stream)
        {
            byte[] array = new byte[sizeof(IMAGE_DOS_HEADER)];
            fixed (IMAGE_DOS_HEADER* ptr = &this)
            {
                Marshal.Copy((IntPtr)((void*)ptr), array, 0, sizeof(IMAGE_DOS_HEADER));
            }
            stream.Write(array, 0, sizeof(IMAGE_DOS_HEADER));
        }

        public ushort e_magic;

        public ushort e_cblp;

        public ushort e_cp;

        public ushort e_crlc;

        public ushort e_cparhdr;

        public ushort e_minalloc;

        public ushort e_maxalloc;

        public ushort e_ss;

        public ushort e_sp;

        public ushort e_csum;

        public ushort e_ip;

        public ushort e_cs;

        public ushort e_lfarlc;

        public ushort e_ovno;

        //TODO: Validation
        //[FixedBuffer(typeof(short), 4)]
        public FixedBuffer0 e_res;

        public ushort e_oemid;

        public ushort e_oeminfo;

        //TODO: Validation
        //[FixedBuffer(typeof(short), 10)]
        public FixedBuffer1 e_res2;

        public uint e_lfanew;

        [UnsafeValueType]
        [CompilerGenerated]
        [StructLayout(LayoutKind.Sequential, Size = 8)]
        public struct FixedBuffer0
        {
            public short FixedElementField;
        }

        [UnsafeValueType]
        [CompilerGenerated]
        [StructLayout(LayoutKind.Sequential, Size = 20)]
        public struct FixedBuffer1
        {
            public short FixedElementField;
        }
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct IMAGE_OS2_HEADER
#else
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct IMAGE_OS2_HEADER
#endif
    {
        public IMAGE_OS2_HEADER(Stream stream)
        {
            this = default(IMAGE_OS2_HEADER);
            this.Read(stream);
        }

        public unsafe void Read(Stream stream)
        {
            byte[] array = new byte[sizeof(IMAGE_OS2_HEADER)];
            stream.Read(array, 0, array.Length);
            fixed (byte* ptr = array)
            {
                this = *(IMAGE_OS2_HEADER*)ptr;
            }
        }

        public unsafe void Write(Stream stream)
        {
            byte[] array = new byte[sizeof(IMAGE_OS2_HEADER)];
            fixed (IMAGE_OS2_HEADER* ptr = &this)
            {
                Marshal.Copy((IntPtr)((void*)ptr), array, 0, sizeof(IMAGE_OS2_HEADER));
            }
            stream.Write(array, 0, sizeof(IMAGE_OS2_HEADER));
        }

        public ushort ne_magic;

        public sbyte ne_ver;

        public sbyte ne_rev;

        public ushort ne_enttab;

        public ushort ne_cbenttab;

        public uint ne_crc;

        public ushort ne_flags;

        public ushort ne_autodata;

        public ushort ne_heap;

        public ushort ne_stack;

        public uint ne_csip;

        public uint ne_sssp;

        public ushort ne_cseg;

        public ushort ne_cmod;

        public ushort ne_cbnrestab;

        public ushort ne_segtab;

        public ushort ne_rsrctab;

        public ushort ne_restab;

        public ushort ne_modtab;

        public ushort ne_imptab;

        public uint ne_nrestab;

        public ushort ne_cmovent;

        public ushort ne_align;

        public ushort ne_cres;

        public byte ne_exetyp;

        public byte ne_flagsothers;

        public ushort ne_pretthunks;

        public ushort ne_psegrefbytes;

        public ushort ne_swaparea;

        public ushort ne_expver;
    }

#if !INTEROP

    [Flags]
    internal enum ResourceMemoryType : ushort
#else
    [Flags]
    public enum ResourceMemoryType : ushort
#endif
    {
        None = 0,
        Moveable = 16,
        Pure = 32,
        PreLoad = 64,
        Unknown = 7168
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct TNAMEINFO
#else
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct TNAMEINFO
#endif
    {
        public TNAMEINFO(Stream stream)
        {
            this = default(TNAMEINFO);
            this.Read(stream);
        }

        public ushort ID
        {
            get
            {
                if (this.rnID <= 32768)
                {
                    return this.rnID;
                }
                return (ushort)((int)this.rnID & -32769);
            }
        }

        public ResourceMemoryType ResourceMemoryType
        {
            get
            {
                return (ResourceMemoryType)this.rnFlags;
            }
        }

        public unsafe void Read(Stream stream)
        {
            byte[] array = new byte[sizeof(TNAMEINFO)];
            stream.Read(array, 0, array.Length);
            fixed (byte* ptr = array)
            {
                this = *(TNAMEINFO*)ptr;
            }
        }

        public unsafe void Write(Stream stream)
        {
            byte[] array = new byte[sizeof(TNAMEINFO)];
            fixed (TNAMEINFO* ptr = &this)
            {
                Marshal.Copy((IntPtr)((void*)ptr), array, 0, sizeof(TNAMEINFO));
            }
            stream.Write(array, 0, sizeof(TNAMEINFO));
        }

        public ushort rnOffset;

        public ushort rnLength;

        public ushort rnFlags;

        public ushort rnID;

        public ushort rnHandle;

        public ushort rnUsage;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct TYPEINFO
#else
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct TYPEINFO
#endif
    {
        public TYPEINFO(Stream stream)
        {
            this = default(TYPEINFO);
            this.Read(stream);
        }

        public ResourceType ResourceType
        {
            get
            {
                return (ResourceType)(this.rtTypeID & 255);
            }
        }

        public void Read(Stream stream)
        {
            BinaryReader binaryReader = new BinaryReader(stream);
            this.rtTypeID = binaryReader.ReadUInt16();
            if (this.rtTypeID == 0)
            {
                return;
            }
            this.rtResourceCount = binaryReader.ReadUInt16();
            this.rtReserved = binaryReader.ReadUInt32();
            this.rtNameInfo = new TNAMEINFO[(int)this.rtResourceCount];
            for (int i = 0; i < this.rtNameInfo.Length; i++)
            {
                this.rtNameInfo[i].Read(stream);
            }
        }

        public void Write(Stream stream)
        {
            BinaryWriter binaryWriter = new BinaryWriter(stream);
            binaryWriter.Write(this.rtTypeID);
            binaryWriter.Write(this.rtResourceCount);
            binaryWriter.Write(this.rtReserved);
            foreach (TNAMEINFO tnameinfo in this.rtNameInfo)
            {
                tnameinfo.Write(stream);
            }
        }

        public ushort rtTypeID;

        public ushort rtResourceCount;

        public uint rtReserved;

        public TNAMEINFO[] rtNameInfo;
    }

#if !INTEROP

    internal enum ResourceType : uint
#else
    public enum ResourceType : uint
#endif
    {
        RT_CURSOR = 1u,
        RT_BITMAP,
        RT_ICON,
        RT_MENU,
        RT_DIALOG,
        RT_STRING,
        RT_FONTDIR,
        RT_FONT,
        RT_ACCELERATOR,
        RT_RCDATA,
        RT_MESSAGETABLE,
        RT_GROUP_CURSOR,
        RT_GROUP_ICON = 14u,
        RT_VERSION = 16u,
        RT_DLGINCLUDE,
        RT_PLUGPLAY = 19u,
        RT_VXD,
        RT_ANICURSOR,
        RT_ANIICON,
        RT_HTML
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct RESOURCE_TABLE
#else
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct RESOURCE_TABLE
#endif
    {
        public RESOURCE_TABLE(Stream stream)
        {
            this = default(RESOURCE_TABLE);
            this.Read(stream);
        }

        public string[] ResourceNames
        {
            get
            {
                List<string> list = new List<string>();
                byte b;
                for (int i = 0; i < this.rscResourceNames.Length; i += (int)b)
                {
                    b = this.rscResourceNames[i++];
                    list.Add(Encoding.GetEncoding(1251).GetString(this.rscResourceNames, i, (int)b));
                }
                return list.ToArray();
            }
        }

        public void Read(Stream stream)
        {
            BinaryReader binaryReader = new BinaryReader(stream);
            this.rscAlignShift = binaryReader.ReadUInt16();
            List<TYPEINFO> list = new List<TYPEINFO>();
            TYPEINFO item = new TYPEINFO(stream);
            while (item.rtTypeID != 0)
            {
                list.Add(item);
                item = new TYPEINFO(stream);
            }
            this.rscTypes = list.ToArray();
            this.rscEndTypes = 0;
            this.rscResourceNames = new byte[0];
            for (byte b = binaryReader.ReadByte(); b != 0; b = binaryReader.ReadByte())
            {
                byte[] array = new byte[this.rscResourceNames.Length + (int)b + 1];
                this.rscResourceNames.CopyTo(array, 0);
                array[this.rscResourceNames.Length] = b;
                stream.Read(array, this.rscResourceNames.Length + 1, (int)b);
                this.rscResourceNames = array;
            }
            this.rscEndNames = 0;
        }

        public void Write(Stream stream)
        {
            BinaryWriter binaryWriter = new BinaryWriter(stream);
            binaryWriter.Write(this.rscAlignShift);
            foreach (TYPEINFO typeinfo in this.rscTypes)
            {
                typeinfo.Write(stream);
            }
            binaryWriter.Write(this.rscEndTypes);
            binaryWriter.Write(this.rscResourceNames);
            binaryWriter.Write(this.rscEndNames);
        }

        public List<GRPICONDIR> GetGroupIcons(Stream stream)
        {
            List<GRPICONDIR> list = new List<GRPICONDIR>();
            for (int i = 0; i < this.rscTypes.Length; i++)
            {
                if (this.rscTypes[i].ResourceType == ResourceType.RT_GROUP_ICON)
                {
                    for (int j = 0; j < this.rscTypes[i].rtNameInfo.Length; j++)
                    {
                        stream.Seek((long)((1 << (int)this.rscAlignShift) * (int)this.rscTypes[i].rtNameInfo[j].rnOffset), SeekOrigin.Begin);
                        GRPICONDIR item = new GRPICONDIR(stream);
                        list.Add(item);
                    }
                    break;
                }
            }
            return list;
        }

        public void SetGroupIcons(Stream stream, List<GRPICONDIR> grpIconDir)
        {
            for (int i = 0; i < this.rscTypes.Length; i++)
            {
                if (this.rscTypes[i].ResourceType == ResourceType.RT_GROUP_ICON)
                {
                    for (int j = 0; j < this.rscTypes[i].rtNameInfo.Length; j++)
                    {
                        stream.Seek((long)((1 << (int)this.rscAlignShift) * (int)this.rscTypes[i].rtNameInfo[j].rnOffset), SeekOrigin.Begin);
                        grpIconDir[j].Write(stream);
                    }
                    return;
                }
            }
        }

        public List<ushort> GetGroupIDs(Stream stream)
        {
            List<ushort> list = new List<ushort>();
            for (int i = 0; i < this.rscTypes.Length; i++)
            {
                if (this.rscTypes[i].ResourceType == ResourceType.RT_GROUP_ICON)
                {
                    for (int j = 0; j < this.rscTypes[i].rtNameInfo.Length; j++)
                    {
                        list.Add(this.rscTypes[i].rtNameInfo[j].ID);
                    }
                    break;
                }
            }
            return list;
        }

        public ushort rscAlignShift;

        public TYPEINFO[] rscTypes;

        public ushort rscEndTypes;

        public byte[] rscResourceNames;

        public byte rscEndNames;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct IMAGE_DATA_DIRECTORY
#else

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct IMAGE_DATA_DIRECTORY
#endif
    {
        public uint VirtualAddress;
        public uint Size;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct IMAGE_OPTIONAL_HEADER
#else
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct IMAGE_OPTIONAL_HEADER
#endif
    {
        public ushort Magic;
        public byte MajorLinkerVersion;
        public byte MinorLinkerVersion;
        public uint SizeOfCode;
        public uint SizeOfInitializedData;
        public uint SizeOfUninitializedData;
        public uint AddressOfEntryPoint;
        public uint BaseOfCode;
        public uint BaseOfData;
        public uint ImageBase;
        public uint SectionAlignment;
        public uint FileAlignment;
        public ushort MajorOperatingSystemVersion;
        public ushort MinorOperatingSystemVersion;
        public ushort MajorImageVersion;
        public ushort MinorImageVersion;
        public ushort MajorSubsystemVersion;
        public ushort MinorSubsystemVersion;
        public uint Win32VersionValue;
        public uint SizeOfImage;
        public uint SizeOfHeaders;
        public uint CheckSum;
        public ushort Subsystem;
        public ushort DllCharacteristics;
        public uint SizeOfStackReserve;
        public uint SizeOfStackCommit;
        public uint SizeOfHeapReserve;
        public uint SizeOfHeapCommit;
        public uint LoaderFlags;
        public uint NumberOfRvaAndSizes;
        public IMAGE_DATA_DIRECTORY DataDirectory1;
        public IMAGE_DATA_DIRECTORY DataDirectory2;
        public IMAGE_DATA_DIRECTORY DataDirectory3;
        public IMAGE_DATA_DIRECTORY DataDirectory4;
        public IMAGE_DATA_DIRECTORY DataDirectory5;
        public IMAGE_DATA_DIRECTORY DataDirectory6;
        public IMAGE_DATA_DIRECTORY DataDirectory7;
        public IMAGE_DATA_DIRECTORY DataDirectory8;
        public IMAGE_DATA_DIRECTORY DataDirectory9;
        public IMAGE_DATA_DIRECTORY DataDirectory10;
        public IMAGE_DATA_DIRECTORY DataDirectory11;
        public IMAGE_DATA_DIRECTORY DataDirectory12;
        public IMAGE_DATA_DIRECTORY DataDirectory13;
        public IMAGE_DATA_DIRECTORY DataDirectory14;
        public IMAGE_DATA_DIRECTORY DataDirectory15;
        public IMAGE_DATA_DIRECTORY DataDirectory16;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct IMAGE_FILE_HEADER
#else

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct IMAGE_FILE_HEADER
#endif
    {
        public ushort Machine;
        public ushort NumberOfSections;
        public uint TimeDateStamp;
        public uint PointerToSymbolTable;
        public uint NumberOfSymbols;
        public ushort SizeOfOptionalHeader;
        public ushort Characteristics;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct IMAGE_NT_HEADERS
#else

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct IMAGE_NT_HEADERS
#endif
    {
        public IMAGE_NT_HEADERS(Stream stream)
        {
            this = default(IMAGE_NT_HEADERS);
            this.Read(stream);
        }

        public unsafe void Read(Stream stream)
        {
            BinaryReader binaryReader = new BinaryReader(stream);
            this.Signature = binaryReader.ReadUInt32();
            byte[] array = new byte[sizeof(IMAGE_FILE_HEADER)];
            stream.Read(array, 0, array.Length);
            fixed (byte* ptr = array)
            {
                this.FileHeader = *(IMAGE_FILE_HEADER*)ptr;
            }
            array = new byte[sizeof(IMAGE_OPTIONAL_HEADER)];
            stream.Read(array, 0, array.Length);
            fixed (byte* ptr2 = array)
            {
                this.OptionalHeader = *(IMAGE_OPTIONAL_HEADER*)ptr2;
            }
        }

        public uint Signature;

        public IMAGE_FILE_HEADER FileHeader;

        public IMAGE_OPTIONAL_HEADER OptionalHeader;
    }

#pragma warning restore
}