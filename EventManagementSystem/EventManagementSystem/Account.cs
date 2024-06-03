using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    public class Account
    {
        private int id;
        private string username;
        private string password;
        private bool adminOrNot;
        private List<Ticket> numberOfTickets;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public bool AdminOrNot
        {
            get { return adminOrNot; }
            set { adminOrNot = value; }
        }

        public List<Ticket> NumberOfTickets
        {
            get { return numberOfTickets; }
            set { numberOfTickets = value; }
        }

        public Account(int id, string username, string password, bool adminOrNot)
        {
            Id = id;
            Username = username;
            Password = password;
            AdminOrNot = adminOrNot;
            NumberOfTickets = new List<Ticket>();
        }

        public string DisplayTickets()
        {
            if (NumberOfTickets.Count == 0)
            {
                return "No tickets booked.";
            }

            var ticketsInfo = new List<string>();
            foreach (var ticket in NumberOfTickets)
            {
                ticketsInfo.Add($"Ticket ID: {ticket.TicketId}, Paid: {ticket.PaidPrice}, Date of Booking: {ticket.DateOfBook}, Event: {ticket.EventCode}");
            }
            return string.Join("\n", ticketsInfo);
        }

        // Helper method to get all tickets for a specific event
        public List<Ticket> GetTicketsForEvent(int eventCode)
        {
            // Access the list of all tickets for the account
            var allTickets = NumberOfTickets;

            // Use the Where method to filter tickets with the specified eventCode
            var filteredTickets = allTickets.Where(ticket =>
            {
                // Check if the EventCode of the current ticket matches the specified eventCode
                bool isMatchingEventCode = ticket.EventCode == eventCode;

                // Return the result of the check (true if it matches, false otherwise)
                return isMatchingEventCode;
            });

            // Convert the filtered IEnumerable<Ticket> to a List<Ticket>
            List<Ticket> ticketsForEvent = filteredTickets.ToList();

            // Return the list of tickets for the specified event
            return ticketsForEvent;
        }
    }

}
