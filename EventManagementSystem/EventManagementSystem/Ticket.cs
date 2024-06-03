using System;

namespace EventManagementSystem
{
    public class Ticket
    {
        private int ticketId;
        private bool paidPrice;
        private DateTime dateOfBook;
        private int eventCode; // Changed to int

        public int TicketId
        {
            get { return ticketId; }
            set { ticketId = value; }
        }

        public bool PaidPrice
        {
            get { return paidPrice; }
            set { paidPrice = value; }
        }

        public DateTime DateOfBook
        {
            get { return dateOfBook; }
            set { dateOfBook = value; }
        }

        public int EventCode
        {
            get { return eventCode; }
            set { eventCode = value; }
        }

        public Ticket(int ticketId, bool paidPrice, DateTime dateOfBook, int eventCode)
        {
            TicketId = ticketId;
            PaidPrice = paidPrice;
            DateOfBook = dateOfBook;
            EventCode = eventCode;
        }
    }
}