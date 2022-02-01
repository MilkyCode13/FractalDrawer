using System;

namespace FractalDrawer.ViewModel
{
    /// <summary>
    /// The main window view model.
    /// </summary>
    public sealed partial class MainWindowViewModel
    {
        /// <summary>
        /// Selected segment length scale factor.
        /// </summary>
        private double scaleFactor = 1.5;

        /// <summary>
        /// Selected left segment angle.
        /// </summary>
        private double leftAngle;

        /// <summary>
        /// Selected right segment angle.
        /// </summary>
        private double rightAngle;

        /// <summary>
        /// Selected line gap length.
        /// </summary>
        private double gapLength = 5;

        /// <summary>
        /// Gets or sets selected segment length scale factor.
        /// </summary>
        public double ScaleFactor
        {
            get => scaleFactor;
            set
            {
                if (Math.Abs(scaleFactor - value) < 0.005)
                {
                    return;
                }

                scaleFactor = value;
                OnPropertyChanged();

                UpdateFractal();
            }
        }

        /// <summary>
        /// Gets or sets selected left segment angle.
        /// </summary>
        public double LeftAngle
        {
            get => leftAngle;
            set
            {
                if (Math.Abs(leftAngle - value) < 0.005)
                {
                    return;
                }

                leftAngle = value;
                OnPropertyChanged();

                UpdateFractal();
            }
        }

        /// <summary>
        /// Gets or sets selected right segment angle.
        /// </summary>
        public double RightAngle
        {
            get => rightAngle;
            set
            {
                if (Math.Abs(rightAngle - value) < 0.005)
                {
                    return;
                }

                rightAngle = value;
                OnPropertyChanged();

                UpdateFractal();
            }
        }

        /// <summary>
        /// Gets or sets selected line gap length.
        /// </summary>
        public double GapLength
        {
            get => gapLength;
            set
            {
                if (Math.Abs(gapLength - value) < 0.005)
                {
                    return;
                }

                gapLength = value;
                OnPropertyChanged();

                UpdateFractal();
            }
        }
    }
}