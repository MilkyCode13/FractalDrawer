using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using FractalDrawer.Model;

namespace FractalDrawer.Fractals
{
    /// <summary>
    /// The carpet fractal.
    /// </summary>
    public class CarpetFractal : AdditiveFractal<ShapeDrawInfo>
    {
        /// <summary>
        /// The factor of rise of complexity of each iteration.
        /// </summary>
        private const int MultiplyFactor = 8;

        /// <summary>
        /// The canvas height.
        /// </summary>
        private const int CanvasHeight = 4000;

        /// <summary>
        /// The canvas width.
        /// </summary>
        private const int CanvasWidth = 4000;

        /// <summary>
        /// The starting X coordinate.
        /// </summary>
        private const double StartX = 2000;

        /// <summary>
        /// The starting Y coordinate.
        /// </summary>
        private const double StartY = 2000;

        /// <summary>
        /// The starting shape size.
        /// </summary>
        private const double StartSize = 1000;

        /// <summary>
        /// The initial drawer parameters for zeroth iteration.
        /// </summary>
        private static readonly ShapeDrawInfo StartParameters = new(0, StartX, StartY, StartSize);

        /// <summary>
        /// Constructs a carpet fractal.
        /// </summary>
        /// <param name="depth">The fractal depth.</param>
        /// <param name="startColor">The start color.</param>
        /// <param name="endColor">The end color.</param>
        public CarpetFractal(int depth, Color startColor, Color endColor) : base(FractalInfo.CarpetFractal, depth,
            CanvasHeight, CanvasWidth, MultiplyFactor, StartParameters, startColor, endColor)
        {
        }

        /// <summary>
        /// Draws the fractal base shape (so-called zeroth iteration).
        /// </summary>
        /// <param name="drawInfo">The drawing info.</param>
        protected override void DrawBase(ShapeDrawInfo drawInfo)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var square = new Rectangle
                {
                    Height = drawInfo.Size * 3,
                    Width = drawInfo.Size * 3,
                    Fill = new SolidColorBrush(Color.FromRgb(50, 0, 87))
                };

                Canvas.Children.Add(square);
                Canvas.SetLeft(square, drawInfo.X - drawInfo.Size * 1.5);
                Canvas.SetTop(square, drawInfo.Y - drawInfo.Size * 1.5);
            });
        }

        /// <summary>
        /// Draws an iteration.
        /// </summary>
        protected override void DrawIteration()
        {
            (int iteration, double x, double y, double size) = DrawQueue.Dequeue();

            Application.Current.Dispatcher.Invoke(() =>
            {
                var square = new Rectangle
                {
                    Height = size,
                    Width = size,
                    Fill = ColorGradient[iteration]
                };

                Canvas.Children.Add(square);
                Canvas.SetLeft(square, x - size / 2);
                Canvas.SetTop(square, y - size / 2);
            });

            double newSize = size / 3;

            DrawQueue.Enqueue(new ShapeDrawInfo(iteration + 1, x - size, y - size, newSize));
            DrawQueue.Enqueue(new ShapeDrawInfo(iteration + 1, x, y - size, newSize));
            DrawQueue.Enqueue(new ShapeDrawInfo(iteration + 1, x + size, y - size, newSize));
            DrawQueue.Enqueue(new ShapeDrawInfo(iteration + 1, x - size, y, newSize));
            DrawQueue.Enqueue(new ShapeDrawInfo(iteration + 1, x + size, y, newSize));
            DrawQueue.Enqueue(new ShapeDrawInfo(iteration + 1, x - size, y + size, newSize));
            DrawQueue.Enqueue(new ShapeDrawInfo(iteration + 1, x, y + size, newSize));
            DrawQueue.Enqueue(new ShapeDrawInfo(iteration + 1, x + size, y + size, newSize));
        }
    }
}