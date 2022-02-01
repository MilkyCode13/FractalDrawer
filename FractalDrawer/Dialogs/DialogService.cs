using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;

namespace FractalDrawer.Dialogs
{
    /// <summary>
    /// Provides dialogs.
    /// </summary>
    public sealed class DialogService : IDialogService
    {
        /// <summary>
        /// Gets or sets the color of the color dialog.
        /// </summary>
        public Color Color { get; set; } = Colors.Black;

        /// <summary>
        /// Gets or sets the file path of the save file dialog.
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Shows the color dialog.
        /// </summary>
        /// <returns>The dialog result.</returns>
        public bool ColorDialog()
        {
            var context = new ColorDialogWindowViewModel { InitColor = Color };
            var window = new ColorDialogWindow { DataContext = context };

            bool result = window.ShowDialog() ?? false;
            if (result)
            {
                Color = context.SelectedColor;
            }

            return result;
        }

        /// <summary>
        /// Shows the save file dialog.
        /// </summary>
        /// <returns>The dialog result.</returns>
        public bool SaveFileDialog()
        {
            var saveFileDialog = new SaveFileDialog
                { FileName = FilePath, DefaultExt = ".png", Filter = "Изображения PNG (.png)|*.png" };

            bool result = saveFileDialog.ShowDialog() ?? false;
            if (result)
            {
                FilePath = saveFileDialog.FileName;
            }

            return result;
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="message">The message to show.</param>
        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}