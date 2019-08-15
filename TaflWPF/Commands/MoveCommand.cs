using System;
using System.Windows.Input;

namespace TaflWPF.Commands
{
    public class MoveCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<string> _execute;
        public MoveCommand(Action<string> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter as string);
        }
    }
}
