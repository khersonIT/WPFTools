using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WPFTools.Commands
{
    /// <summary>
    /// Command which will be auto disabled while action running.
    /// </summary>
    public class AutoDisabledCommand : SimpleCommand
    {
        #region Fields
        
        // TODO : this part of funtionality must be tested.
        private bool _isRunning = false;

        #endregion

        /// <summary>
        /// AutoDisabledCommand constructor.
        /// </summary>
        /// <param name="execute">Action binded to new instance of AutoDisabledCommand.</param>
        public AutoDisabledCommand(Action execute) : this(execute, _defaultCanexecute) { }

        /// <summary>
        /// AutoDisabledCommand constructor.
        /// </summary>
        /// <param name="execute">Action binded to new instance of AutoDisabledCommand.</param>
        /// <param name="canExecute">Predicate to disable the command.</param>
        public AutoDisabledCommand(Action execute, Predicate<object> canExecute) : base(execute, canExecute) { }
        
        public new bool CanExecute(object parameter)
        {
            return _isRunning || _canExecute(parameter);
        }

        public new void Execute(object parameter)
        {
            try
            {
                // TODO : need to test a case when action is async.
                _isRunning = true;
                _execute();
            }
            finally
            {
                _isRunning = false;
            }
        }
    }
}
