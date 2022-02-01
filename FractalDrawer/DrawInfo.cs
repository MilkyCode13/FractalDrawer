namespace FractalDrawer
{
    /// <summary>
    /// Contains information for drawer.
    /// </summary>
    /// <param name="Iteration">Iteration number.</param>
    /// <param name="X">X coordinate.</param>
    /// <param name="Y">X coordinate.</param>
    public record DrawInfo(int Iteration, double X, double Y);
}