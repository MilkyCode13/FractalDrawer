using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using FractalDrawer.Fractals;
using FractalDrawer.Model;

namespace FractalDrawer.ViewModel
{
    /// <summary>
    /// The main window view model.
    /// </summary>
    public sealed partial class MainWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Canvas shown on loading.
        /// </summary>
        private readonly Canvas loadingCanvas;

        /// <summary>
        /// The current fractal.
        /// </summary>
        private IFractal currentFractal = null!;

        /// <summary>
        /// The canvas shown.
        /// </summary>
        private Canvas canvas = new();

        /// <summary>
        /// The fractal calculation worker.
        /// </summary>
        private FractalCalculationWorker? worker;

        /// <summary>
        /// The zoom factor.
        /// </summary>
        private double zoomFactor = 1;

        /// <summary>
        /// Horizontal pan offset.
        /// </summary>
        private double offsetX;

        /// <summary>
        /// Vertical pan offset.
        /// </summary>
        private double offsetY;

        /// <summary>
        /// Constructs the instance of the main window view model.
        /// </summary>
        public MainWindowViewModel()
        {
            // Init the fractal types collection.
            FractalTypes = new ObservableCollection<FractalInfo>
            {
                FractalInfo.TreeFractal,
                FractalInfo.KochFractal,
                FractalInfo.CarpetFractal,
                FractalInfo.TriangleFractal,
                FractalInfo.CantorFractal
            };

            // Init the loading canvas.
            loadingCanvas = new Canvas { Width = 150, Height = 50 };
            loadingCanvas.Children.Add(new TextBlock
            {
                Text = "Пожалуйста, подождите...",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            });

            UpdateFractal();
        }

        /// <summary>
        /// Gets or sets the canvas.
        /// </summary>
        public Canvas Canvas
        {
            get => canvas;
            private set
            {
                if (canvas == value)
                {
                    return;
                }

                canvas = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets zoom factor.
        /// </summary>
        public double ZoomFactor
        {
            get => zoomFactor;
            set
            {
                zoomFactor = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets horizontal offset.
        /// </summary>
        public double OffsetX
        {
            get => offsetX;
            set
            {
                offsetX = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets vertical offset.
        /// </summary>
        public double OffsetY
        {
            get => offsetY;
            set
            {
                offsetY = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Invokes an update of the fractal.
        /// </summary>
        private void UpdateFractal()
        {
            try
            {
                Canvas = loadingCanvas;

                if (Depth > SelectedFractalInfo.MaxDepth)
                {
                    Depth = SelectedFractalInfo.MaxDepth;
                }

                currentFractal = BaseFractal.GetFractal(selectedFractalInfo.Type, depth, startColor, endColor,
                    scaleFactor, leftAngle, rightAngle, gapLength);

                worker?.CancelAsync();
                worker = new FractalCalculationWorker(WorkerCompleted);
                worker.RunWorkerAsync(currentFractal);
            }
            catch (Exception e)
            {
                dialogService.ShowErrorMessage(e.Message);
            }
        }

        /// <summary>
        /// The background worker completed event handler. Updates the canvas.
        /// </summary>
        /// <param name="sender">The event sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void WorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (sender is not FractalCalculationWorker calculationWorker)
            {
                return;
            }

            if (calculationWorker.Error != null)
            {
                dialogService.ShowErrorMessage(calculationWorker.Error.Message);
            }

            if (calculationWorker.Result != null)
            {
                Canvas = calculationWorker.Result;
            }
        }
    }
}