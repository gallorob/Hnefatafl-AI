using System;
using System.Windows.Input;

namespace TaflWPF.Commands
{
	public class RelayCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;
		private Action _execute;

		public RelayCommand(Action execute)
		{
			_execute = execute;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			_execute.Invoke();
		}
	}

	public class RelayCommand<T> : ICommand
	{
		public event EventHandler CanExecuteChanged;
		private Action<T> _execute;
		public RelayCommand(Action<T> execute)
		{
			_execute = execute;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			_execute?.Invoke((T)parameter);
		}
	}
}
