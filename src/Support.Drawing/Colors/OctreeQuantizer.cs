using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing.Colors
{
    public class OctreeQuantizer : IPaletteQuantizer
    {
        public unsafe ColorPalette CreatePalette(Bitmap image, int maxColors, int bitsPerPixel)
        {
            OctreeQuantizer.Node[] array = new OctreeQuantizer.Node[9];
            if ((double)maxColors > System.Math.Pow(2.0, (double)bitsPerPixel))
            {
                throw new Exception(string.Concat(new object[]
                {
                    "param maxColors out of range, maximum ",
                    System.Math.Pow(2.0, (double)bitsPerPixel),
                    " colors for ",
                    bitsPerPixel,
                    " bits"
                }));
            }
            OctreeQuantizer.Node tree = null;
            int i = 0;
            if (bitsPerPixel > 8)
            {
                return null;
            }
            for (int j = 0; j <= bitsPerPixel; j++)
            {
                array[j] = null;
            }
            BitmapData bitmapData = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
            ColorPalette palette;
            try
            {
                int num = bitmapData.Stride - (image.Width * Image.GetPixelFormatSize(image.PixelFormat) + 7) / 8;
                int pixelFormatSize = Image.GetPixelFormatSize(image.PixelFormat);
                if (pixelFormatSize != 16)
                {
                    if (pixelFormatSize != 24)
                    {
                        if (pixelFormatSize != 32)
                        {
                            return null;
                        }
                        uint num2 = 16711680u;
                        uint num3 = 65280u;
                        uint num4 = 255u;
                        int rightShiftCount = this.GetRightShiftCount(num2);
                        int rightShiftCount2 = this.GetRightShiftCount(num3);
                        int rightShiftCount3 = this.GetRightShiftCount(num4);
                        uint* ptr = (uint*)bitmapData.Scan0.ToPointer();
                        for (int k = 0; k < image.Height; k++)
                        {
                            for (int l = 0; l < image.Width; l++)
                            {
                                uint num5 = *(ptr++);
                                byte b = (byte)((num5 & num4) >> rightShiftCount3);
                                byte g = (byte)((num5 & num3) >> rightShiftCount2);
                                byte r = (byte)((num5 & num2) >> rightShiftCount);
                                this.AddColor(ref tree, r, g, b, bitsPerPixel, 0, ref i, ref array);
                                while (i > maxColors)
                                {
                                    this.ReduceTree(bitsPerPixel, ref i, ref array);
                                }
                            }
                            ptr += num / 4;
                        }
                    }
                    else
                    {
                        byte* ptr2 = (byte*)bitmapData.Scan0.ToPointer();
                        for (int m = 0; m < image.Height; m++)
                        {
                            for (int n = 0; n < image.Width; n++)
                            {
                                byte b = *(ptr2++);
                                byte g = *(ptr2++);
                                byte r = *(ptr2++);
                                this.AddColor(ref tree, r, g, b, bitsPerPixel, 0, ref i, ref array);
                                while (i > maxColors)
                                {
                                    this.ReduceTree(bitsPerPixel, ref i, ref array);
                                }
                            }
                            ptr2 += num;
                        }
                    }
                }
                else
                {
                    uint num2 = 31744u;
                    uint num3 = 992u;
                    uint num4 = 31u;
                    int rightShiftCount = this.GetRightShiftCount(num2);
                    int rightShiftCount2 = this.GetRightShiftCount(num3);
                    int rightShiftCount3 = this.GetRightShiftCount(num4);
                    int leftShiftCount = this.GetLeftShiftCount(num2);
                    int leftShiftCount2 = this.GetLeftShiftCount(num3);
                    int leftShiftCount3 = this.GetLeftShiftCount(num4);
                    ushort* ptr3 = (ushort*)bitmapData.Scan0.ToPointer();
                    for (int num6 = 0; num6 < image.Height; num6++)
                    {
                        for (int num7 = 0; num7 < image.Width; num7++)
                        {
                            ushort num8 = *(ptr3++);
                            byte b = (byte)((num8 & (ushort)num4) >> rightShiftCount3 << leftShiftCount3);
                            byte g = (byte)((num8 & (ushort)num3) >> rightShiftCount2 << leftShiftCount2);
                            byte r = (byte)((num8 & (ushort)num2) >> rightShiftCount << leftShiftCount);
                            this.AddColor(ref tree, r, g, b, bitsPerPixel, 0, ref i, ref array);
                            while (i > maxColors)
                            {
                                this.ReduceTree(bitsPerPixel, ref i, ref array);
                            }
                        }
                        ptr3 += num / 2;
                    }
                }
                if (i > maxColors)
                {
                    tree = null;
                }
                Bitmap bitmap = null;
                if (bitsPerPixel != 1)
                {
                    if (bitsPerPixel != 4)
                    {
                        if (bitsPerPixel == 8)
                        {
                            bitmap = new Bitmap(1, 1, PixelFormat.Format8bppIndexed);
                        }
                    }
                    else
                    {
                        bitmap = new Bitmap(1, 1, PixelFormat.Format4bppIndexed);
                    }
                }
                else
                {
                    bitmap = new Bitmap(1, 1, PixelFormat.Format1bppIndexed);
                }
                palette = bitmap.Palette;
                bitmap.Dispose();
                int num9 = 0;
                this.GetPaletteColors(tree, ref palette, ref num9);
                Color[] entries = palette.Entries;
                for (int num10 = num9 + 1; num10 < entries.Length; num10++)
                {
                    entries[num10] = Color.FromArgb(0, 0, 0, 0);
                }
            }
            finally
            {
                if (bitmapData != null)
                {
                    image.UnlockBits(bitmapData);
                }
            }
            return palette;
        }

        private int GetRightShiftCount(uint dwVal)
        {
            for (int i = 0; i < 32; i++)
            {
                if ((dwVal & 1u) == 1u)
                {
                    return i;
                }
                dwVal >>= 1;
            }
            return -1;
        }

        private int GetLeftShiftCount(uint dwVal)
        {
            int num = 0;
            for (int i = 0; i < 32; i++)
            {
                if ((dwVal & 1u) == 1u)
                {
                    num++;
                }
                dwVal >>= 1;
            }
            return 8 - num;
        }

        private void GetPaletteColors(OctreeQuantizer.Node tree, ref ColorPalette palEntries, ref int index)
        {
            Color[] entries = palEntries.Entries;
            if (tree.bIsLeaf)
            {
                entries[index] = Color.FromArgb((int)((byte)((ulong)tree.nRedSum / (ulong)((long)tree.nPixelCount))), (int)((byte)((ulong)tree.nGreenSum / (ulong)((long)tree.nPixelCount))), (int)((byte)((ulong)tree.nBlueSum / (ulong)((long)tree.nPixelCount))));
                index++;
                return;
            }
            for (int i = 0; i < 8; i++)
            {
                if (tree.Child[i] != null)
                {
                    this.GetPaletteColors(tree.Child[i], ref palEntries, ref index);
                }
            }
        }

        private void ReduceTree(int nColorBits, ref int leafCount, ref OctreeQuantizer.Node[] reducibleNodes)
        {
            int i = nColorBits - 1;
            while (i > 0 && reducibleNodes[i] == null)
            {
                i--;
            }
            OctreeQuantizer.Node node = reducibleNodes[i];
            reducibleNodes[i] = node.Next;
            uint num3;
            uint num2;
            uint num = num2 = (num3 = 0u);
            int num4 = 0;
            for (i = 0; i < 8; i++)
            {
                if (node.Child[i] != null)
                {
                    num2 += node.Child[i].nRedSum;
                    num += node.Child[i].nGreenSum;
                    num3 += node.Child[i].nBlueSum;
                    node.nPixelCount += node.Child[i].nPixelCount;
                    node.Child[i] = null;
                    num4++;
                }
            }
            node.bIsLeaf = true;
            node.nRedSum = num2;
            node.nGreenSum = num;
            node.nBlueSum = num3;
            leafCount -= num4 - 1;
        }

        private void AddColor(ref OctreeQuantizer.Node node, byte r, byte g, byte b, int nColorBits, int nLevel, ref int leafCount, ref OctreeQuantizer.Node[] reducibleNodes)
        {
            byte[] array = new byte[]
            {
                128,
                64,
                32,
                16,
                8,
                4,
                2,
                1
            };
            if (node == null)
            {
                node = this.CreateNode(nLevel, nColorBits, ref leafCount, ref reducibleNodes);
            }
            if (node.bIsLeaf)
            {
                node.nPixelCount++;
                node.nRedSum += (uint)r;
                node.nGreenSum += (uint)g;
                node.nBlueSum += (uint)b;
                return;
            }
            int num = 7 - nLevel;
            int num2 = (r & array[nLevel]) >> num << 2 | (g & array[nLevel]) >> num << 1 | (b & array[nLevel]) >> num;
            this.AddColor(ref node.Child[num2], r, g, b, nColorBits, nLevel + 1, ref leafCount, ref reducibleNodes);
        }

        private OctreeQuantizer.Node CreateNode(int nLevel, int nColorBits, ref int leafCount, ref OctreeQuantizer.Node[] reducibleNodes)
        {
            OctreeQuantizer.Node node = new OctreeQuantizer.Node();
            node.bIsLeaf = (nLevel == nColorBits);
            if (node.bIsLeaf)
            {
                leafCount++;
            }
            else
            {
                node.Next = reducibleNodes[nLevel];
                reducibleNodes[nLevel] = node;
            }
            return node;
        }

        private class Node
        {
            public bool bIsLeaf;

            public int nPixelCount;

            public uint nRedSum;

            public uint nGreenSum;

            public uint nBlueSum;

            public OctreeQuantizer.Node[] Child = new OctreeQuantizer.Node[8];

            public OctreeQuantizer.Node Next;
        }
    }
}