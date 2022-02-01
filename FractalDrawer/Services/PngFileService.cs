using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FractalDrawer.Services
{
    /// <summary>
    /// Provides the PNG file service.
    /// </summary>
    public class PngFileService : IFileService
    {
        /// <summary>
        /// Saves the image to the PNG file.
        /// </summary>
        /// <param name="filename">Name of the file to save to.</param>
        /// <param name="canvas">Canvas containing the image to save.</param>
        public void Save(string filename, Canvas canvas)
        {
            var renderTargetBitmap = new RenderTargetBitmap(
                (int) canvas.RenderSize.Width, (int) canvas.RenderSize.Height, 96, 96,
                PixelFormats.Default);
            renderTargetBitmap.Render(canvas);

            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            using FileStream fs = File.OpenWrite(filename);
            pngEncoder.Save(fs);
        }
    }
}