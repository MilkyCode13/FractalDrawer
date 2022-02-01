using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using FractalDrawer.Model;

namespace FractalDrawer.Fractals
{
    /// <summary>
    /// The Koch fractal.
    /// </summary>
    public class KochFractal : LineFractal
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
        /// The real part of rotation number.
        /// </summary>
        private const double RotationReal = 0.5;

        /// <summary>
        /// The imaginary part of rotation number.
        /// </summary>
        private static readonly double RotationImaginary = -Math.Sqrt(3) / 2;

        /// <summary>
        /// The start point.
        /// </summary>
        private static readonly Point StartPoint = new(800, 3200);

        /// <summary>
        /// The end point.
        /// </summary>
        private static readonly Point EndPoint = new(7200, 3200);

        public KochFractal(int depth, Color startColor, Color endColor) : base(
            FractalInfo.KochFractal, depth, CanvasHeight, CanvasWidth, StartPoint, EndPoint, startColor, endColor)
        {
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

            Point left = node.Value;
            Point right = nextNode.Value;

            Vector dist = (right - left) / 3;
            var rotated = new Vector(dist.X * RotationReal - dist.Y * RotationImaginary,
                dist.Y * RotationReal + dist.X * RotationImaginary);

            Point point1 = left + dist;
            Point point2 = point1 + rotated;
            Point point3 = right - dist;

            Points.AddBefore(nextNode, point1);
            Points.AddBefore(nextNode, point2);
            Points.AddBefore(nextNode, point3);
        }

        /// <summary>
        /// Draws thr fractal using the points.
        /// </summary>
        protected override void ProcessPoints()
        {
            LinkedList<int> iterations = CalculateIterationMapping();

            LinkedListNode<Point>? node = Points.First;
            LinkedListNode<int>? iterationNode = iterations.First;

            if (node == null || iterationNode == null)
            {
                return;
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                Point previousPoint = node.Value;

                while ((node = node.Next) != null && iterationNode != null)
                {
                    var line = new Line
                    {
                        X1 = previousPoint.X,
                        Y1 = previousPoint.Y,
                        X2 = node.Value.X,
                        Y2 = node.Value.Y,
                        Stroke = ColorGradient[iterationNode.Value],
                        StrokeStartLineCap = PenLineCap.Triangle,
                        StrokeEndLineCap = PenLineCap.Triangle,
                        StrokeThickness = 4
                    };
                    Canvas.Children.Add(line);
                    previousPoint = node.Value;
                    iterationNode = iterationNode.Next;
                }
            });
        }

        /// <summary>
        /// Calculates the iteration mapping.
        /// </summary>
        /// <returns>The linked list of iteration number corresponding to the node.</returns>
        private LinkedList<int> CalculateIterationMapping()
        {
            var iterations = new LinkedList<int>();
            iterations.AddFirst(0);
            iterations.AddLast(0);

            for (var i = 1; i <= Depth; i++)
            {
                LinkedListNode<int>? node = iterations.First;
                LinkedListNode<int>? nextNode;

                while ((nextNode = node?.Next) != null && node != null)
                {
                    iterations.AddBefore(nextNode, i);
                    iterations.AddBefore(nextNode, i);
                    iterations.AddBefore(nextNode, node.Value);

                    node = nextNode;
                }
            }

            return iterations;
        }
    }
}