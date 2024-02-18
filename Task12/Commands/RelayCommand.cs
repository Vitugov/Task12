using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Task12.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this._Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this._CanExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _CanExecute == null || _CanExecute(parameter);

        public void Execute(object parameter) => _Execute(parameter);
    }
}
