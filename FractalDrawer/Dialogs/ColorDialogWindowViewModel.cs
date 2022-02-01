using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FractalDrawer.ViewModel;

namespace FractalDrawer.Dialogs
{
    /// <summary>
    /// The color selection dialog window view model.
    /// </summary>
    public sealed class ColorDialogWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The selected color.
        /// </summary>
        private Color selectedColor;

        /// <summary>
        /// The red component of the selected color.
        /// </summary>
        private byte red;

        /// <summary>
        /// The green component of the selected color.
        /// </summary>
        private byte green;

        /// <summary>
        /// The blue component of the selected color.
        /// </summary>
        private byte blue;

        /// <summary>
        /// The "Ok" command.
        /// </summary>
        private ICommand? okCommand;

        /// <summary>
        /// The "Cancel" command.
        /// </summary>
        private ICommand? cancelCommand;

        /// <summary>
        /// Gets the "Ok" command.
        /// </summary>
        public ICommand OkCommand
        {
            get
            {
                return okCommand ??= new RelayCommand(obj =>
                {
                    if (obj is Window window)
                    {
                        window.DialogResult = true;
                    }
                });
            }
        }

        /// <summary>
        /// Gets the "Cancel" command.
        /// </summary>
        public ICommand CancelCommand
        {
            get
            {
                return cancelCommand ??= new RelayCommand(obj =>
                {
                    if (obj is Window window)
                    {
                        window.DialogResult = false;
                    }
                });
            }
        }

        /// <summary>
        /// Gets or sets the selected color.
        /// </summary>
        public Color SelectedColor
        {
            get => selectedColor;
            set
            {
                if (SelectedColor == value)
                {
                    return;
                }

                selectedColor = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes the selected color.
        /// </summary>
        public Color InitColor
        {
            init
            {
                Red = value.R;
                Green = value.G;
                Blue = value.B;
            }
        }

        /// <summary>
        /// Gets or sets the red component of the selected color.
        /// </summary>
        public byte Red
        {
            get => red;
            set
            {
                red = value;
                OnPropertyChanged();
                UpdateColor();
            }
        }

        /// <summary>
        /// Gets or sets the green component of the selected color.
        /// </summary>
        public byte Green
        {
            get => green;
            set
            {
                green = value;
                OnPropertyChanged();
                UpdateColor();
            }
        }

        /// <summary>
        /// Gets or sets the blue component of the selected color.
        /// </summary>
        public byte Blue
        {
            get => blue;
            set
            {
                blue = value;
                OnPropertyChanged();
                UpdateColor();
            }
        }

        /// <summary>
        /// Updates the selected color.
        /// </summary>
        private void UpdateColor()
        {
            SelectedColor = Color.FromRgb(red, green, blue);
        }

        /// <summary>
        /// Invokes when the property is changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Invokes the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property to pass in to the event.</param>
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}