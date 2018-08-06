using Platform.Support.Drawing.Icons;
using Platform.Support.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing
{
    public static partial class Extensions
    {
        internal static Dictionary<ushort, IconImage> GetIcons(this RESOURCE_TABLE @this, Stream stream)
        {
            Dictionary<ushort, IconImage> dictionary = new Dictionary<ushort, IconImage>();
            for (int i = 0; i < @this.rscTypes.Length; i++)
            {
                if (@this.rscTypes[i].ResourceType == ResourceType.RT_ICON)
                {
                    string[] resourceNames = @this.ResourceNames;
                    for (int j = 0; j < @this.rscTypes[i].rtNameInfo.Length; j++)
                    {
                        stream.Seek((long)((1 << (int)@this.rscAlignShift) * (int)@this.rscTypes[i].rtNameInfo[j].rnOffset), SeekOrigin.Begin);
                        dictionary.Add(@this.rscTypes[i].rtNameInfo[j].ID, new IconImage(stream, (1 << (int)@this.rscAlignShift) * (int)@this.rscTypes[i].rtNameInfo[j].rnLength));
                    }
                    break;
                }
            }
            return dictionary;
        }

        internal static void SetIcons(this RESOURCE_TABLE @this, Stream stream, Dictionary<ushort, IconImage> icons)
        {
            for (int i = 0; i < @this.rscTypes.Length; i++)
            {
                if (@this.rscTypes[i].ResourceType == ResourceType.RT_ICON)
                {
                    string[] resourceNames = @this.ResourceNames;
                    for (int j = 0; j < @this.rscTypes[i].rtNameInfo.Length; j++)
                    {
                        stream.Seek((long)((1 << (int)@this.rscAlignShift) * (int)@this.rscTypes[i].rtNameInfo[j].rnOffset), SeekOrigin.Begin);
                        icons[@this.rscTypes[i].rtNameInfo[j].ID].Write(stream);
                    }
                    return;
                }
            }
        }

        public static Icon AsIcon(this Image image, int size = 32)
        {
            var bitmapResult = (Bitmap)image.GetThumbnailImage(size, size, null, new IntPtr());
            var result = System.Drawing.Icon.FromHandle(bitmapResult.GetHicon());
            return result;
        }
    }
}