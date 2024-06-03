using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    public class DeleteEvent : Transaction
    {
        private int ticketNum;
        private Event chosenEvent;

        public int TicketNum
        {
            get { return ticketNum; }
            set { ticketNum = value; }
        }

        public Event ChosenEvent
        {
            get { return chosenEvent; }
            set { chosenEvent = value; }
        }

        public DeleteEvent(DateTime transactionDatetime, int ticketNum, Event chosenEvent)
            : base("Delete", transactionDatetime)
        {
            TicketNum = ticketNum;
            ChosenEvent = chosenEvent;
        }

        public override string ToString()
        {
            return $"{GetTransactionDatetime():dd/MM/yyyy HH:mm} - Delete Event: {ChosenEvent.EventCode}, Tickets: {TicketNum}";
        }
    }
}
