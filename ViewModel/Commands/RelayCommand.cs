using System;
using System.Windows.Input;

namespace ViewModel.Commands
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other objects by invoking delegates.
    /// </summary>
    public class RelayCommand : ICommand // Source: https://stackoverflow.com/questions/34996198/the-name-commandmanager-does-not-exist-in-the-current-context-visual-studio-2
    {
        private readonly Action<object> execute;
        private readonly Func<object,bool> canExecute;

        #region Con-/Destructors
        public RelayCommand(Action execute)
            : this(execute, (Func<object, bool>)null)
        { }

        public RelayCommand(Action execute, Func<bool> canExecute) 
            : this(new Action<object>((parameter) => execute()), new Func<object, bool>((parameter) => canExecute())) 
        { }

        public RelayCommand(Action execute, Func<object, bool> canExecute) 
            : this(new Action<object>((parameter) => execute()), canExecute) 
        { }

        public RelayCommand(Action<object> execute, Func<bool> canExecute) 
            : this(execute, new Func<object, bool>((parameter) => canExecute())) 
        { }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute is null.");
            this.canExecute = canExecute ?? throw new ArgumentException("canExecute is null");
            this.RaiseCanExecuteChangedAction = RaiseCanExecuteChanged;
            SimpleCommandManager.AddRaiseCanExecuteChangedAction(ref RaiseCanExecuteChangedAction);
        }

        ~RelayCommand()
        {
            RemoveCommand();
        }
        #endregion

        public void RemoveCommand() => SimpleCommandManager.RemoveRaiseCanExecuteChangedAction(RaiseCanExecuteChangedAction);

        bool ICommand.CanExecute(object parameter) => canExecute(parameter);

        public void Execute(object parameter)
        {
            execute(parameter);
            SimpleCommandManager.RefreshCommandStates();
        }

        public bool CanExecute
        {
            get { return canExecute == null || canExecute(null); }
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());

        private readonly Action RaiseCanExecuteChangedAction;

        public event EventHandler CanExecuteChanged;
    }
}
