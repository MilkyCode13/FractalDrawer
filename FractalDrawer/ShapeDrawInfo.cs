namespace FractalDrawer
{
    /// <summary>
    /// Contains information for shape drawer.
    /// </summary>
    /// <param name="Iteration">Iteration number.</param>
    /// <param name="X">X coordinate.</param>
    /// <param name="Y">X coordinate.</param>
    /// <param name="Size">Size of the shape.</param>
    public record ShapeDrawInfo(int Iteration, double X, double Y, double Size) : DrawInfo(Iteration, X, Y);
}