using Platform.Support.Windows.User32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Windows.Gdi32
{

    #region Enums

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
    public enum GraphicsMode : int
    {
        Compatible = 1,
        Advanced = 2
    }

    #endregion

    /// <summary>
    /// Imports from Gdi32.dll
    /// </summary>
    public static partial class NativeMethods
    {

        #region Painting and Drawing Functions

        /// <summary>
        /// The GetBoundsRect function obtains the current accumulated bounding rectangle for a specified device context.
        /// The system maintains an accumulated bounding rectangle for each application.An application can retrieve and set this rectangle.
        /// </summary>
        /// <param name="hdc">A handle to the device context whose bounding rectangle the function will return.</param>
        /// <param name="lprcBounds">A pointer to the RECT structure that will receive the current bounding rectangle. The application's rectangle is returned in logical coordinates, and the bounding rectangle is returned in screen coordinates.</param>
        /// <param name="flags">Specifies how the GetBoundsRect function will behave. This parameter can be the following value.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd144854(v=vs.85).aspx</remarks>
        /// <returns>The return value specifies the state of the accumulated bounding rectangle; it can be one of the following values.</returns>
        [DllImport(ExternDll.Gdi32)]
        public static extern uint GetBoundsRect(IntPtr hdc, out RECT lprcBounds, uint flags);

        #endregion

        #region Brush Functions

        /// <summary>
        /// The CreateSolidBrush function creates a logical brush that has the specified solid color.
        /// </summary>
        /// <param name="crColor">The color of the brush. To create a COLORREF color value, use the RGB macro.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd183518(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value identifies a logical brush.
        /// If the function fails, the return value is NULL.
        /// </returns>
        [DllImport(ExternDll.Gdi32)]
        public static extern IntPtr CreateSolidBrush(uint crColor);

        /// <summary>
        /// The CreatePatternBrush function creates a logical brush with the specified bitmap pattern. The bitmap can be a DIB section bitmap, which is created by the CreateDIBSection function, or it can be a device-dependent bitmap.
        /// </summary>
        /// <param name="hbmp">A handle to the bitmap to be used to create the logical brush.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd183508(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value identifies a logical brush.
        /// If the function fails, the return value is NULL.
        /// </returns>
        [DllImport(ExternDll.Gdi32)]
        public static extern IntPtr CreatePatternBrush(IntPtr hbmp);

        #endregion

        #region Device Context Functions

        /// <summary>
        /// The GetDeviceCaps function retrieves device-specific information for the specified device.
        /// </summary>
        /// <param name="hdc">A handle to the DC.</param>
        /// <param name="nIndex">The item to be returned. This parameter can be one of the following values.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd144877(v=vs.85).aspx</remarks>
        /// <returns>
        /// The return value specifies the value of the desired item.
        /// When nIndex is BITSPIXEL and the device has 15bpp or 16bpp, the return value is 16.
        /// </returns>
        [DllImport(ExternDll.Gdi32)]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        /// <summary>
        /// The CreateCompatibleDC function creates a memory device context (DC) compatible with the specified device.
        /// </summary>
        /// <param name="hdc">A handle to an existing DC. If this handle is NULL, the function creates a memory DC compatible with the application's current screen.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd183489(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value is the handle to a memory DC.
        /// If the function fails, the return value is NULL.
        /// </returns>
        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        /// <summary>
        /// The DeleteDC function deletes the specified device context (DC).
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd183533(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.
        /// </returns>
        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);

        /// <summary>
        /// The SelectObject function selects an object into the specified device context (DC). The new object replaces the previous object of the same type.
        /// </summary>
        /// <param name="hdc">A handle to the DC.</param>
        /// <param name="hgdiobj">A handle to the object to be selected. The specified object must have been created by using one of the following functions.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd162957(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the selected object is not a region and the function succeeds, the return value is a handle to the object being replaced. If the selected object is a region and the function succeeds, the return value is one of the following values.
        /// If an error occurs and the selected object is not a region, the return value is NULL. Otherwise, it is HGDI_ERROR.
        /// </returns>
        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        /// <summary>
        /// The GetObject function retrieves information for the specified graphics object.
        /// </summary>
        /// <param name="hObject">A handle to the graphics object of interest. This can be a handle to one of the following: a logical bitmap, a brush, a font, a palette, a pen, or a device independent bitmap created by calling the CreateDIBSection function.</param>
        /// <param name="cbBuffer">The number of bytes of information to be written to the buffer.</param>
        /// <param name="lpvObject">A pointer to a buffer that receives the information about the specified graphics object.
        /// The following table shows the type of information the buffer receives for each type of graphics object you can specify with hgdiobj.
        /// If the lpvObject parameter is NULL, the function return value is the number of bytes required to store the information it writes to the buffer for the specified graphics object.
        /// The address of lpvObject must be on a 4-byte boundary; otherwise, GetObject fails.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd144904(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, and lpvObject is a valid pointer, the return value is the number of bytes stored into the buffer.
        /// If the function succeeds, and lpvObject is NULL, the return value is the number of bytes required to hold the information the function would store into the buffer.
        /// If the function fails, the return value is zero.
        /// </returns>
        [DllImport(ExternDll.Gdi32, CharSet = CharSet.Unicode)]
        public static extern int GetObject(IntPtr hObject, int cbBuffer, IntPtr lpvObject);

        /// <summary>
        /// The DeleteObject function deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. After the object is deleted, the specified handle is no longer valid.
        /// </summary>
        /// <param name="hObject">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd183539(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the specified handle is not valid or is currently selected into a DC, the return value is zero.
        /// </returns>
        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern bool DeleteObject(IntPtr hObject);  // handle to object

        /// <summary>
        /// The CreateDC function creates a device context (DC) for a device using the specified name.
        /// </summary>
        /// <param name="lpszDriver">A pointer to a null-terminated character string that specifies either DISPLAY or the name of a specific display device. For printing, we recommend that you pass NULL to lpszDriver because GDI ignores lpszDriver for printer devices.</param>
        /// <param name="lpszDevice">A pointer to a null-terminated character string that specifies the name of the specific output device being used, as shown by the Print Manager (for example, Epson FX-80). It is not the printer model name. The lpszDevice parameter must be used.
        /// To obtain valid names for displays, call EnumDisplayDevices.
        /// If lpszDriver is DISPLAY or the device name of a specific display device, then lpszDevice must be NULL or that same device name. If lpszDevice is NULL, then a DC is created for the primary display device.
        /// If there are multiple monitors on the system, calling CreateDC(TEXT("DISPLAY"),NULL,NULL,NULL) will create a DC covering all the monitors.</param>
        /// <param name="lpszOutput">This parameter is ignored and should be set to NULL. It is provided only for compatibility with 16-bit Windows.</param>
        /// <param name="lpInitData">A pointer to a DEVMODE structure containing device-specific initialization data for the device driver. The DocumentProperties function retrieves this structure filled in for a specified device. The lpInitData parameter must be NULL if the device driver is to use the default initialization (if any) specified by the user.
        /// If lpszDriver is DISPLAY, lpInitData must be NULL; GDI then uses the display device's current DEVMODE.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd183490(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value is the handle to a DC for the specified device.
        /// If the function fails, the return value is NULL.</returns>
        [DllImport(ExternDll.Gdi32, CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateDC(string lpszDriver, IntPtr lpszDevice, IntPtr lpszOutput, IntPtr lpInitData);

        /// <summary>
        /// The GetLayout function returns the layout of a device context (DC).
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd144896(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, it returns the layout flags for the current device context.
        /// If the function fails, it returns GDI_ERROR.For extended error information, call GetLastError.
        /// </returns>
        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern uint GetLayout(IntPtr hdc);

        #endregion

        #region Bitmap Functions

        /// <summary>
        /// The CreateCompatibleBitmap function creates a bitmap compatible with the device that is associated with the specified device context.
        /// </summary>
        /// <param name="hdc">A handle to a device context.</param>
        /// <param name="nWidth">The bitmap width, in pixels.</param>
        /// <param name="nHeight">The bitmap height, in pixels.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd183488(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the compatible bitmap (DDB).
        /// If the function fails, the return value is NULL.
        /// </returns>
        [DllImport(ExternDll.Gdi32)]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        /// <summary>
        /// The BitBlt function performs a bit-block transfer of the color data corresponding to a rectangle of pixels from the specified source device context into a destination device context.
        /// </summary>
        /// <param name="hdcDest">A handle to the destination device context.</param>
        /// <param name="nXDest">The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
        /// <param name="nYDest">The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
        /// <param name="nWidth">The width, in logical units, of the source and destination rectangles.</param>
        /// <param name="nHeight">The height, in logical units, of the source and the destination rectangles.</param>
        /// <param name="hdcSrc">A handle to the source device context.</param>
        /// <param name="nXSrc">The x-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
        /// <param name="nYSrc">The y-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
        /// <param name="dwRop">A raster-operation code. These codes define how the color data for the source rectangle is to be combined with the color data for the destination rectangle to achieve the final color.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd183370(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.To get extended error information, call GetLastError.
        /// </returns>
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

        #endregion

        #region Painting and Drawing Functions

        /// <summary>
        /// The SetBkMode function sets the background mix mode of the specified device context. The background mix mode is used with text, hatched brushes, and pen styles that are not solid lines.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="bkMode">The background mode. This parameter can be one of the following values.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd162965(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value specifies the previous background mode.
        /// If the function fails, the return value is zero.
        /// </returns>
        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern int SetBkMode(IntPtr hdc, int bkMode);

        /// <summary>
        /// The SetBkColor function sets the current background color to the specified color value, or to the nearest physical color if the device cannot represent the specified color value.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="crColor">The new background color. To make a COLORREF value, use the RGB macro.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd162964(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value specifies the previous background color as a COLORREF value.
        /// If the function fails, the return value is CLR_INVALID.
        /// </returns>
        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern int SetBkColor(IntPtr hdc, int crColor);

        #endregion
        
        #region Coordinate Space and Transformation Functions

        /// <summary>
        /// The SetGraphicsMode function sets the graphics mode for the specified device context.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="iMode">The graphics mode.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd162977(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value is the old graphics mode.
        /// If the function fails, the return value is zero.
        /// </returns>
        [DllImport(ExternDll.Gdi32)]
        public static extern GraphicsMode SetGraphicsMode(IntPtr hdc, GraphicsMode iMode);

        /// <summary>
        /// The SetWorldTransform function sets a two-dimensional linear transformation between world space and page space for the specified device context. This transformation can be used to scale, rotate, shear, or translate graphics output.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="lpXform">A pointer to an XFORM structure that contains the transformation data.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd145104(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.
        /// </returns>
        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern bool SetWorldTransform(IntPtr hdc, [In] ref XFORM lpXform);

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
        public static extern bool GetWorldTransform(IntPtr hdc, [Out] out XFORM lpXform);

        /// <summary>
        /// The GetWindowOrgEx function retrieves the x-coordinates and y-coordinates of the window origin for the specified device context.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="lpPoint">A pointer to a POINT structure that receives the coordinates, in logical units, of the window origin.</param>
        /// <returns>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd144949(v=vs.85).aspx</remarks>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.
        /// </returns>
        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern bool GetWindowOrgEx(IntPtr hdc, out POINT lpPoint);

        /// <summary>
        /// The LPtoDP function converts logical coordinates into device coordinates. The conversion depends on the mapping mode of the device context, the settings of the origins and extents for the window and viewport, and the world transformation.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="lpPoints">A pointer to an array of POINT structures. The x-coordinates and y-coordinates contained in each of the POINT structures will be transformed.</param>
        /// <param name="nCounts">The number of points in the array.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd145042(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.
        /// </returns>
        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern bool LPtoDP(
           IntPtr hdc,
           POINT[] lpPoints,
           int nCounts);

        #endregion

        #region Filled Shape Functions

        /// <summary>
        /// The RoundRect function draws a rectangle with rounded corners. The rectangle is outlined by using the current pen and filled by using the current brush.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="nLeftRect">The x-coordinate, in logical coordinates, of the upper-left corner of the rectangle.</param>
        /// <param name="nTopRect">The y-coordinate, in logical coordinates, of the upper-left corner of the rectangle.</param>
        /// <param name="nRightRect">The x-coordinate, in logical coordinates, of the lower-right corner of the rectangle.</param>
        /// <param name="nBottomRect">The y-coordinate, in logical coordinates, of the lower-right corner of the rectangle.</param>
        /// <param name="nWidth">The width, in logical coordinates, of the ellipse used to draw the rounded corners.</param>
        /// <param name="nHeight">The height, in logical coordinates, of the ellipse used to draw the rounded corners.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd162944(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.
        /// </returns>
        [DllImport(ExternDll.Gdi32)]
        public static extern bool RoundRect(IntPtr hdc, int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect, int nWidth, int nHeight);

        #endregion

        #region Region Functions

        /// <summary>
        /// The CreateRoundRectRgn function creates a rectangular region with rounded corners.
        /// </summary>
        /// <param name="nLeftRect">Specifies the x-coordinate of the upper-left corner of the region in device units.</param>
        /// <param name="nTopRect">Specifies the y-coordinate of the upper-left corner of the region in device units.</param>
        /// <param name="nRightRect">Specifies the x-coordinate of the lower-right corner of the region in device units.</param>
        /// <param name="nBottomRect">Specifies the y-coordinate of the lower-right corner of the region in device units.</param>
        /// <param name="nWidthEllipse">Specifies the width of the ellipse used to create the rounded corners in device units.</param>
        /// <param name="nHeightEllipse">Specifies the height of the ellipse used to create the rounded corners in device units.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd183516(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value is the handle to the region.
        /// If the function fails, the return value is NULL.
        /// </returns>
        [DllImport(ExternDll.Gdi32)]
        public static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,      // x-coordinate of upper-left corner
            int nTopRect,       // y-coordinate of upper-left corner
            int nRightRect,     // x-coordinate of lower-right corner
            int nBottomRect,    // y-coordinate of lower-right corner
            int nWidthEllipse,  // height of ellipse
            int nHeightEllipse  // width of ellipse
            );

        #endregion

        #region Font and Text Functions

        /// <summary>
        /// The SetTextColor function sets the text color for the specified device context to the specified color.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="crColor">The color of the text.</param>
        /// <remarks>https://msdn.microsoft.com/es-es/library/windows/desktop/dd145093(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value is a color reference for the previous text color as a COLORREF value.
        /// If the function fails, the return value is CLR_INVALID.
        /// </returns>
        [DllImport(ExternDll.Gdi32, SetLastError = true)]
        public static extern int SetTextColor(IntPtr hdc, int crColor);

        /// <summary>
        /// The GetTextExtentPoint32 function computes the width and height of the specified string of text.
        /// </summary>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="lpString">A pointer to a buffer that specifies the text string. The string does not need to be null-terminated, because the c parameter specifies the length of the string.</param>
        /// <param name="cbString">The length of the string pointed to by lpString.</param>
        /// <param name="lpSize">A pointer to a SIZE structure that receives the dimensions of the string, in logical units.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/dd144938(v=vs.85).aspx</remarks>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.
        /// </returns>
        [DllImport(ExternDll.Gdi32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool GetTextExtentPoint32(
            IntPtr hdc,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpString,
            int cbString,
            out SIZE lpSize
            );

        #endregion

        public static int ToColorRef(Color color)
        {
            return (color.B << 16) | (color.G << 8) | color.R;
        }

    }

    #region Structures

    public struct XFORM
    {
        public float eM11;
        public float eM12;
        public float eM21;
        public float eM22;
        public float eDx;
        public float eDy;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct SIZEL
    {
        public int cx;
        public int cy;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct POINTL
    {
        public int x;
        public int y;
    }

    public struct DEVICECAPS
    {
        public const int LOGPIXELSX = 88;
        public const int LOGPIXELSY = 90;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct BITMAP
    {
        public int bmType;
        public int bmWidth;
        public int bmHeight;
        public int bmWidthBytes;
        public short bmPlanes;
        public short bmBitsPixel;
        public int bmBits;
    }

    #endregion

}
