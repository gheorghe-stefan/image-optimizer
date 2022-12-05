using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageOptimizer
{
    using System.IO;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // both main window + log text accept drop of files
            this.DragEnter += this.OnDragEnter;
            this.Drop += this.OnDrop;

            this.txtLog.DragEnter += this.OnDragEnter;
            this.txtLog.Drop += this.OnDrop;

            this.txtLog.IsEnabled = false;
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            var lockRatio = chkLock.IsChecked ?? false;

            var newWidth = -1;
            if (!string.IsNullOrEmpty(txtWidth.Text) && int.TryParse(txtWidth.Text, out var width) && width > 0)
            {
                newWidth = width;
            }

            var newHeight = -1;
            if (!lockRatio && !string.IsNullOrEmpty(txtHeight.Text) && int.TryParse(txtHeight.Text, out var height) && height > 0)
            {
                newHeight = height;
            }

            if (newWidth == -1 || (!lockRatio && newHeight == -1))
            {
                MessageBox.Show("Invalid supplied width/height. ");
                return;
            }

            var imageResizer = new ImageResizer();
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var filePath in files)
            {
                try
                {
                    var outputFile = Path.GetDirectoryName(filePath) + "\\" + Path.GetFileNameWithoutExtension(filePath) + "_converted" + Path.GetExtension(filePath);
                    imageResizer.ResizeImage(filePath, newWidth, newHeight, lockRatio, outputFile);

                    txtLog.Text += $"Successfully resized image `{filePath}`, saved as `{outputFile}`.\n";
                }
                catch (Exception exception)
                {
                    txtLog.Text += $"Failed to resize image `{filePath}`; Exception: {exception}\n";
                }

            }
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void LockRationClick(object sender, RoutedEventArgs e)
        {
            this.txtHeight.IsEnabled = !this.chkLock.IsChecked ?? true;
        }
    }
}
