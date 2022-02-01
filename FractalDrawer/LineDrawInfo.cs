namespace FractalDrawer
{
    /// <summary>
    /// Contains information for line drawer.
    /// </summary>
    /// <param name="Iteration">Iteration number.</param>
    /// <param name="X">X coordinate.</param>
    /// <param name="Y">X coordinate.</param>
    /// <param name="Angle">Angle of the line.</param>
    /// <param name="Length">Length of the line.</param>
    public record LineDrawInfo(int Iteration, double X, double Y, double Angle, double Length)
        : DrawInfo(Iteration, X, Y);
}