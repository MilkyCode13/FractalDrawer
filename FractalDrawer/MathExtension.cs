namespace FractalDrawer
{
    /// <summary>
    /// Contains helper math methods.
    /// </summary>
    public static class MathExtension
    {
        /// <summary>
        /// Calculates an integer power.
        /// </summary>
        /// <param name="b">The base.</param>
        /// <param name="pow">The exponent.</param>
        /// <returns>The power of passed numbers.</returns>
        public static int Pow(this int b, int pow)
        {
            checked
            {
                var value = 1;

                for (var i = 0; i < pow; i++)
                {
                    value *= b;
                }

                return value;
            }
        }
    }
}