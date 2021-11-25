using System;
using System.Windows.Input;

namespace ViewModel.Commands
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other objects by invoking delegates.
    /// </summary>
    public class RelayCommand : ICommand // Source: https://stackoverflow.com/questions/34996198/the-name-commandmanager-does-not-exist-in-the-current-context-visual-studio-2
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public RelayCommand(Action execute)
            : this(execute, null)
        { }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute is null.");
            this.canExecute = canExecute;
            this.RaiseCanExecuteChangedAction = RaiseCanExecuteChanged;
            SimpleCommandManager.AddRaiseCanExecuteChangedAction(ref RaiseCanExecuteChangedAction);
        }

        ~RelayCommand()
        {
            RemoveCommand();
        }

        public void RemoveCommand() => SimpleCommandManager.RemoveRaiseCanExecuteChangedAction(RaiseCanExecuteChangedAction);

        bool ICommand.CanExecute(object parameter) => CanExecute;

        public void Execute(object parameter)
        {
            execute();
            SimpleCommandManager.RefreshCommandStates();
        }

        public bool CanExecute
        {
            get { return canExecute == null || canExecute(); }
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());

        private readonly Action RaiseCanExecuteChangedAction;

        public event EventHandler CanExecuteChanged;
    }
}
