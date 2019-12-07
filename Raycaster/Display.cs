using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Raycaster
{
	public static class Display
	{
		public static WriteableBitmap bmp;

		public static Image image;

		public static void Init (Image img)
		{
			image = img;
			Debug.WriteLine("W: " + image.Width + " H: " + image.Height);
			// Initialize the Bitmap buffer
			bmp = new WriteableBitmap((int)image.Width, (int)image.Height, 96, 96, PixelFormats.Bgr32, null);
			// Set the image source
			image.Source = bmp;

		}
		
		// Source: https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.writeablebitmap?view=netframework-4.8 
		
		public static void DrawPixel(int x, int y, Color c)
		{
			int column = x;
			int row = y;

			try
			{
				// Reserve the back buffer for updates.
				bmp.Lock();

				unsafe
				{
					// Get a pointer to the back buffer.
					IntPtr pBackBuffer = bmp.BackBuffer;

					// Find the address of the pixel to draw.
					pBackBuffer += row * bmp.BackBufferStride;
					pBackBuffer += column * 4;

					// Compute the pixel's color.
					int color_data = (int)(c.r * 255) << 16; // R
					color_data |= (int)(c.g * 255) << 8;   // G
					color_data |= (int)(c.b * 255) << 0;   // B

					// Assign the color data to the pixel.
					*((int*)pBackBuffer) = color_data;
				}

				// Specify the area of the bitmap that changed.
				bmp.AddDirtyRect(new Int32Rect(column, row, 1, 1));
			}
			finally
			{
				// Release the back buffer and make it available for display.
				bmp.Unlock();
			}

		}
	}
}
