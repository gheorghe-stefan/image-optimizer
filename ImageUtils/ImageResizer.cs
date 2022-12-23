using System.Drawing;
using System.Drawing.Imaging;
using static System.Drawing.Image;

namespace ImageUtils;

/// <summary>
/// Provides routines for image resizing.
/// </summary>
public class ImageResizer
{
    #region Static Fields and Constants

    private static readonly int MaxResizeMegaPixels = 100;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Resizes an image at given dimensions.
    /// </summary>
    /// <param name="inputFilePath">Input file path.</param>
    /// <param name="width">Desired width.</param>
    /// <param name="height">Desired height.</param>
    /// <param name="outputFilePath">Output file path.</param>
    /// <remarks>Resized output image is limited to 100Mpx (10,000 x 10,000).</remarks>
    public static void ImageResize(string inputFilePath, int width, int height, string outputFilePath)
    {
        if (string.IsNullOrEmpty(inputFilePath))
        {
            throw new ArgumentNullException(nameof(inputFilePath));
        }

        if (string.IsNullOrEmpty(outputFilePath))
        {
            throw new ArgumentNullException(nameof(inputFilePath));
        }

        if (!File.Exists(inputFilePath))
        {
            throw new ArgumentException($"Input file `{nameof(inputFilePath)}` does not exist.");
        }

        if (width <= 0 || height <= 0)
        {
            throw new ArgumentException($"Supplied width/height are <= 0.");
        }

        if (width * height >= MaxResizeMegaPixels * 1000 * 1000)
        {
            throw new ArgumentException($"Supplied width/height result in the output >= {MaxResizeMegaPixels} Mpx.");
        }

        var convertInPlace = false;
        if (outputFilePath.Equals(inputFilePath))
        {
            outputFilePath = Path.GetTempFileName();
            convertInPlace = true;
        }

        using var bmpInput = FromFile(inputFilePath);
        if (bmpInput.Height == 0 || bmpInput.Width == 0)
        {
            throw new ArgumentException("Supplied image has width or height 0.");
        }

        var originalRatio = (double)bmpInput.Width / bmpInput.Height;
        var desiredRatio = (double)width / height;

        if (!desiredRatio.AlmostEqual(originalRatio))
        {
            // TODO: log message or warning? that the given ratio is different
        }

        // TODO: use ExifLib to retrieve all options (sometimes camera type is not copied over)
        using var bmpOutput = new Bitmap(bmpInput, new Size(width, height));
        foreach (var propertyItem in bmpInput.PropertyItems)
        {
            bmpOutput.SetPropertyItem(propertyItem);
        }

        bmpInput.Dispose();

        // TODO: get original dpi?
        bmpOutput.SetResolution(300.0f, 300.0f);

        bmpOutput.Save(outputFilePath, ImageFormat.Jpeg);
        bmpOutput.Dispose();

        if (convertInPlace)
        {
            File.Delete(inputFilePath);
            File.Move(outputFilePath, inputFilePath);
        }
    }

    #endregion
}