﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Csharp2Nem.Sample
{
    class ViewModel : INotifyPropertyChanged
    {
        public string RecipientAddress { get; set; }
        public Wallet Wallet { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand SendCommand { get; private set; }

        public ViewModel()
        {
            Wallet = new Wallet();

            OpenCommand = new DelegateCommand((parameter) =>
            {
                Wallet.Sync();
                OnPropertyChanged(nameof(Wallet));
            });

            SendCommand = new DelegateCommand((parameter) =>
            {

            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
