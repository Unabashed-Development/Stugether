using System;
using System.Windows.Input;

namespace ViewModel.Commands
{
    /// <summary>
    /// Handles the click events of the main menu buttons
    /// </summary>
    public class NavigateButtonCommand : ICommand
    {
        /// <summary>
        /// The viewmodel modified by this command
        /// </summary>
        public MainPageViewModel VM { get; set; }

        /// <summary>
        /// Creates a new NavigateButtonCommand
        /// </summary>
        /// <param name="vm">The viewmodel modified by this command</param>
        public NavigateButtonCommand(MainPageViewModel vm)
        {
            VM = vm;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">This value can be null, because it is not used.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter) => VM.MainNavigationItems.Count > 0;

        /// <summary>
        /// Updates the viewmodel with the given information.
        /// </summary>
        /// <param name="parameter">The String or MainMenuNavigationItemData containing information about information where to navigate to</param>
        public void Execute(object parameter)
        {
            if (parameter.GetType() == typeof(string))
            {
                VM.CurrentVisiblePage = (string)parameter;
            }
            else if (parameter.GetType() == typeof(MainPageViewModel.MainMenuNavigationItemData))
            {
                var data = (MainPageViewModel.MainMenuNavigationItemData)parameter;
                VM.CurrentVisiblePage = data.Page;
            }
            else
            {
                throw new InvalidOperationException("Accepts only strings");
            }
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;
    }
}
