using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using FractalDrawer.Model;

namespace FractalDrawer.Fractals
{
    /// <summary>
    /// The tree fractal.
    /// </summary>
    public class TreeFractal : AdditiveFractal<LineDrawInfo>
    {
        /// <summary>
        /// The factor of rise of complexity of each iteration.
        /// </summary>
        private const int MultiplyFactor = 2;

        /// <summary>
        /// The fractal segment scale factor.
        /// </summary>
        private readonly double scaleFactor;

        /// <summary>
        /// The canvas height.
        /// </summary>
        private const int CanvasHeight = 8000;

        /// <summary>
        /// The canvas width.
        /// </summary>
        private const int CanvasWidth = 8000;

        /// <summary>
        /// The starting X coordinate.
        /// </summary>
        private const double StartX = 4000;

        /// <summary>
        /// The starting Y coordinate.
        /// </summary>
        private const double StartY = 6400;

        /// <summary>
        /// The starting segment length.
        /// </summary>
        private const double StartLength = 1600;

        /// <summary>
        /// The starting segment angle.
        /// </summary>
        private const double StartAngle = Math.PI / 2;

        /// <summary>
        /// The fractal left segment angle.
        /// </summary>
        private readonly double leftAngle;

        /// <summary>
        /// The fractal right segment angle.
        /// </summary>
        private readonly double rightAngle;

        /// <summary>
        /// The initial drawer parameters for zeroth iteration.
        /// </summary>
        private static readonly LineDrawInfo StartParameters = new(0, StartX, StartY, StartAngle, StartLength);

        /// <summary>
        /// Constructs the tree fractal.
        /// </summary>
        /// <param name="depth">The fractal depth.</param>
        /// <param name="startColor">The start color.</param>
        /// <param name="endColor">The end color.</param>
        /// <param name="scaleFactor">The fractal segment scale factor.</param>
        /// <param name="leftAngle">The fractal left segment angle.</param>
        /// <param name="rightAngle">The fractal right segment angle.</param>
        public TreeFractal(int depth, Color startColor, Color endColor, double scaleFactor, double leftAngle,
            double rightAngle) : base(FractalInfo.TreeFractal, depth, CanvasHeight, CanvasWidth, MultiplyFactor,
            StartParameters, startColor, endColor)
        {
            this.scaleFactor = scaleFactor;
            this.leftAngle = leftAngle;
            this.rightAngle = rightAngle;
        }

        /// <summary>
        /// Draws an iteration.
        /// </summary>
        protected override void DrawIteration()
        {
            (int iteration, double x, double y, double angle, double length) = DrawQueue.Dequeue();

            double newX = x + length * Math.Cos(angle);
            double newY = y - length * Math.Sin(angle);

            Application.Current.Dispatcher.Invoke(() =>
            {
                var line = new Line
                {
                    X1 = x,
                    Y1 = y,
                    X2 = newX,
                    Y2 = newY,
                    Stroke = ColorGradient[iteration],
                    StrokeThickness = 8
                };

                Canvas.Children.Add(line);
            });

            DrawQueue.Enqueue(new LineDrawInfo(
                iteration + 1, newX, newY, angle - rightAngle, length / scaleFactor));
            DrawQueue.Enqueue(new LineDrawInfo(
                iteration + 1, newX, newY, angle + leftAngle, length / scaleFactor));
        }
    }
}