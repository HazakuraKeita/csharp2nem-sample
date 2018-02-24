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

        string address;
        string balance;
        Connection connection;

        public Wallet(string address)
        {
            Address = address;
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
            }
            catch(Exception ex)
            {

            }
        }
    }
}