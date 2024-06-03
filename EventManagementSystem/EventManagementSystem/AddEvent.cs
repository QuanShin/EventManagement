using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    public class AddEvent : Transaction
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

        public AddEvent(DateTime transactionDatetime, int ticketNum, Event chosenEvent)
            : base("Add", transactionDatetime)
        {
            TicketNum = ticketNum;
            ChosenEvent = chosenEvent;
        }

        public override string ToString()
        {
            return $"{GetTransactionDatetime():dd/MM/yyyy HH:mm} - Add Event: {ChosenEvent.EventCode}, Tickets: {TicketNum}";
        }
    }

}
