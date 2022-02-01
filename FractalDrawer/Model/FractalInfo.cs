using FractalDrawer.Fractals;

namespace FractalDrawer.Model
{
    /// <summary>
    /// Represents the information of a fractal.
    /// </summary>
    public record FractalInfo
    {
        /// <summary>
        /// Gets or initializes the type of the fractal.
        /// </summary>
        public FractalType Type { get; init; }

        /// <summary>
        /// Gets or initializes the maximum depth of the fractal.
        /// </summary>
        public int MaxDepth { get; private init; }

        /// <summary>
        /// Gets the tree fractal information.
        /// </summary>
        public static FractalInfo TreeFractal { get; } = new() { Type = FractalType.TreeFractal, MaxDepth = 11 };

        /// <summary>
        /// Gets the Koch fractal information.
        /// </summary>
        public static FractalInfo KochFractal { get; } = new() { Type = FractalType.KochFractal, MaxDepth = 6 };

        /// <summary>
        /// Gets the carpet fractal information.
        /// </summary>
        public static FractalInfo CarpetFractal { get; } = new() { Type = FractalType.CarpetFractal, MaxDepth = 4 };

        /// <summary>
        /// Gets the triangle fractal information.
        /// </summary>
        public static FractalInfo TriangleFractal { get; } = new() { Type = FractalType.TriangleFractal, MaxDepth = 7 };

        /// <summary>
        /// Gets the Cantor fractal information.
        /// </summary>
        public static FractalInfo CantorFractal { get; } = new() { Type = FractalType.CantorFractal, MaxDepth = 12 };

        /// <summary>
        /// Converts the value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            return Type.ToString();
        }
    }
}