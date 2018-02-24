using System;
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
        public Wallet Wallet { get; private set; }
        public ICommand StartCommand { get; private set; }

        public ViewModel()
        {
            StartCommand = new DelegateCommand((parameter) =>
            {
                Wallet = new Wallet("TAQBNLN7NTE3CY2SZ7I5XTZZWBL5AADAXWQ6ZNOP");
                Wallet.Sync();
                OnPropertyChanged(nameof(Wallet));
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
