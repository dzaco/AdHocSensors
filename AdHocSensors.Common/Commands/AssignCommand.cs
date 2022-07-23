using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdHocSensors.Common.Commands
{
    public class AssignCommand
    {
        private readonly Action<object, object> _execute;
        private readonly Predicate<object> _canExecute;

        public event EventHandler? CanExecuteChanged;

        public AssignCommand(Action<object, object> execute)
            : this(execute, null)
        {
        }

        public AssignCommand(Action<object, object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object obj, object value)
        {
            _execute(obj, value);
        }
    }
}