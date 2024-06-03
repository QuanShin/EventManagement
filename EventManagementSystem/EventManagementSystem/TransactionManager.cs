using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    public class TransactionManager
    {
        private List<Transaction> transactions;

        public TransactionManager()
        {
            transactions = new List<Transaction>();
        }

        public List<Transaction> GetAllTransactions()
        {
            return transactions;
        }

        public void RecordAdd(Event eventObj, int ticketNum, bool addedOrNot)
        {
            var now = DateTime.Now;
            if (addedOrNot)
            {
                var addEvent = new AddEvent(now, ticketNum, eventObj);
                transactions.Add(addEvent);
                Console.WriteLine(addEvent.ToString());
            }
            else
            {
                var failedTransaction = new Transaction("Add Failed", now);
                transactions.Add(failedTransaction);
                Console.WriteLine(failedTransaction.ToString());
            }
        }

        public void RecordDelete(Event eventObj, int ticketNum, bool deletedOrNot)
        {
            var now = DateTime.Now;
            if (deletedOrNot)
            {
                var deleteEvent = new DeleteEvent(now, ticketNum, eventObj);
                transactions.Add(deleteEvent);
                Console.WriteLine(deleteEvent.ToString());
            }
            else
            {
                var failedTransaction = new Transaction("Delete Failed", now);
                transactions.Add(failedTransaction);
                Console.WriteLine(failedTransaction.ToString());
            }
        }

        public void RecordUpdate(Event eventObj, int ticketNum, int eventId, bool ticketPrice, bool updatedOrNot)
        {
            var now = DateTime.Now;
            if (updatedOrNot)
            {
                var updateEvent = new UpdateEvent(now, ticketNum, eventId, ticketPrice, eventObj);
                transactions.Add(updateEvent);
                Console.WriteLine(updateEvent.ToString());
            }
            else
            {
                var failedTransaction = new Transaction("Update Failed", now);
                transactions.Add(failedTransaction);
                Console.WriteLine(failedTransaction.ToString());
            }
        }
        public void RecordBook(Event eventObj, int ticketNum, string username, bool bookedOrNot)
        {
            var now = DateTime.Now;
            if (bookedOrNot)
            {
                var bookEvent = new BookEvent(now, ticketNum, username, eventObj);
                transactions.Add(bookEvent);
                Console.WriteLine(bookEvent.ToString());
            }
            else
            {
                var failedTransaction = new Transaction("Booking Failed", now);
                transactions.Add(failedTransaction);
                Console.WriteLine(failedTransaction.ToString());
            }
        }
    }

}

