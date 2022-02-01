using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FractalDrawer.Dialogs;
using FractalDrawer.Services;

namespace FractalDrawer.ViewModel
{
    /// <summary>
    /// The main window view model.
    /// </summary>
    public sealed partial class MainWindowViewModel
    {
        /// <summary>
        /// The dialog service.
        /// </summary>
        private readonly IDialogService dialogService = new DialogService();

        /// <summary>
        /// The file service.
        /// </summary>
        private readonly IFileService fileService = new PngFileService();

        /// <summary>
        /// Select start color command.
        /// </summary>
        private ICommand? startColorCommand;

        /// <summary>
        /// Select end color command.
        /// </summary>
        private ICommand? endColorCommand;

        /// <summary>
        /// Save file command.
        /// </summary>
        private ICommand? saveFileCommand;

        /// <summary>
        /// Gets select start color command.
        /// </summary>
        public ICommand StartColorCommand
        {
            get
            {
                return startColorCommand ??= new RelayCommand(_ =>
                {
                    dialogService.Color = StartColor;
                    if (dialogService.ColorDialog())
                    {
                        StartColor = dialogService.Color;
                    }
                });
            }
        }

        /// <summary>
        /// Gets select end color command.
        /// </summary>
        public ICommand EndColorCommand
        {
            get
            {
                return endColorCommand ??= new RelayCommand(_ =>
                {
                    dialogService.Color = EndColor;
                    if (dialogService.ColorDialog())
                    {
                        EndColor = dialogService.Color;
                    }
                });
            }
        }

        /// <summary>
        /// Gets save file command.
        /// </summary>
        public ICommand SaveFileCommand
        {
            get
            {
                return saveFileCommand ??= new RelayCommand(_ =>
                {
                    try
                    {
                        if (dialogService.SaveFileDialog())
                        {
                            fileService.Save(dialogService.FilePath, canvas);
                        }
                    }
                    catch (Exception e)
                    {
                        dialogService.ShowErrorMessage(e.Message);
                    }
                });
            }
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