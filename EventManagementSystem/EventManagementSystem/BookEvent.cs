using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    public class BookEvent : Transaction
    {
        private int ticketNum;
        private string username;
        private Event eventObj;

        public BookEvent(DateTime transactionDatetime, int ticketNum, string username, Event eventObj)
            : base("Book Event", transactionDatetime)
        {
            this.ticketNum = ticketNum;
            this.username = username;
            this.eventObj = eventObj;
        }

        public override string ToString()
        {
            return $"{GetTransactionDatetime:dd/MM/yyyy HH:mm} Book Event - Username: {username}, Event {eventObj.EventCode}, Tickets: {ticketNum}";
        }
    }
}

