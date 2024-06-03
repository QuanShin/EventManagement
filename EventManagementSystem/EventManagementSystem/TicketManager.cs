using System;
using System.Linq;
using System.Security.Principal;

namespace EventManagementSystem 
{
    public class TicketManager
    {
        // Method to update tickets
        public void UpdateTickets(Event chosenEvent, Account account, bool updatedOrNot)
        {
            if (updatedOrNot)
            {
                // Find all tickets associated with the event for the account using the helper method
                var ticketsToUpdate = account.GetTicketsForEvent(chosenEvent.EventCode);

                // Iterate through each ticket and mark it as paid
                foreach (var ticket in ticketsToUpdate)
                {
                    ticket.PaidPrice = true; // Example update logic
                }

                // Print a confirmation message
                Console.WriteLine($"Tickets for event {chosenEvent.EventCode} have been updated.");
            }
            else
            {
                // Print an error message if tickets could not be updated
                Console.WriteLine($"Tickets for event {chosenEvent.EventCode} could not be updated.");
            }
        }

        // Method to cancel tickets
        public void CancelTickets(Event chosenEvent, Account account, bool cancelledOrNot)
        {
            if (cancelledOrNot)
            {
                // Find all tickets associated with the event for the account using the helper method
                var ticketsToCancel = account.GetTicketsForEvent(chosenEvent.EventCode);

                // Iterate through each ticket to remove it from the account and mark it as not booked in the event
                foreach (var ticket in ticketsToCancel)
                {
                    account.NumberOfTickets.Remove(ticket); // Remove the ticket from the account
                    chosenEvent.Tickets[ticket.TicketId] = false; // Mark the ticket as not booked
                    chosenEvent.TicketsAvailable++; // Increase the number of available tickets for the event
                }

                // Print a confirmation message
                Console.WriteLine($"Tickets for event {chosenEvent.EventCode} have been cancelled.");
            }
            else
            {
                // Print an error message if tickets could not be cancelled
                Console.WriteLine($"Tickets for event {chosenEvent.EventCode} could not be cancelled.");
            }
        }
    }

}