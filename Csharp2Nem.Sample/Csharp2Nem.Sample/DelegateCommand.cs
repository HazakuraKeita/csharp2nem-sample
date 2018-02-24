﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Csharp2Nem.Sample
{
    class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Action<object> action;

        public DelegateCommand(Action<object> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action?.Invoke(parameter);
        }
    }
}
