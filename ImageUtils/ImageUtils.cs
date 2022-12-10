using System.Drawing;
using System.Drawing.Imaging;
using static System.Drawing.Image;

namespace ImageUtils;

public class ImageUtils
{
    #region Public Methods and Operators

    /// <summary>
    /// Resizes an image at given dimensions.
    /// </summary>
    /// <param name="inputFilePath">Input file path.</param>
    /// <param name="width">Desired width.</param>
    /// <param name="height">Desired height.</param>
    /// <param name="outputFilePath">Output file path.</param>
    public static void ImageResize(string inputFilePath, int width, int height, string outputFilePath)
    {
        if (string.IsNullOrEmpty(inputFilePath))
        {
            throw new ArgumentNullException(nameof(inputFilePath));
        }

        if (!File.Exists(inputFilePath))
        {
            throw new ArgumentException($"Input file `{nameof(inputFilePath)}` does not exist.");
        }

        if (string.IsNullOrEmpty(outputFilePath))
        {
            outputFilePath = inputFilePath;
            // TODO: check write access of folder
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

        using var bmpOutput = new Bitmap(bmpInput, new Size(width, height));
        foreach (var id in bmpInput.PropertyIdList)
        {
            var propItem = bmpInput.GetPropertyItem(id);

            if (propItem != null)
            {
                bmpOutput.SetPropertyItem(propItem);
            }
        }

        // TODO: get original dpi?
        bmpOutput.SetResolution(300.0f, 300.0f);

        // TODO: some backup mechanism when outputFilePath is not writable -> move to temp?
        bmpOutput.Save(outputFilePath, ImageFormat.Jpeg);
    }

    #endregion
}