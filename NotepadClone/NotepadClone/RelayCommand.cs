﻿using System;
using System.Windows.Input;

namespace Tema1
{
    class RelayCommand : ICommand
    {
        private readonly Action<object> commandTask;

        public RelayCommand(Action<object> workToDo)
        {
            commandTask = workToDo;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            commandTask(parameter);
        }
    }
}