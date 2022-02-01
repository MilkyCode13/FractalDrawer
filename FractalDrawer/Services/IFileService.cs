using System.Windows.Controls;

namespace FractalDrawer.Services
{
    /// <summary>
    /// Represents the file service.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Saves the image to the file.
        /// </summary>
        /// <param name="filename">Name of the file to save to.</param>
        /// <param name="canvas">Canvas containing the image to save.</param>
        public void Save(string filename, Canvas canvas);
    }
}