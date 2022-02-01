using System.Collections.ObjectModel;
using System.Windows.Media;
using FractalDrawer.Model;

namespace FractalDrawer.ViewModel
{
    /// <summary>
    /// The main window view model.
    /// </summary>
    public sealed partial class MainWindowViewModel
    {
        /// <summary>
        /// Selected fractal info.
        /// </summary>
        private FractalInfo selectedFractalInfo = FractalInfo.TreeFractal;

        /// <summary>
        /// Selected fractal depth.
        /// </summary>
        private int depth;

        /// <summary>
        /// Selected start color.
        /// </summary>
        private Color startColor = Colors.Indigo;

        /// <summary>
        /// Selected end color.
        /// </summary>
        private Color endColor = Colors.Red;

        /// <summary>
        /// Fractal type collection.
        /// </summary>
        public ObservableCollection<FractalInfo> FractalTypes { get; }

        /// <summary>
        /// Gets or sets selected fractal info.
        /// </summary>
        public FractalInfo SelectedFractalInfo
        {
            get => selectedFractalInfo;
            set
            {
                if (selectedFractalInfo == value)
                {
                    return;
                }

                selectedFractalInfo = value;
                OnPropertyChanged();

                UpdateFractal();
            }
        }

        /// <summary>
        /// Gets or sets selected fractal depth.
        /// </summary>
        public int Depth
        {
            get => depth;
            set
            {
                if (depth == value)
                {
                    return;
                }

                depth = value;
                OnPropertyChanged();

                UpdateFractal();
            }
        }

        /// <summary>
        /// Gets or sets selected start color.
        /// </summary>
        public Color StartColor
        {
            get => startColor;
            set
            {
                startColor = value;
                OnPropertyChanged();

                UpdateFractal();
            }
        }

        /// <summary>
        /// Gets or sets selected end color.
        /// </summary>
        public Color EndColor
        {
            get => endColor;
            set
            {
                endColor = value;
                OnPropertyChanged();

                UpdateFractal();
            }
        }
    }
}