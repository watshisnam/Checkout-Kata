using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Checkout_Kata.ViewModels
{
    public class BaseCommand : INotifyPropertyChanged, ICommand
    {
        private readonly Action _command;

        public BaseCommand(Action command)
        {
            if (command == null)
                throw new ArgumentNullException("command");
            _command = command;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _command();
        }


        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                Dispatcher.CurrentDispatcher.BeginInvoke(CanExecuteChanged, new object[] { this, null });
            OnPropertyChanged("Enabled");
        }
        public event EventHandler CanExecuteChanged;

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
