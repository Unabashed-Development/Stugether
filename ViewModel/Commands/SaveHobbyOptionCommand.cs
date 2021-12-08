using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModel.Commands
{
    public class SaveHobbyOptionCommand : ICommand
    {
        private HobbyOptionViewModel VM;


        public SaveHobbyOptionCommand(HobbyOptionViewModel vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.SaveOptionsToDatabase();
        }

        public event EventHandler CanExecuteChanged;
    }
}
