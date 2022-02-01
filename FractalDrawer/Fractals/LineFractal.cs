using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FractalDrawer.Model;

namespace FractalDrawer.Fractals
{
    /// <summary>
    /// The line fractal.
    /// </summary>
    public abstract class LineFractal : BaseFractal
    {
        /// <summary>
        /// The linked list of points used to draw the fractal.
        /// </summary>
        protected readonly LinkedList<Point> Points = new();

        /// <summary>
        /// The start point.
        /// </summary>
        private readonly Point startPoint;

        /// <summary>
        /// The end point.
        /// </summary>
        private readonly Point endPoint;

        /// <summary>
        /// Constructs the line fractal.
        /// </summary>
        /// <param name="info">The fractal info.</param>
        /// <param name="depth">The fractal depth.</param>
        /// <param name="canvasHeight">The canvas height.</param>
        /// <param name="canvasWidth">The canvas width.</param>
        /// <param name="startPoint">The start point.</param>
        /// <param name="endPoint">The end point.</param>
        /// <param name="startColor">The start color.</param>
        /// <param name="endColor">The end color.</param>
        protected LineFractal(FractalInfo info, int depth, int canvasHeight, int canvasWidth, Point startPoint,
            Point endPoint,
            Color startColor, Color endColor) : base(info, depth, canvasHeight, canvasWidth,
            startColor, endColor)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }

        /// <summary>
        /// Draws the fractal.
        /// </summary>
        /// <returns>Canvas with the fractal drawn.</returns>
        public override Canvas Draw()
        {
            Points.Clear();
            Points.AddFirst(startPoint);
            Points.AddLast(endPoint);

            for (var i = 0; i < Depth; i++)
            {
                LinkedListNode<Point>? node = Points.First;
                LinkedListNode<Point>? nextNode;

                var j = 0;
                while ((nextNode = node?.Next) != null && node != null)
                {
                    DetailLink(node, j);
                    node = nextNode;
                    j++;
                }
            }

            ProcessPoints();

            return Canvas;
        }

        /// <summary>
        /// Details the segment with more points.
        /// </summary>
        /// <param name="node">The linked list node.</param>
        /// <param name="index">The index of the node.</param>
        protected abstract void DetailLink(LinkedListNode<Point> node, int index);

        /// <summary>
        /// Draws thr fractal using the points.
        /// </summary>
        protected abstract void ProcessPoints();
    }
}