using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    public class Transaction
    {
        private DateTime transactionDatetime;
        private string transactionType;

        public Transaction(string type, DateTime dt)
        {
            transactionType = type;
            transactionDatetime = dt;
        }

        public DateTime GetTransactionDatetime()
        {
            return transactionDatetime;
        }

        public string GetTransactionType()
        {
            return transactionType;
        }

        public override string ToString()
        {
            return $"{transactionDatetime:dd/MM/yyyy HH:mm} - {transactionType}";
        }
    }

}
