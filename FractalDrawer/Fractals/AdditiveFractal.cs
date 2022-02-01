using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using FractalDrawer.Model;

namespace FractalDrawer.Fractals
{
    /// <summary>
    /// The additive fractal.
    /// </summary>
    /// <typeparam name="TInfo">The type of drawer info.</typeparam>
    public abstract class AdditiveFractal<TInfo> : BaseFractal where TInfo : DrawInfo
    {
        /// <summary>
        /// The factor of rise of complexity of each iteration.
        /// </summary>
        private readonly int multiplyFactor;

        /// <summary>
        /// The initial drawer parameters for zeroth iteration.
        /// </summary>
        private readonly TInfo startParameters;

        /// <summary>
        /// Constructs an additive fractal.
        /// </summary>
        /// <param name="info">The fractal info.</param>
        /// <param name="depth">The fractal depth.</param>
        /// <param name="canvasHeight">The canvas height.</param>
        /// <param name="canvasWidth">The canvas width.</param>
        /// <param name="multiplyFactor">The factor of rise of complexity of each iteration.</param>
        /// <param name="startParameters">The initial drawer parameters for zeroth iteration.</param>
        /// <param name="startColor">The start color.</param>
        /// <param name="endColor">The end color.</param>
        protected AdditiveFractal(
            FractalInfo info, int depth, int canvasHeight, int canvasWidth, int multiplyFactor, TInfo startParameters,
            Color startColor, Color endColor) : base(info, depth, canvasHeight, canvasWidth, startColor, endColor)
        {
            this.multiplyFactor = multiplyFactor;
            this.startParameters = startParameters;
        }

        /// <summary>
        /// The queue of drawing operations represented by drawing information instances.
        /// </summary>
        protected Queue<TInfo> DrawQueue { get; } = new();

        /// <summary>
        /// Draws the fractal.
        /// </summary>
        /// <returns>Canvas with the fractal drawn.</returns>
        public override Canvas Draw()
        {
            DrawBase(startParameters);

            DrawQueue.Clear();
            DrawQueue.Enqueue(startParameters);

            int count = (multiplyFactor.Pow(Depth + 1) - 1) / (multiplyFactor - 1);

            for (var i = 0; i < count; i++)
            {
                DrawIteration();
            }

            return Canvas;
        }

        /// <summary>
        /// Draws the fractal base shape (so-called zeroth iteration).
        /// </summary>
        /// <param name="drawInfo">The drawing info.</param>
        protected virtual void DrawBase(TInfo drawInfo)
        {
        }

        /// <summary>
        /// Draws an iteration.
        /// </summary>
        protected abstract void DrawIteration();
    }
}