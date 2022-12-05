namespace ImageOptimizer
{
    interface IImageResizer
    {
        #region Public Methods and Operators

        /// <summary>
        /// Resizes an image at given dimensions.
        /// </summary>
        /// <param name="inputFilePath">Input file path.</param>
        /// <param name="width">Desired width.</param>
        /// <param name="height">Desired height; ignored & recomputed, if keepRation is true.</param>
        /// <param name="keepRatio">If true, the height is recomputed based on initial ratio.</param>
        /// <param name="outputFilePath">Output file path.</param>
        public void ResizeImage(string inputFilePath, int width, int height, bool keepRatio, string outputFilePath);

        #endregion
    }
}