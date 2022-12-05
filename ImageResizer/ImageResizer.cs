namespace ImageOptimizer;

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

public class ImageResizer : IImageResizer
{
    #region Public Methods and Operators

    /// <inheritdoc />
    public void ResizeImage(string inputFilePath, int width, int height, bool keepRatio, string outputFilePath)
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

        var image = Image.FromFile(inputFilePath);
        if (image.Height == 0 || image.Width == 0)
        {
            throw new ArgumentException("Supplied image has width or height 0.");
        }

        if (keepRatio)
        {
            var originalRatio = (double)image.Width / image.Height;
            height = (int)(width / originalRatio);
        }

        using var result = new Bitmap(width, height);
        using (var input = new Bitmap(inputFilePath))
        {
            using var g = Graphics.FromImage(result);
            g.DrawImage(input, 0, 0, width, height);
        }

        // TODO: obtain input's file encoder
        var imageCodecInfo = ImageCodecInfo.GetImageEncoders().FirstOrDefault(ie => ie.MimeType == "image/jpeg");
        var eps = new EncoderParameters(1);
        eps.Param[0] = new EncoderParameter(Encoder.Quality, 90L);
        result.Save(outputFilePath, imageCodecInfo, eps);
    }

    #endregion
}