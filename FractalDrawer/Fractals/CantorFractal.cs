using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using FractalDrawer.Model;

namespace FractalDrawer.Fractals
{
    /// <summary>
    /// The Cantor fractal.
    /// </summary>
    public class CantorFractal : LineFractal
    {
        /// <summary>
        /// The canvas height.
        /// </summary>
        private const int CanvasHeight = 4000;

        /// <summary>
        /// The canvas width.
        /// </summary>
        private const int CanvasWidth = 8000;

        /// <summary>
        /// The start point.
        /// </summary>
        private static readonly Point StartPoint = new(800, 2000);

        /// <summary>
        /// The end point.
        /// </summary>
        private static readonly Point EndPoint = new(7200, 2000);

        /// <summary>
        /// The fractal line gap length.
        /// </summary>
        private readonly double gapLength;

        /// <summary>
        /// Constructs the Cantor fractal.
        /// </summary>
        /// <param name="depth">The fractal depth.</param>
        /// <param name="startColor">The fractal start color.</param>
        /// <param name="endColor">The fractal end color.</param>
        /// <param name="gapLength">The fractal line gap length.</param>
        public CantorFractal(int depth, Color startColor, Color endColor, double gapLength) : base(
            FractalInfo.CantorFractal, depth, CanvasHeight, CanvasWidth, StartPoint, EndPoint, startColor, endColor)
        {
            this.gapLength = gapLength * 4;
        }

        /// <summary>
        /// Details the segment with more points.
        /// </summary>
        /// <param name="node">The linked list node.</param>
        /// <param name="index">The index of the node.</param>
        protected override void DetailLink(LinkedListNode<Point> node, int index)
        {
            LinkedListNode<Point>? nextNode = node.Next;

            if (nextNode == null)
            {
                return;
            }

            if (index % 2 == 1)
            {
                return;
            }

            Point left = node.Value;
            Point right = nextNode.Value;

            Vector dist = (right - left) / 3;

            Point point1 = left + dist;
            Point point2 = right - dist;

            Points.AddBefore(nextNode, point1);
            Points.AddBefore(nextNode, point2);
        }

        /// <summary>
        /// Draws thr fractal using the points.
        /// </summary>
        protected override void ProcessPoints()
        {
            List<Point> points = Points.ToList();

            for (var i = 0; i <= Depth; i++)
            {
                int interval = 2 << i;
                double offset = gapLength * ((double) Depth / 2 - i);
                int index = Depth - i;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var figure = new PathFigure { IsClosed = false, StartPoint = points[0] };

                    for (var j = 0; j < points.Count; j += interval)
                    {
                        figure.Segments.Add(new LineSegment(points[j + interval - 1], true));
                        if (j + interval < points.Count)
                        {
                            figure.Segments.Add(new LineSegment(points[j + interval], false));
                        }
                    }

                    var geometry = new PathGeometry();
                    geometry.Figures.Add(figure);

                    var path = new Path
                    {
                        Data = geometry,
                        Stroke = ColorGradient[index],
                        StrokeThickness = 8,
                        RenderTransform = new TranslateTransform(0, offset)
                    };

                    Canvas.Children.Add(path);
                });
            }
        }
    }
}