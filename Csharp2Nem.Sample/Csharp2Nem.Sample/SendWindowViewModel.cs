using CSharp2nem.Model.Transfer.Mosaics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Csharp2Nem.Sample
{
    public class SendWindowViewModel
    {
        public string Recipient { get; private set; }
        public string Amount { get; set; }
        public string Message { get; set; }
        public Wallet Wallet { get; private set; }
        public Mosaic SelectedMosaic { get; set; }
        public Action Closing { get; set; }
        public ICommand SendCommand { get; private set; }

        public SendWindowViewModel(Wallet wallet, string address)
        {
            Recipient = address;
            Wallet = wallet;
            SendCommand = new DelegateCommand((parameter) =>
            {
                try
                {
                    var isSuccess = Wallet.Send(Recipient, Message, SelectedMosaic, long.Parse(Amount));

                    if (isSuccess)
                    {
                        Closing();
                    }
                }
                catch (Exception ex)
                {

                }
            });
        }
    }
}
