using System.Windows.Media;

namespace FractalDrawer.Dialogs
{
    /// <summary>
    /// Represents a dialog service.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Gets or sets the color of the color dialog.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the file path of the save file dialog.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Shows the color dialog.
        /// </summary>
        /// <returns>The dialog result.</returns>
        public bool ColorDialog();

        /// <summary>
        /// Shows the save file dialog.
        /// </summary>
        /// <returns>The dialog result.</returns>
        public bool SaveFileDialog();

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="message">The message to show.</param>
        public void ShowErrorMessage(string message);
    }
}