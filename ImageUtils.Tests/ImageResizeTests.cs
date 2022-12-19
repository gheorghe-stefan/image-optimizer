using System.Reflection;

namespace ImageUtils.Tests;

[TestClass]
public class ImageResizeTests
{
    #region Static Fields and Constants

    private static readonly string TestDataFolder = @$"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\..\..\..\TestData";
    private static readonly string DefaultInputFile = @$"{TestDataFolder}\PXL_20221205_153055566.jpg";
    private static readonly string DefaultOutputFile = @$"{TestDataFolder}\PXL_20221205_153055566_converted.jpg";

    #endregion

    #region Public Methods and Operators

    [TestMethod]
    public void TestConvertJpg_BiggerRescale()
    {
        TestUtils.TestImageResize(DefaultInputFile, 5000, -1, DefaultOutputFile);
    }

    [TestMethod]
    public void TestConvertJpg_BreakRatio()
    {
        TestUtils.TestImageResize(DefaultInputFile, 1600, 300, DefaultOutputFile, false);
    }

    [TestMethod]
    public void TestConvertJpg_InputAsOutput()
    {
        TestUtils.TestImageResize(DefaultInputFile, 1600, -1, DefaultInputFile);
    }

    [TestMethod]
    public void TestConvertJpg_KeepRatio()
    {
        TestUtils.TestImageResize(DefaultInputFile, 1600, -1, DefaultOutputFile);
    }

    [TestMethod]
    public void TestConvertJpg_VerySmall()
    {
        TestUtils.TestImageResize(DefaultInputFile, 160, -1, DefaultOutputFile);
    }

    [TestMethod]
    public void TestExceptions()
    {
        var aValidFilePath = Path.GetTempFileName();

        Assert.ThrowsException<ArgumentNullException>(() => ImageResizer.ImageResize(null!, 100, 100, "fake_output_file"));
        Assert.ThrowsException<ArgumentNullException>(() => ImageResizer.ImageResize("", 100, 100, "fake_output_file"));
        Assert.ThrowsException<ArgumentNullException>(() => ImageResizer.ImageResize(aValidFilePath, 100, 100, null!));
        Assert.ThrowsException<ArgumentNullException>(() => ImageResizer.ImageResize(aValidFilePath, 100, 100, ""));
        Assert.ThrowsException<ArgumentException>(() => ImageResizer.ImageResize("invalid_path", 100, 100, "fake_output_file"));
        Assert.ThrowsException<ArgumentException>(() => ImageResizer.ImageResize(aValidFilePath, -100, 100, "fake_output_file"));
        Assert.ThrowsException<ArgumentException>(() => ImageResizer.ImageResize(aValidFilePath, 0, 100, "fake_output_file"));
        Assert.ThrowsException<ArgumentException>(() => ImageResizer.ImageResize(aValidFilePath, 100, -100, "fake_output_file"));
        Assert.ThrowsException<ArgumentException>(() => ImageResizer.ImageResize(aValidFilePath, 100, 0, "fake_output_file"));
        Assert.ThrowsException<ArgumentException>(() => ImageResizer.ImageResize(aValidFilePath, 100000, 100000, "fake_output_file"));
        // TODO: how to test images with width/height 0?

        File.Delete(aValidFilePath);
    }

    #endregion
}