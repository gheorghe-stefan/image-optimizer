namespace ImageUtils.Tests;

internal sealed class TestUtils
{
    #region Public Methods and Operators

    public static void TestImageResize(string inputFilePath, int newWidth, int newHeight, string outputFilePath, bool keepHeightRatio = true)
    {
        Assert.IsTrue(File.Exists(inputFilePath), $"Test input file `{inputFilePath}` doesn't exist.");

        try
        {
            // copy image to temp folder, not to alter it
            if (ImageUtils.TryGetImageDimensions(inputFilePath, out var dimensions))
            {
                if (keepHeightRatio)
                {
                    newHeight = (newWidth * dimensions.height / dimensions.width);
                }

                ImageResizer.ImageResize(inputFilePath, newWidth, newHeight, outputFilePath);

                if (ImageUtils.TryGetImageDimensions(outputFilePath, out var newDimensions))
                {
                    Assert.AreEqual(newWidth, newDimensions.width);
                    Assert.AreEqual(newHeight, newDimensions.height);

                    // TODO: save original props before replace?
                    /*if (!inputFilePath.Equals(outputFilePath))
                    {
                        Assert.IsTrue(ComparerUtils.CompareImagePropertyItems(inputFilePath, outputFilePath), "Input/output image property items are not the same.");
                    }*/
                }
                else
                {
                    Assert.Fail($"Cannot retrieve dimensions for newly converted image `{outputFilePath}`.");
                }
            }
            else
            {
                Assert.Fail($"Cannot retrieve dimensions for image `{inputFilePath}`.");
            }
        }
        catch (Exception ex)
        {
            Assert.Fail($"Exception `{ex}` was thrown.");
        }
        finally
        {
            if (File.Exists(outputFilePath) && !inputFilePath.Equals(outputFilePath))
            {
                File.Delete(outputFilePath);
            }
        }
    }

    #endregion
}