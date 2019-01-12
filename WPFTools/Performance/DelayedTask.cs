using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace WPFTools.Performance
{
    /// <summary>
    /// Encapsulate an action which must be execute after delay. Used to improve performance of the WPF UI.
    /// </summary>
    /// <example>
    /// This sample shows how to use it with a frequently updated properties.
    /// <code>
    /// private DelayedTask _filtrationTask = new DelayedTask();
    /// 
    /// private string _filtrationString;
    /// public string FiltrationString
    /// {
    ///     get => _filtrationString;
    ///     set
    ///     {
    ///         _filtrationString = value;
    ///         _filtrationTask.Register(() =>
    ///         {
    ///             SomeSourceCollectionView?.Refresh();
    ///             NotifyPropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredCount"));
    ///         });
    ///     }
    /// }
    /// </code>
    /// </example>
    public class DelayedTask
    {
        #region Fields

        private Timer _timer;

        private Action _action = null;
        private TimeSpan _delay;

        #endregion

        /// <summary>
        /// DelayedTask constructor with a default delay (1000 ms).
        /// </summary>
        public DelayedTask() : this(TimeSpan.FromMilliseconds(1000)) { }

        /// <summary>
        /// DelayedTask constructor.
        /// </summary>
        /// <param name="milliseconds">Delay in milliseconds</param>
        public DelayedTask(long milliseconds) : this(TimeSpan.FromMilliseconds(milliseconds)) { }

        /// <summary>
        /// DelayedTask constructor.
        /// </summary>
        /// <param name="delay">Delay.</param>
        public DelayedTask(TimeSpan delay)
        {
            _delay = delay;
        }

        /// <summary>
        /// Register a new action to run delayed. Old action will not be fired.
        /// </summary>
        /// <param name="action"></param>
        public void Register(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));

            if (_timer != null && _timer.Enabled)
                _timer.Stop();

            _timer = new Timer(_delay.TotalMilliseconds);
            _timer.Elapsed += RunAction;
            _timer.Start();
        }

        /// <summary>
        /// Run the action and stop the timer.
        /// </summary>
        private void RunAction(object sender, ElapsedEventArgs e)
        {
            _action?.Invoke();
            _timer.Stop();
        }
    }
}
