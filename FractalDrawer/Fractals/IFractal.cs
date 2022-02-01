using System.Windows.Controls;

namespace FractalDrawer.Fractals
{
    /// <summary>
    /// Represents a fractal.
    /// </summary>
    public interface IFractal
    {
        /// <summary>
        /// Draws the fractal.
        /// </summary>
        /// <returns>Canvas with the fractal drawn.</returns>
        public Canvas Draw();
    }
}