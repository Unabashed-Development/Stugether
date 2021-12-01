using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ViewModel.Commands
{
    public static class SimpleCommandManager // Source: https://stackoverflow.com/questions/34996198/the-name-commandmanager-does-not-exist-in-the-current-context-visual-studio-2
    {
        private static List<Action> _raiseCanExecuteChangedActions = new List<Action>();

        public static void AddRaiseCanExecuteChangedAction(ref Action raiseCanExecuteChangedAction)
        {
            _raiseCanExecuteChangedActions.Add(raiseCanExecuteChangedAction);
        }

        public static void RemoveRaiseCanExecuteChangedAction(Action raiseCanExecuteChangedAction)
        {
            _raiseCanExecuteChangedActions.Remove(raiseCanExecuteChangedAction);
        }

        public static void AssignOnPropertyChanged(ref PropertyChangedEventHandler propertyEventHandler)
        {
            propertyEventHandler += OnPropertyChanged;
        }

        private static void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // This if clause is to prevent an infinity loop
            if (e.PropertyName != "CanExecute")
            {
                RefreshCommandStates();
            }
        }

        public static void RefreshCommandStates()
        {
            for (int i = 0; i < _raiseCanExecuteChangedActions.Count; i++)
            {
                Action raiseCanExecuteChangedAction = _raiseCanExecuteChangedActions[i];
                if (raiseCanExecuteChangedAction != null)
                {
                    raiseCanExecuteChangedAction.Invoke();
                }
            }
        }
    }
}