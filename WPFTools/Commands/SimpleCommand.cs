using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WPFTools.Commands
{
    /// <summary>
    /// Just a simple command to use with WPF button.
    /// </summary>
    public class SimpleCommand : ICommand
    {
        #region Fields

        private Action _execute;
        private Predicate<object> _canExecute;

        private static Predicate<object> _defaultCanexecute = new Predicate<object>((_) => true);

        #endregion

        /// <summary>
        /// SimpleCommand constructor.
        /// </summary>
        /// <param name="execute">Action binded to new instance of SimpleCommand.</param>
        public SimpleCommand(Action execute) : this(execute, _defaultCanexecute) { }

        /// <summary>
        /// SimpleCommand constructor.
        /// </summary>
        /// <param name="execute">Action binded to new instance of SimpleCommand.</param>
        /// <param name="canExecute">Predicate to disable the command.</param>
        public SimpleCommand(Action execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
