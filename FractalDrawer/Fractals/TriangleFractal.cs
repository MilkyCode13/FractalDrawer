using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using FractalDrawer.Model;

namespace FractalDrawer.Fractals
{
    /// <summary>
    /// The triangle fractal.
    /// </summary>
    public class TriangleFractal : AdditiveFractal<ShapeDrawInfo>
    {
        /// <summary>
        /// The factor of rise of complexity of each iteration.
        /// </summary>
        private const int MultiplyFactor = 3;

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
        private const double StartY = 4800;

        /// <summary>
        /// The starting size.
        /// </summary>
        private const double StartSize = 1840;

        /// <summary>
        /// An offset Y ratio.
        /// </summary>
        private const double OffsetYRatio = 0.5;

        /// <summary>
        /// An offset X ratio.
        /// </summary>
        private static readonly double OffsetXRatio = Math.Sqrt(3) / 2;

        /// <summary>
        /// The initial drawer parameters for zeroth iteration.
        /// </summary>
        private static readonly ShapeDrawInfo StartParameters = new(0, StartX, StartY, StartSize);

        /// <summary>
        /// Constructs the triangle fractal.
        /// </summary>
        /// <param name="depth">The fractal depth.</param>
        /// <param name="startColor">The start color.</param>
        /// <param name="endColor">The end color.</param>
        public TriangleFractal(int depth, Color startColor, Color endColor) : base(FractalInfo.TriangleFractal, depth,
            CanvasHeight, CanvasWidth, MultiplyFactor, StartParameters, startColor, endColor)
        {
        }

        /// <summary>
        /// Draws the fractal base shape (so-called zeroth iteration).
        /// </summary>
        /// <param name="drawInfo">The drawing info.</param>
        protected override void DrawBase(ShapeDrawInfo drawInfo)
        {
            (int iteration, double x, double y, double size) = drawInfo;

            var point1 = new Point(x, y - 2 * size);
            var point2 = new Point(x - 2 * size * OffsetXRatio, y + 2 * size * OffsetYRatio);
            var point3 = new Point(x + 2 * size * OffsetXRatio, y + 2 * size * OffsetYRatio);

            Application.Current.Dispatcher.Invoke(() =>
            {
                var triangle = new Polygon
                {
                    Points = new PointCollection(new[] { point1, point2, point3 }),
                    Stroke = ColorGradient[iteration],
                    StrokeLineJoin = PenLineJoin.Bevel,
                    StrokeThickness = 4
                };

                Canvas.Children.Add(triangle);
            });
        }

        /// <summary>
        /// Draws an iteration.
        /// </summary>
        protected override void DrawIteration()
        {
            (int iteration, double x, double y, double size) = DrawQueue.Dequeue();

            var point1 = new Point(x, y + size);
            var point2 = new Point(x - size * OffsetXRatio, y - size * OffsetYRatio);
            var point3 = new Point(x + size * OffsetXRatio, y - size * OffsetYRatio);

            Application.Current.Dispatcher.Invoke(() =>
            {
                var triangle = new Polygon()
                {
                    Points = new PointCollection(new[] { point1, point2, point3 }),
                    Stroke = ColorGradient[iteration],
                    StrokeLineJoin = PenLineJoin.Bevel,
                    StrokeThickness = 4
                };

                Canvas.Children.Add(triangle);
            });

            double newSize = size / 2;

            DrawQueue.Enqueue(new ShapeDrawInfo(iteration + 1, x, y - size, newSize));
            DrawQueue.Enqueue(new ShapeDrawInfo(
                iteration + 1, x - OffsetXRatio * size, y + OffsetYRatio * size, newSize));
            DrawQueue.Enqueue(new ShapeDrawInfo(
                iteration + 1, x + OffsetXRatio * size, y + OffsetYRatio * size, newSize));
        }
    }
}