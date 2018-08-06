using Platform.Support.Drawing.Icons.Exceptions;
using Platform.Support.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing.Icons.EncodingFormats
{
    internal unsafe class NEFormat : ILibraryFormat
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
                IMAGE_OS2_HEADER image_OS2_HEADER = new IMAGE_OS2_HEADER(stream);
                if (image_OS2_HEADER.ne_magic != 17742)
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
            IMAGE_DOS_HEADER image_DOS_HEADER = new IMAGE_DOS_HEADER(stream);
            if (image_DOS_HEADER.e_magic != 23117)
            {
                throw new InvalidICLFileException();
            }
            stream.Seek((long)((ulong)image_DOS_HEADER.e_lfanew), SeekOrigin.Begin);
            IMAGE_OS2_HEADER image_OS2_HEADER = new IMAGE_OS2_HEADER(stream);
            if (image_OS2_HEADER.ne_magic != 17742)
            {
                throw new InvalidICLFileException();
            }
            stream.Seek((long)((ulong)((uint)image_OS2_HEADER.ne_rsrctab + image_DOS_HEADER.e_lfanew)), SeekOrigin.Begin);
            if (image_OS2_HEADER.ne_restab == image_OS2_HEADER.ne_rsrctab)
            {
                return new MultiIcon();
            }
            RESOURCE_TABLE resource_TABLE = new RESOURCE_TABLE(stream);
            Dictionary<ushort, IconImage> icons = resource_TABLE.GetIcons(stream);
            List<GRPICONDIR> groupIcons = resource_TABLE.GetGroupIcons(stream);
            List<string> list = new List<string>(resource_TABLE.ResourceNames);
            if (list[0].ToLower() == "icl")
            {
                list.RemoveAt(0);
            }
            SingleIcon[] array = new SingleIcon[groupIcons.Count];
            for (int i = 0; i < array.Length; i++)
            {
                if (i < list.Count)
                {
                    array[i] = new SingleIcon(list[i]);
                }
                else
                {
                    string text = this.FindFreeName(list);
                    list.Add(text);
                    array[i] = new SingleIcon(text);
                }
                foreach (GRPICONDIRENTRY grpicondirentry in groupIcons[i].idEntries)
                {
                    array[i].Add(icons[grpicondirentry.nID]);
                }
            }
            return new MultiIcon(array);
        }

        public void Save(MultiIcon multiIcon, Stream stream)
        {
            IMAGE_DOS_HEADER image_DOS_HEADER = default(IMAGE_DOS_HEADER);
            IMAGE_OS2_HEADER image_OS2_HEADER = default(IMAGE_OS2_HEADER);
            RESOURCE_TABLE resource_TABLE = default(RESOURCE_TABLE);
            TYPEINFO typeinfo = default(TYPEINFO);
            TYPEINFO typeinfo2 = default(TYPEINFO);
            List<GRPICONDIR> list = new List<GRPICONDIR>();
            Dictionary<ushort, IconImage> dictionary = new Dictionary<ushort, IconImage>();
            int num = 0;
            image_DOS_HEADER.e_magic = 23117;
            image_DOS_HEADER.e_lfanew = 144u;
            image_DOS_HEADER.e_cblp = 80;
            image_DOS_HEADER.e_cp = 2;
            image_DOS_HEADER.e_cparhdr = 4;
            image_DOS_HEADER.e_lfarlc = 64;
            image_DOS_HEADER.e_maxalloc = ushort.MaxValue;
            image_DOS_HEADER.e_minalloc = 15;
            image_DOS_HEADER.e_sp = 184;
            num += (int)image_DOS_HEADER.e_lfanew;
            image_OS2_HEADER.ne_magic = 17742;
            image_OS2_HEADER.ne_ver = 71;
            image_OS2_HEADER.ne_rev = 70;
            image_OS2_HEADER.ne_enttab = 178;
            image_OS2_HEADER.ne_cbenttab = 10;
            image_OS2_HEADER.ne_crc = 0u;
            image_OS2_HEADER.ne_flags = 33545;
            image_OS2_HEADER.ne_autodata = 3;
            image_OS2_HEADER.ne_heap = 1024;
            image_OS2_HEADER.ne_stack = 0;
            image_OS2_HEADER.ne_csip = 65536u;
            image_OS2_HEADER.ne_sssp = 0u;
            image_OS2_HEADER.ne_cseg = 0;
            image_OS2_HEADER.ne_cmod = 1;
            image_OS2_HEADER.ne_cbnrestab = 26;
            image_OS2_HEADER.ne_segtab = 64;
            image_OS2_HEADER.ne_rsrctab = 64;
            image_OS2_HEADER.ne_restab = 132;
            image_OS2_HEADER.ne_modtab = 168;
            image_OS2_HEADER.ne_imptab = 170;
            image_OS2_HEADER.ne_nrestab = 332u;
            image_OS2_HEADER.ne_cmovent = 1;
            image_OS2_HEADER.ne_align = 10;
            image_OS2_HEADER.ne_cres = 0;
            image_OS2_HEADER.ne_exetyp = 2;
            image_OS2_HEADER.ne_flagsothers = 0;
            image_OS2_HEADER.ne_pretthunks = 0;
            image_OS2_HEADER.ne_psegrefbytes = 0;
            image_OS2_HEADER.ne_swaparea = 0;
            image_OS2_HEADER.ne_expver = 768;
            num += (int)image_OS2_HEADER.ne_rsrctab;
            resource_TABLE.rscAlignShift = 10;
            num += 2;
            typeinfo.rtTypeID = 32782;
            typeinfo.rtResourceCount = (ushort)multiIcon.Count;
            num += 8;
            TNAMEINFO[] array = new TNAMEINFO[multiIcon.Count];
            num += sizeof(TNAMEINFO) * multiIcon.Count;
            int num2 = 0;
            foreach (SingleIcon singleIcon in multiIcon)
            {
                num2 += singleIcon.Count;
            }
            typeinfo2.rtTypeID = 32771;
            typeinfo2.rtResourceCount = (ushort)num2;
            num += 8;
            TNAMEINFO[] array2 = new TNAMEINFO[num2];
            num += sizeof(TNAMEINFO) * num2;
            resource_TABLE.rscEndTypes = 0;
            num += 2;
            image_OS2_HEADER.ne_restab = (ushort)((long)num - (long)((ulong)image_DOS_HEADER.e_lfanew));
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
            binaryWriter.Write("ICL");
            foreach (SingleIcon singleIcon2 in multiIcon)
            {
                binaryWriter.Write(singleIcon2.Name);
            }
            byte[] array3 = new byte[memoryStream.Length];
            Array.Copy(memoryStream.GetBuffer(), array3, array3.Length);
            memoryStream.Dispose();
            num += array3.Length + 1;
            int num3 = (num >> (int)resource_TABLE.rscAlignShift) + 1;
            int num4 = 0;
            for (int i = 0; i < multiIcon.Count; i++)
            {
                SingleIcon singleIcon3 = multiIcon[i];
                GRPICONDIR item = default(GRPICONDIR);
                item.idCount = (ushort)singleIcon3.Count;
                item.idType = 14;
                GRPICONDIRENTRY[] array4 = new GRPICONDIRENTRY[singleIcon3.Count];
                for (int j = 0; j < singleIcon3.Count; j++)
                {
                    array2[num4].rnFlags = 7216;
                    array2[num4].rnHandle = 0;
                    array2[num4].rnID = (ushort)(32768 + num4 + 1);
                    array2[num4].rnUsage = 0;
                    array2[num4].rnOffset = (ushort)num3;
                    array2[num4].rnLength = (ushort)System.Math.Ceiling((double)((float)singleIcon3[j].IconImageSize / (float)(1 << (int)resource_TABLE.rscAlignShift)));
                    num3 += (int)array2[num4].rnLength;
                    array4[j] = singleIcon3[j].GRPICONDIRENTRY;
                    array4[j].nID = (ushort)(num4 + 1);
                    dictionary.Add((ushort)(num4 + 1), singleIcon3[j]);
                    num4++;
                }
                array[i].rnFlags = 7216;
                array[i].rnHandle = 0;
                array[i].rnID = (ushort)(32768 + i + 1);
                array[i].rnUsage = 0;
                array[i].rnOffset = (ushort)num3;
                array[i].rnLength = (ushort)System.Math.Ceiling((double)((float)(6 + singleIcon3.Count * sizeof(GRPICONDIRENTRY)) / (float)(1 << (int)resource_TABLE.rscAlignShift)));
                item.idEntries = array4;
                list.Add(item);
                num3 += (int)array[i].rnLength;
            }
            resource_TABLE.rscTypes = new TYPEINFO[2];
            resource_TABLE.rscTypes[0] = typeinfo;
            resource_TABLE.rscTypes[0].rtNameInfo = array;
            resource_TABLE.rscTypes[1] = typeinfo2;
            resource_TABLE.rscTypes[1].rtNameInfo = array2;
            resource_TABLE.rscResourceNames = array3;
            image_DOS_HEADER.Write(stream);
            stream.Write(NEFormat.MSDOS_STUB, 0, NEFormat.MSDOS_STUB.Length);
            stream.Seek((long)((ulong)image_DOS_HEADER.e_lfanew), SeekOrigin.Begin);
            image_OS2_HEADER.Write(stream);
            stream.Seek((long)((ulong)((uint)image_OS2_HEADER.ne_rsrctab + image_DOS_HEADER.e_lfanew)), SeekOrigin.Begin);
            resource_TABLE.Write(stream);
            resource_TABLE.SetGroupIcons(stream, list);
            resource_TABLE.SetIcons(stream, dictionary);
        }

        private string FindFreeName(List<string> names)
        {
            int num = 1;
            for (; ; )
            {
                bool flag = false;
                foreach (string text in names)
                {
                    if (text.ToLower() == "icon " + num)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    break;
                }
                num++;
            }
            return "Icon " + num;
        }

        private const byte SHIFT_FACTOR = 10;

        private static byte[] MSDOS_STUB = new byte[]
        {
            186,
            16,
            0,
            14,
            31,
            180,
            9,
            205,
            33,
            184,
            1,
            76,
            205,
            33,
            144,
            144,
            84,
            104,
            105,
            115,
            32,
            112,
            114,
            111,
            103,
            114,
            97,
            109,
            32,
            109,
            117,
            115,
            116,
            32,
            98,
            101,
            32,
            114,
            117,
            110,
            32,
            117,
            110,
            100,
            101,
            114,
            32,
            77,
            105,
            99,
            114,
            111,
            115,
            111,
            102,
            116,
            32,
            87,
            105,
            110,
            100,
            111,
            119,
            115,
            46,
            13,
            10,
            36,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0
        };
    }
}