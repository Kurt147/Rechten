using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KurtVerheyenRechtenProject
{
    public class clsCustomCommand : ICommand
    {
        private Action<object> execute;
        private Predicate<object> canExecute;

        public clsCustomCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object param)
        {
            bool b = canExecute == null ? true : canExecute(param);
            return b;
        }
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        public void Execute(object param)
        {
            execute(param);
        }
    }
}
