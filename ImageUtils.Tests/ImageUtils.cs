using static System.Drawing.Image;

namespace ImageUtils.Tests;

internal sealed class ImageUtils
{
    #region Public Methods and Operators

    /// <summary>
    /// Tries to obtain the image size, in pixels.
    /// </summary>
    /// <param name="imageFilePath">Input file.</param>
    /// <param name="size">Tuple with the size of the image, in pixels.</param>
    /// <returns>True, if size could be retrieved; false otherwise.</returns>
    public static bool TryGetImageDimensions(string imageFilePath, out (int width, int height) size)
    {
        size = (-1, -1);

        if (!File.Exists(imageFilePath))
        {
            return false;
        }

        try
        {
            using var image = FromFile(imageFilePath);
            size = (image.Width, image.Height);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    #endregion
}
