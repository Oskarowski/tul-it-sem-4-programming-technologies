using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    internal class OnClickCommand : ICommand
    {
        private readonly Action<object> _execute;

        private readonly Predicate<object>? _canExecute;

        public OnClickCommand(Action<object> execute, Predicate<object>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object param)
        {
            return _canExecute == null || _canExecute(param);
        }

        public virtual void Execute(object param)
        {
            _execute.Invoke(param);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
