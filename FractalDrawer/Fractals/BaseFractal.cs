using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FractalDrawer.Model;

namespace FractalDrawer.Fractals
{
    /// <summary>
    /// The base fractal.
    /// </summary>
    public abstract class BaseFractal : IFractal
    {
        /// <summary>
        /// The canvas to draw on.
        /// </summary>
        protected Canvas Canvas;

        /// <summary>
        /// Constructs the base fractal.
        /// </summary>
        /// <param name="info">The fractal info.</param>
        /// <param name="depth">The fractal depth.</param>
        /// <param name="canvasHeight">The canvas height.</param>
        /// <param name="canvasWidth">The canvas width.</param>
        /// <param name="startColor">The start color.</param>
        /// <param name="endColor">The end color.</param>
        /// <exception cref="ArgumentException">The depth is below 0 or above maximum depth.</exception>
        protected BaseFractal(FractalInfo info, int depth, int canvasHeight, int canvasWidth, Color startColor,
            Color endColor)
        {
            MaxDepth = info.MaxDepth;
            if (depth < 0 || depth > MaxDepth)
            {
                throw new ArgumentException("Depth cannot be below 0 or above maximum depth.");
            }

            Depth = depth;

            ColorGradient = new Brush[Depth + 1];
            CalculateGradient(startColor, endColor);

            Canvas = null!;
            Application.Current.Dispatcher.Invoke(() =>
            {
                Canvas = new Canvas { Height = canvasHeight, Width = canvasWidth, Background = Brushes.White };
            });
        }
        
        public int MaxDepth { get; }

        /// <summary>
        /// Gets the fractal depth.
        /// </summary>
        public int Depth { get; }

        /// <summary>
        /// Gets the color gradient values. 
        /// </summary>
        protected Brush[] ColorGradient { get; }

        /// <summary>
        /// Gets the fractal from the fractal type.
        /// </summary>
        /// <param name="type">The fractal type.</param>
        /// <param name="depth">The fractal depth.</param>
        /// <param name="startColor">The fractal start color.</param>
        /// <param name="endColor">The fractal end color.</param>
        /// <param name="scaleFactor">The fractal segment scale factor.</param>
        /// <param name="leftAngle">The fractal left segment angle.</param>
        /// <param name="rightAngle">The fractal right segment angle.</param>
        /// <param name="gapLength">The fractal line gap length.</param>
        /// <returns>The fractal object.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The fractal type is invalid.</exception>
        public static BaseFractal GetFractal(
            FractalType type, int depth, Color startColor, Color endColor,
            double scaleFactor, double leftAngle, double rightAngle, double gapLength)
        {
            return type switch
            {
                FractalType.TreeFractal =>
                    new TreeFractal(depth, startColor, endColor, scaleFactor, leftAngle, rightAngle),
                FractalType.KochFractal => new KochFractal(depth, startColor, endColor),
                FractalType.CarpetFractal => new CarpetFractal(depth, startColor, endColor),
                FractalType.TriangleFractal => new TriangleFractal(depth, startColor, endColor),
                FractalType.CantorFractal => new CantorFractal(depth, startColor, endColor, gapLength),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid fractal type.")
            };
        }

        /// <summary>
        /// Draws the fractal.
        /// </summary>
        /// <returns>Canvas with the fractal drawn.</returns>
        public abstract Canvas Draw();

        /// <summary>
        /// Calculates the gradient values.
        /// </summary>
        /// <param name="startColor">The fractal start color.</param>
        /// <param name="endColor">The fractal end color.</param>
        private void CalculateGradient(Color startColor, Color endColor)
        {
            if (Depth == 0)
            {
                ColorGradient[0] = new SolidColorBrush(startColor);
                return;
            }

            int deltaR = endColor.R - startColor.R;
            int deltaG = endColor.G - startColor.G;
            int deltaB = endColor.B - startColor.B;

            for (var i = 0; i <= Depth; i++)
            {
                var red = (byte) (startColor.R + deltaR * i / Depth);
                var green = (byte) (startColor.G + deltaG * i / Depth);
                var blue = (byte) (startColor.B + deltaB * i / Depth);
                ColorGradient[i] = new SolidColorBrush(Color.FromRgb(red, green, blue));
            }
        }
    }
}