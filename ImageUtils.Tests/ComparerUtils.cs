using static System.Drawing.Image;

namespace ImageUtils.Tests;

internal static class ComparerUtils
{
    #region Public Methods and Operators

    public static bool CompareImagePropertyItems(string firstImagePath, string secondImagePath)
    {
        try
        {
            // below will throw if file doesn't exist
            using var firstImage = FromFile(firstImagePath);
            using var secondImage = FromFile(secondImagePath);

            return firstImage.PropertyItems.Equals(secondImage.PropertyItems);
        }
        catch (Exception)
        {
            return false;
        }
    }

    #endregion
}
