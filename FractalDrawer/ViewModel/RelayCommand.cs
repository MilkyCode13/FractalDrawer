using System;
using System.Windows.Input;

namespace FractalDrawer.ViewModel
{
    /// <summary>
    /// Represents a basic command.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// The action to perform on execution.
        /// </summary>
        private readonly Action<object?> execute;

        /// <summary>
        /// The function representing whether the command can execute.
        /// </summary>
        private readonly Func<object?, bool>? canExecute;

        /// <summary>
        /// Constructs the command.
        /// </summary>
        /// <param name="execute">The lambda action to perform on execution.</param>
        /// <param name="canExecute">The lambda function representing whether the command can execute.</param>
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Gets whether the command can execute.
        /// </summary>
        /// <param name="parameter">The parameter of the command.</param>
        /// <returns>Whether the command can execute.</returns>
        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">The parameter of the command.</param>
        public void Execute(object? parameter)
        {
            execute(parameter);
        }

        /// <summary>
        /// Invokes when the CanExecute parameter changed.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}