using System;
using System.ComponentModel;
using System.Windows.Controls;
using FractalDrawer.Fractals;

namespace FractalDrawer.ViewModel
{
    /// <summary>
    /// The background fractal calculation worker.
    /// </summary>
    public class FractalCalculationWorker : BackgroundWorker
    {
        /// <summary>
        /// Constructs an instance of the background fractal calculation worker.
        /// </summary>
        /// <param name="handler">Completed event handler.</param>
        public FractalCalculationWorker(RunWorkerCompletedEventHandler handler)
        {
            WorkerSupportsCancellation = true;
            DoWork += Calculate;
            RunWorkerCompleted += handler;
        }

        /// <summary>
        /// Gets or sets the exception occurred during calculation. 
        /// </summary>
        public Exception? Error { get; private set; }

        /// <summary>
        /// Gets or sets the result of the calculation.
        /// </summary>
        public Canvas? Result { get; private set; }

        /// <summary>
        /// Calculates the fractal.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Calculate(object? sender, DoWorkEventArgs e)
        {
            if (e.Argument is not IFractal fractal)
            {
                return;
            }

            try
            {
                Result = fractal.Draw();
            }
            catch (Exception exception)
            {
                Error = exception;
            }

            if (!CancellationPending)
            {
                return;
            }

            Result = null;
            e.Cancel = true;
        }
    }
}