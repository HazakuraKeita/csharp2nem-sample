using CSharp2nem.Connectivity;
using CSharp2nem.RequestClients;
using System;
using System.Collections.Generic;
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

        string address;
        string balance;
        string publicKey;
        string vestedBalance;
        int harvestedBlocks;
        double importance;
        Connection connection;

        public Wallet()
        {
            connection = new Connection();
            connection.SetTestnet();
        }

        public void Sync()
        {
            try
            {
                var client = new AccountClient(connection);
                var result = client.BeginGetAccountInfoFromAddress(Address);
                var response = client.EndGetAccountInfo(result);

                Balance = ((double)response.Account.Balance / 1000000).ToString("N6");
                PublicKey = response.Account.PublicKey;
                HarvestedBlocks = response.Account.HarvestedBlocks;
                Importance = response.Account.Importance;
                VestedBalance = ((double)response.Account.VestedBalance / 1000000).ToString("N6");

                var mosaic = new NamespaceMosaicClient(connection);
                var mosaicResult = mosaic.BeginGetMosaicsOwned(Address);
                var mosaicResponse = mosaic.EndGetMosaicsOwned(mosaicResult);
            }
            catch(Exception ex)
            {

            }
        }
    }
}