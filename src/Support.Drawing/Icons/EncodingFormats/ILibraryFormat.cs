using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing.Icons.EncodingFormats
{
    public interface ILibraryFormat
    {
        bool IsRecognizedFormat(Stream stream);

        void Save(MultiIcon singleIcon, Stream stream);

        MultiIcon Load(Stream stream);
    }
}