using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing.Icons.Exceptions
{
    public class IconNameAlreadyExistException : Exception
    {
        public IconNameAlreadyExistException() : base("Icon name already exist in the collection")
        {
        }
    }

    public class ImageAlreadyExistsException : Exception
    {
        public ImageAlreadyExistsException() : base("Image with same size and format already exists")
        {
        }
    }

    public class ImageTooBigException : Exception
    {
        public ImageTooBigException() : base("Image width and height cannot be bigger than 256 pixels.")
        {
        }
    }

    public class InvalidFileException : Exception
    {
        public InvalidFileException() : base("Format not recognized by IconLib")
        {
        }
    }

    public class InvalidICLFileException : Exception
    {
        public InvalidICLFileException() : base("Invalid ICL file.")
        {
        }
    }

    public class InvalidIconFormatSelectionException : Exception
    {
        public InvalidIconFormatSelectionException() : base("Invalid IconImageFormat selection")
        {
        }
    }

    public class InvalidIconSelectionException : Exception
    {
        public InvalidIconSelectionException() : base("Selected Icon is invalid")
        {
        }
    }

    public class InvalidMultiIconFileException : Exception
    {
        public InvalidMultiIconFileException() : base("Invalid icon file. Signature does not match")
        {
        }
    }

    public class InvalidMultiIconMaskBitmap : Exception
    {
        public InvalidMultiIconMaskBitmap() : base("Invalid mask bitmap. Mask must be same size as the bitmap and PixelFormat must be Format1bppIndexed")
        {
        }
    }

    public class InvalidPixelFormatException : Exception
    {
        public InvalidPixelFormatException(PixelFormat invalid, PixelFormat expected) : base((invalid != PixelFormat.Undefined) ? ("PixelFormat " + invalid.ToString() + " is invalid") : ((expected != PixelFormat.Undefined) ? ("PixelFormat " + expected.ToString() + " expected") : "Invalid PixelFormat"))
        {
        }
    }
}