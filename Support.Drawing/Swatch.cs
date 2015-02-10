using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Support.Drawing
{

    public static class Swatch
    {

        #region Enums

        public enum ColorSpace
        {
            Rgb = 0,

            Hsb = 1,

            Cmyk = 2,

            Lab = 7,

            Grayscale = 8
        }

        public enum FileVersion
        {
            Version1 = 1,

            Version2
        }

        #endregion

        #region Private Members

        private static int ReadInt16(Stream stream)
        {
            return (stream.ReadByte() << 8) | (stream.ReadByte() << 0);
        }
        private static int ReadInt32(Stream stream)
        {
            return ((byte)stream.ReadByte() << 24) | ((byte)stream.ReadByte() << 16) | ((byte)stream.ReadByte() << 8) | ((byte)stream.ReadByte() << 0);
        }

        private static UInt16 ReadUInt16(byte[] data, int offset)
        {
            if (BitConverter.IsLittleEndian) Array.Reverse(data, offset, sizeof(UInt16));
            return BitConverter.ToUInt16(data, offset);
        }
        private static UInt32 ReadUInt32(byte[] data, int offset)
        {
            if (BitConverter.IsLittleEndian) Array.Reverse(data, offset, sizeof(UInt32));
            return BitConverter.ToUInt32(data, offset);
        }

        private static Single ReadSingle(byte[] data, int offset)
        {
            if (BitConverter.IsLittleEndian) Array.Reverse(data, offset, sizeof(Single));
            return BitConverter.ToSingle(data, offset);
        }

        private static string ReadString(Stream stream, int length)
        {
            byte[] buffer;
            buffer = new byte[length * 2];
            stream.Read(buffer, 0, buffer.Length);
            return Encoding.BigEndianUnicode.GetString(buffer);
        }

        private static System.Drawing.Color ReadColor(byte[] data, int offset, int block)
        {
            UInt16 lengthName = ReadUInt16(data, offset);
            offset += sizeof(UInt16);

            lengthName *= 2; // turn into a count of bytes, not 16-bit characters

            string Name = Encoding.BigEndianUnicode.GetString(data, offset, lengthName - 2).Trim();
            offset += lengthName;

            string colorModel = Encoding.ASCII.GetString(data, offset, 4).Trim();
            offset += 4;

            if (colorModel != "RGB")
            {
                throw new InvalidDataException("Color \"" + Name + "\" is in " + colorModel + " but this program only does RGB, sorry.");
                //return;
            }

            int r = (int)Math.Ceiling(255.0 * ReadSingle(data, offset));
            offset += sizeof(Single);

            int g = (int)Math.Ceiling(255.0 * ReadSingle(data, offset));
            offset += sizeof(Single);

            int b = (int)Math.Ceiling(255.0 * ReadSingle(data, offset));
            offset += sizeof(Single);

            UInt16 colorType = ReadUInt16(data, offset);
            // I don't care about colorType either.  You might.  See the link at the top of this page.

            return System.Drawing.Color.FromArgb(r, g, b);

            //if (block > 0)
            //    Console.WriteLine(",");
            //Console.Write("   { name: \"" + Name + "\", color: 0x" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2") + " }");


        }

        #endregion

        public static System.Drawing.Color[] ReadSwatchFile(string fileName)
        {
            List<System.Drawing.Color> colorPalette;

            using (Stream stream = File.OpenRead(fileName))
            {
                FileVersion version;

                // read the version, which occupies two bytes
                version = (FileVersion)ReadInt16(stream);

                if (version != FileVersion.Version1 && version != FileVersion.Version2)
                    throw new InvalidDataException("Invalid version information.");

                // the specification states that a version2 palette follows a version1
                // the only difference between version1 and version2 is the inclusion 
                // of a name property. Perhaps there's addtional color spaces as well
                // but we can't support them all anyway
                // I noticed some files no longer include a version 1 palette

                colorPalette = new List<System.Drawing.Color>( ReadSwatches(stream, version));
                if (version == FileVersion.Version1)
                {
                    version = (FileVersion)ReadInt16(stream);
                    if (version == FileVersion.Version2)
                        colorPalette = new List<System.Drawing.Color>( ReadSwatches(stream, version));
                }
            }

            return colorPalette.ToArray();
        }

        public static System.Drawing.Color[] ReadSwatches(Stream stream, FileVersion version)
        {
            int colorCount;
            List<System.Drawing.Color> results;

            results = new List<System.Drawing.Color>();

            // read the number of colors, which also occupies two bytes
            colorCount = ReadInt16(stream);

            for (int i = 0; i < colorCount; i++)
            {
                ColorSpace colorSpace;
                int value1;
                int value2;
                int value3;
                int value4;

                // again, two bytes for the color space
                colorSpace = (ColorSpace)(ReadInt16(stream));

                value1 = ReadInt16(stream);
                value2 = ReadInt16(stream);
                value3 = ReadInt16(stream);
                value4 = ReadInt16(stream);

                if (version == FileVersion.Version2)
                {
                    int length;

                    // need to read the name even though currently our colour collection doesn't support names
                    length = ReadInt32(stream);
                    ReadString(stream, length);
                }

                switch (colorSpace)
                {
                    case ColorSpace.Rgb:
                        int red;
                        int green;
                        int blue;

                        // RGB.
                        // The first three values in the color data are red , green , and blue . They are full unsigned
                        //  16-bit values as in Apple's RGBColor data structure. Pure red = 65535, 0, 0.

                        red = value1 / 256; // 0-255
                        green = value2 / 256; // 0-255
                        blue = value3 / 256; // 0-255

                        results.Add(System.Drawing.Color.FromArgb(red, green, blue));
                        break;

                    case ColorSpace.Hsb:
                        double hue;
                        double saturation;
                        double brightness;

                        // HSB.
                        // The first three values in the color data are hue , saturation , and brightness . They are full 
                        // unsigned 16-bit values as in Apple's HSVColor data structure. Pure red = 0,65535, 65535.

                        hue = value1 / 182.04; // 0-359
                        saturation = value2 / 655.35; // 0-100
                        brightness = value3 / 655.35; // 0-100

                        throw new InvalidDataException(string.Format("Color space '{0}' not supported.", colorSpace));

                    case ColorSpace.Grayscale:

                        int gray;

                        // Grayscale.
                        // The first value in the color data is the gray value, from 0...10000.

                        gray = (int)(value1 / 39.0625); // 0-255

                        results.Add(System.Drawing.Color.FromArgb(gray, gray, gray));
                        break;

                    default:
                        throw new InvalidDataException(string.Format("Color space '{0}' not supported.", colorSpace));
                }
            }

            return results.ToArray();
        }

        public static System.Drawing.Color[] ReadExchangeFile(string fileName)
        {
            List<System.Drawing.Color> colorPalette = new List<System.Drawing.Color>();


            byte[] data;
            data = File.ReadAllBytes(fileName);

            if (data.Length < 12 || data[0] != 'A' || data[1] != 'S' || data[2] != 'E' || data[3] != 'F')
                throw new InvalidDataException("The file \"" + fileName + "\" doesn't appear to be in Adobe Swatch Exchange format.");


            UInt16 versionHigh = ReadUInt16(data, 4);
            UInt16 versionLow = ReadUInt16(data, 6);
            UInt32 blocks = ReadUInt32(data, 8);

            int offset = 12;

            for (int b = 0; b < blocks; b++)
            {
                UInt16 blockType = ReadUInt16(data, offset);
                offset += sizeof(UInt16);

                UInt32 blockLength = ReadUInt32(data, offset);
                offset += sizeof(UInt32);

                switch (blockType)
                {
                    case 0xC001:
                        // Group Start Block (ignored)
                        break;

                    case 0xC002:
                        // Group End Block (ignored)
                        break;

                    case 0x0001:
                        // COLOR!!
                        colorPalette.Add(ReadColor(data, offset, b));
                        break;

                    default:
                        throw new InvalidDataException("Warning: Block " + b + " is of an unknown type 0x" + blockType.ToString("X") + " (file corrupt?)");
                }

                offset += (int)blockLength;
            }

            return colorPalette.ToArray();
        }


    }

}
