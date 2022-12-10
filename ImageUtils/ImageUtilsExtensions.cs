namespace ImageUtils;

internal static class ImageUtilsExtensions
{
    #region Static Fields and Constants

    private const double DefaultDoubleEpsilon = 1e-3;

    #endregion

    #region Public Methods and Operators

    public static bool AlmostEqual(this double first, double second, double epsilon = DefaultDoubleEpsilon)
    {
        return Math.Abs(first - second) < epsilon;
    }

    #endregion
}
