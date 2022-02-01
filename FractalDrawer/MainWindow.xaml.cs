using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using FractalDrawer.ViewModel;

namespace FractalDrawer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// The window view model.
        /// </summary>
        private readonly MainWindowViewModel viewModel;

        /// <summary>
        /// Constructs the window.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            DataContext = viewModel = new MainWindowViewModel();
        }

        /// <summary>
        /// Handles the DragDelta event of the Thumb.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            viewModel.OffsetX -= e.HorizontalChange;
            viewModel.OffsetY -= e.VerticalChange;

            ScrollViewer.ScrollToHorizontalOffset(viewModel.OffsetX);
            ScrollViewer.ScrollToVerticalOffset(viewModel.OffsetY);
        }

        /// <summary>
        /// Handles the OnScrollChanged event of the ScrollViewer.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            viewModel.OffsetX = ScrollViewer.HorizontalOffset;
            viewModel.OffsetY = ScrollViewer.VerticalOffset;
        }
    }
}