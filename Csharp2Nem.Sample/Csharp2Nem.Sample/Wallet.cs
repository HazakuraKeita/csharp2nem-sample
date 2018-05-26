using CSharp2nem.Connectivity;
using CSharp2nem.Model.AccountSetup;
using CSharp2nem.Model.DataModels;
using CSharp2nem.Model.Transfer.Mosaics;
using CSharp2nem.RequestClients;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp2Nem.Sample
{
    class Wallet
    {
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        public string PublicKey
        {
            get { return publicKey; }
            set { publicKey = value; }
        }
        public int HarvestedBlocks
        {
            get { return harvestedBlocks; }
            set { harvestedBlocks = value; }
        }
        public string VestedBalance
        {
            get { return vestedBalance; }
            set { vestedBalance = value; }
        }
        public double Importance
        {
            get { return importance; }
            set { importance = value; }
        }
        public ObservableCollection<Mosaic> Mosaics
        {
            get { return mosaics; }
            set { mosaics = value; }
        }

        string address;
        string balance;
        string publicKey;
        string vestedBalance;
        int harvestedBlocks;
        double importance;
        ObservableCollection<Mosaic> mosaics;
        Connection connection;

        public Wallet()
        {
            Mosaics = new ObservableCollection<Mosaic>();

            connection = new Connection();
            connection.SetTestnet();
        }

        public void Sync()
        {
            try
            {
                // To get an account information from the address.
                var accountClient = new AccountClient(connection);
                var accountResult = accountClient.BeginGetAccountInfoFromAddress(Address);
                var accountResponse = accountClient.EndGetAccountInfo(accountResult);

                Balance = ((double)accountResponse.Account.Balance / 1000000).ToString("N6");
                PublicKey = accountResponse.Account.PublicKey.Substring(0,10) + "**********...";
                HarvestedBlocks = accountResponse.Account.HarvestedBlocks;
                Importance = accountResponse.Account.Importance;
                VestedBalance = ((double)accountResponse.Account.VestedBalance / 1000000).ToString("N6");

                // To get mosaic information of the account from the address.
                var mosaicClient = new NamespaceMosaicClient(connection);
                var mosaicResult = mosaicClient.BeginGetMosaicsOwned(Address);
                var mosaicResponse = mosaicClient.EndGetMosaicsOwned(mosaicResult);

                foreach(var data in mosaicResponse.Data)
                {
                    // XEM is mosaic, but it does not shown because it has already been shown on the above.
                    if (data.MosaicId.Name != "xem")
                    {
                        Mosaics.Add(new Mosaic(data.MosaicId.NamespaceId, data.MosaicId.Name, data.Quantity));
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void Send(string address, string message, Mosaic mosaic, long amount)
        {
            try
            {
                var accountFactory = new PrivateKeyAccountClientFactory(connection);
                var accClient = accountFactory.FromPrivateKey(string.Empty);
                var mosaicList = new List<Mosaic>() { mosaic };

                var transData = new TransferTransactionData()
                {
                    Amount = amount,
                    Message = message,
                    RecipientAddress = address,
                    ListOfMosaics = mosaicList,
                };
                
                var asyncResult = accClient.BeginSendTransaction(transData);

                while (!asyncResult.IsCompleted) ;

                var response = accClient.EndTransaction(asyncResult);
            }
            catch (Exception ex)
            {

            }
        }
    }
}