using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModel.Commands
{
    public class CheckBoxCheckCommand : ICommand
    {
        private HobbyOptionViewModel VM;

        public CheckBoxCheckCommand(HobbyOptionViewModel vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.UpdateRelationCheckBox();
        }

        public event EventHandler CanExecuteChanged;
    }
}
