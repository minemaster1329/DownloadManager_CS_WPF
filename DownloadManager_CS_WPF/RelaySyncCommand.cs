using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DownloadManager_CS_WPF
{
    public delegate void ExecuteMethod(object parameter);
    public delegate bool CanExecuteMethod(object parameter);

    public class RelaySyncCommand : ICommand
    {
        private ExecuteMethod _execute;
        private CanExecuteMethod _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        RelaySyncCommand() { }

        public RelaySyncCommand(ExecuteMethod execute, CanExecuteMethod canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
    }
}
