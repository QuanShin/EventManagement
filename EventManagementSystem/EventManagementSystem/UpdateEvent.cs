using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    public class UpdateEvent : Transaction
    {
        private int ticketNum;
        private int eventId;
        private bool ticketPrice;
        private Event chosenEvent;

        public int TicketNum
        {
            get { return ticketNum; }
            set { ticketNum = value; }
        }

        public int EventId
        {
            get { return eventId; }
            set { eventId = value; }
        }

        public bool TicketPrice
        {
            get { return ticketPrice; }
            set { ticketPrice = value; }
        }

        public Event ChosenEvent
        {
            get { return chosenEvent; }
            set { chosenEvent = value; }
        }

        public UpdateEvent(DateTime transactionDatetime, int ticketNum, int eventId, bool ticketPrice, Event chosenEvent)
            : base("Update", transactionDatetime)
        {
            TicketNum = ticketNum;
            EventId = eventId;
            TicketPrice = ticketPrice;
            ChosenEvent = chosenEvent;
        }

        public override string ToString()
        {
            return $"{GetTransactionDatetime():dd/MM/yyyy HH:mm} - Update Event: {ChosenEvent.EventCode}, Tickets: {TicketNum}, Event ID: {EventId}";
        }
    }
}
