﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Drawing;

namespace Platform.Support.Drawing.Geometrics
{
    /// <summary>
    /// Rectangle operation helper.
    /// </summary>
    public sealed class Rectangle
    {
        /// <summary>
        /// Initializes a new instance of the RectangleHelper class.
        /// </summary>
        private Rectangle()
        {
        }

        /// <summary>
        /// Creates a rectangle from two points.
        /// </summary>
        /// <param name="topLeft">Top left point of the rectangle.</param>
        /// <param name="bottomRight">Bottom right point of the rectangle.</param>
        /// <returns>New Rectangle structure.</returns>
        public static System.Drawing.Rectangle Create(Point topLeft, Point bottomRight)
        {
            return new System.Drawing.Rectangle(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
        }

        /// <summary>
        /// Adjusts the specified rectangle structure by the specified x and y amount and returns
        /// a new rectangle structure.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static System.Drawing.Rectangle Adjust(System.Drawing.Rectangle rectangle, int x, int y)
        {
            return new System.Drawing.Rectangle(rectangle.X + x,
                                    rectangle.Y + y,
                                     System.Math.Max(0, rectangle.Width - x),
                                    System.Math.Max(0, rectangle.Height - y));
        }

        /// <summary>
        /// Returns the top half of the specified rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to return the top half of.</param>
        /// <returns>A new rectangle representing the top half of the input rectangle.</returns>
        public static System.Drawing.Rectangle TopHalf(System.Drawing.Rectangle rectangle)
        {
            return new System.Drawing.Rectangle(rectangle.X,
                                    rectangle.Y,
                                    rectangle.Width,
                                    rectangle.Height / 2);
        }

        /// <summary>
        /// Returns the bottom half of the specified rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to return the bottom half of.</param>
        /// <returns>A new rectangle representing the bottom half of the input rectangle.</returns>
        public static System.Drawing.Rectangle BottomHalf(System.Drawing.Rectangle rectangle)
        {
            int height = rectangle.Height / 2;
            return new System.Drawing.Rectangle(rectangle.X,
                                    rectangle.Y + height,
                                    rectangle.Width,
                                    rectangle.Height - height);
        }

        /// <summary>
        /// Pins a rectangle to the top-left of another rectangle.
        /// </summary>
        /// <param name="rectangleToPin">Rectangle to pin.</param>
        /// <param name="rectangleToPinTo">Rectangle to pin to.</param>
        /// <returns>Pinned rectangle.</returns>
        public static System.Drawing.Rectangle PinTopLeft(System.Drawing.Rectangle rectangleToPin, System.Drawing.Rectangle rectangleToPinTo)
        {
            return new System.Drawing.Rectangle(rectangleToPinTo.X,
                                    rectangleToPinTo.Y,
                                    rectangleToPin.Width,
                                    rectangleToPin.Height);
        }

        /// <summary>
        /// Pins a rectangle to the top-right of another rectangle.
        /// </summary>
        /// <param name="rectangleToPin">Rectangle to pin.</param>
        /// <param name="rectangleToPinTo">Rectangle to pin to.</param>
        /// <returns>Pinned rectangle.</returns>
        public static System.Drawing.Rectangle PinTopRight(System.Drawing.Rectangle rectangleToPin, System.Drawing.Rectangle rectangleToPinTo)
        {
            return new System.Drawing.Rectangle(rectangleToPinTo.Right - rectangleToPin.Width,
                                    rectangleToPinTo.Y,
                                    rectangleToPin.Width,
                                    rectangleToPin.Height);
        }

        /// <summary>
        /// Pins a rectangle to the bottom-right of another rectangle.
        /// </summary>
        /// <param name="rectangleToPin">Rectangle to pin.</param>
        /// <param name="rectangleToPinTo">Rectangle to pin to.</param>
        /// <returns>Pinned rectangle.</returns>
        public static System.Drawing.Rectangle PinBottomRight(System.Drawing.Rectangle rectangleToPin, System.Drawing.Rectangle rectangleToPinTo)
        {
            return new System.Drawing.Rectangle(rectangleToPinTo.Right - rectangleToPin.Width,
                                    rectangleToPinTo.Bottom - rectangleToPin.Height,
                                    rectangleToPin.Width,
                                    rectangleToPin.Height);
        }

        /// <summary>
        /// Pins a rectangle to the bottom-left of another rectangle.
        /// </summary>
        /// <param name="rectangleToPin">Rectangle to pin.</param>
        /// <param name="rectangleToPinTo">Rectangle to pin to.</param>
        /// <returns>Pinned rectangle.</returns>
        public static System.Drawing.Rectangle PinBottomLeft(System.Drawing.Rectangle rectangleToPin, System.Drawing.Rectangle rectangleToPinTo)
        {
            return new System.Drawing.Rectangle(rectangleToPinTo.X,
                                    rectangleToPinTo.Bottom - rectangleToPin.Height,
                                    rectangleToPin.Width,
                                    rectangleToPin.Height);
        }

        /// <summary>
        /// Convert a Win32 RECT structure into a .NET rectangle
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static System.Drawing.Rectangle Convert(RECT rect)
        {
            return new System.Drawing.Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
        }

        /// <summary>
        /// Convert a .NET rectangle structure into a Win32 RECT structure
        /// </summary>
        /// <param name="rect">rect to convert</param>
        /// <returns>converted rect</returns>
        public static RECT Convert(System.Drawing.Rectangle rect)
        {
            var newRect = new RECT
            {
                left = rect.Left,
                top = rect.Top,
                right = rect.Right,
                bottom = rect.Bottom
            };
            return newRect;
        }

        public static System.Drawing.Rectangle Center(Size desiredSize, System.Drawing.Rectangle anchor, bool shrinkIfNecessary)
        {
            int dW = anchor.Width - desiredSize.Width;
            int dH = anchor.Height - desiredSize.Height;
            int width = desiredSize.Width;
            int height = desiredSize.Height;

            if (shrinkIfNecessary && dW < 0)
            {
                dW = 0;
                width = anchor.Width;
            }
            if (shrinkIfNecessary && dH < 0)
            {
                dH = 0;
                height = anchor.Height;
            }

            return new System.Drawing.Rectangle(anchor.Left + dW / 2, anchor.Top + dH / 2, width, height);
        }

        public static System.Drawing.Rectangle RotateFlip(Size container, System.Drawing.Rectangle rect, RotateFlipType rotateFlip)
        {
            bool rotate = false;
            bool flipX = false;
            bool flipY = false;

            switch (rotateFlip)
            {
                case RotateFlipType.RotateNoneFlipNone:
                    return rect;

                case RotateFlipType.RotateNoneFlipX:
                    flipX = true;
                    break;

                case RotateFlipType.RotateNoneFlipXY:
                    flipX = flipY = true;
                    break;

                case RotateFlipType.RotateNoneFlipY:
                    flipY = true;
                    break;

                case RotateFlipType.Rotate90FlipNone:
                    rotate = true;
                    break;

                case RotateFlipType.Rotate90FlipX:
                    rotate = flipX = true;
                    break;

                case RotateFlipType.Rotate90FlipXY:
                    rotate = flipX = flipY = true;
                    break;

                case RotateFlipType.Rotate90FlipY:
                    rotate = flipY = true;
                    break;
            }

            if (flipX)
                rect.X = container.Width - rect.Right;
            if (flipY)
                rect.Y = container.Height - rect.Bottom;
            if (rotate)
            {
                Point p = new Point(rect.Left, rect.Bottom);
                rect.Location = new Point(container.Height - p.Y, p.X);

                int oldWidth = rect.Width;
                rect.Width = rect.Height;
                rect.Height = oldWidth;
            }

            return rect;
        }

        public static System.Drawing.Rectangle UndoRotateFlip(Size container, System.Drawing.Rectangle rect, RotateFlipType rotateFlip)
        {
            switch (rotateFlip)
            {
                case RotateFlipType.RotateNoneFlipNone:
                    return rect;

                case RotateFlipType.RotateNoneFlipX:
                    rotateFlip = RotateFlipType.RotateNoneFlipX;
                    break;

                case RotateFlipType.RotateNoneFlipXY:
                    rotateFlip = RotateFlipType.RotateNoneFlipXY;
                    break;

                case RotateFlipType.RotateNoneFlipY:
                    rotateFlip = RotateFlipType.RotateNoneFlipY;
                    break;

                case RotateFlipType.Rotate90FlipNone:
                    rotateFlip = RotateFlipType.Rotate270FlipNone;
                    break;

                case RotateFlipType.Rotate90FlipX:
                    rotateFlip = RotateFlipType.Rotate270FlipX;
                    break;

                case RotateFlipType.Rotate90FlipXY:
                    rotateFlip = RotateFlipType.Rotate270FlipXY;
                    break;

                case RotateFlipType.Rotate90FlipY:
                    rotateFlip = RotateFlipType.Rotate270FlipY;
                    break;
            }
            return RotateFlip(container, rect, rotateFlip);
        }

        public static System.Drawing.Rectangle EnforceAspectRatio(System.Drawing.Rectangle bounds, float aspectRatio)
        {
            Size originalSize = bounds.Size;

            float portalAspectRatio = aspectRatio;
            float imageAspectRatio = originalSize.Width / (float)originalSize.Height;

            System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(Point.Empty, originalSize);
            if (imageAspectRatio < portalAspectRatio)
            {
                srcRect.Height = System.Convert.ToInt32(originalSize.Width / portalAspectRatio);
                srcRect.Y = System.Math.Max(0, (originalSize.Height - srcRect.Height) / 2);
                srcRect.Height = System.Math.Min(srcRect.Height, originalSize.Height - srcRect.Y);
            }
            else
            {
                srcRect.Width = System.Convert.ToInt32(originalSize.Height * portalAspectRatio);
                srcRect.X = System.Math.Max(0, (originalSize.Width - srcRect.Width) / 2);
                srcRect.Width = System.Math.Min(srcRect.Width, originalSize.Width - srcRect.X);
            }

            srcRect.Offset(bounds.Location);
            return srcRect;
        }
    }

    /// <summary>
    /// Windows RECT structure
    /// </summary>
    public struct RECT
    {
        public Int32 left;
        public Int32 top;
        public Int32 right;
        public Int32 bottom;

        public static implicit operator System.Drawing.Rectangle(RECT rect)
        {
            return System.Drawing.Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
        }

        public static implicit operator RECT(System.Drawing.Rectangle rectangle)
        {
            var rect = new RECT
            {
                left = rectangle.Left,
                top = rectangle.Top,
                right = rectangle.Right,
                bottom = rectangle.Bottom
            };
            return rect;
        }

        public int Width { get { return right - left; } }
        public int Height { get { return bottom - top; } }
    }
}