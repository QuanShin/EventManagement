using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EventManagementSystem
{
    public class UserUI
    {
        private EventManager eventMng;
        private TicketManager ticketMng;
        private TransactionManager transactionMng;
        private Dictionary<int, Account> AccountDictionary;

        // Constructor
        public UserUI(EventManager evMg, TicketManager ticMg, TransactionManager transMg)
        {
            eventMng = evMg;
            ticketMng = ticMg;
            transactionMng = transMg;
            AccountDictionary = new Dictionary<int, Account>();
        }

        public EventManager EventManager
        {

        get { return eventMng; } 
        
        }

        public void AddAccount(Account account)
        {
            if (!AccountDictionary.ContainsKey(account.Id))
            {
                AccountDictionary[account.Id] = account;
            }
        }

        public List<string> GetAllTransactions()
        {
            List<string> transactionStrings = new List<string>();

            List<Transaction> allTransactions = transactionMng.GetAllTransactions();

            foreach (Transaction transaction in allTransactions)
            {
                string transactionString = transaction.ToString();

                transactionStrings.Add(transactionString);
            }

            // Return the list of string representations of the transactions
            return transactionStrings;
        }

        public Account AccountLogin(string username, string password)
        {
            // Iterate through each account in the dictionary
            foreach (var account in AccountDictionary.Values)
            {
                // Check if the current account matches the username and password
                if (account.Username == username && account.Password == password)
                {
                    // Return the matching account
                    return account;
                }
            }
            // If no account matches, return null
            return null;
        }

        public Account AccountRegister(string username, string password, bool adminOrNot)
        {
            // Check if the username already exists
            bool usernameExists = AccountDictionary.Values.Any(acc => acc.Username == username);

            // If the username exists, display a message and return null
            if (usernameExists)
            {
                Console.WriteLine("Username already exists. Please choose a different username.");
                return null;
            }

            // Check if the admin flag is true
            if (adminOrNot)
            {
                //  If adminOrNot is true, display a message and return null
                Console.WriteLine("New admin accounts cannot be created.");
                return null;
            }

            //  Generate a new ID for the account
            int newId;
            if (AccountDictionary.Count > 0)
            {
                // Get the maximum existing ID and add 1
                newId = AccountDictionary.Keys.Max() + 1;
            }
            else
            {
                // If there are no accounts, start with ID 1
                newId = 1;
            }

            //  Create a new Account object with the generated ID, username, password, and admin status
            var newAccount = new Account(newId, username, password, adminOrNot);

            //  Add the new account to the AccountDictionary
            AccountDictionary[newId] = newAccount;

            //  Return the newly created account
            return newAccount;
        }

        public List<Ticket> DisplayAvailableTickets(int eventId)
        {
            // Get the event by ID
            var chosenEvent = eventMng.GetEvent(eventId);

            // Check if the event exists
            if (chosenEvent != null)
            {
                // Create a list to store available tickets
                var availableTickets = new List<Ticket>();

                // Iterate through each ticket in the event
                foreach (var ticket in chosenEvent.Tickets)
                {
                    // Check if the ticket is not booked
                    if (!ticket.Value)
                    {
                        // Add the ticket to the list of available tickets
                        availableTickets.Add(new Ticket(ticket.Key, false, DateTime.Now, eventId));
                    }
                }

                // Return the list of available tickets
                return availableTickets;
            }

            // If the event does not exist, return an empty list
            return new List<Ticket>();
        }

        public bool UserBookTicket(Account account, Event chosenEvent, int ticketNumber, bool pricePaid)
        {
            if (!pricePaid)
            {
                transactionMng.RecordBook(chosenEvent, ticketNumber, account.Username, false);
                return false;
            }
            //  Check if there are enough available tickets in the chosen event
            if (chosenEvent.TicketsAvailable < ticketNumber)
            {
                // Record a failed booking transaction due to insufficient tickets
                transactionMng.RecordBook(chosenEvent, ticketNumber, account.Username, false);
                return false;
            }

            //  Book the required number of tickets
            for (int i = 0; i < ticketNumber; i++)
            {
                // Find the first available (not booked) ticket in the event
                int ticketId = -1; // Initialize with an invalid value
                foreach (var ticket in chosenEvent.Tickets)
                {
                    if (!ticket.Value) // If the ticket is not booked
                    {
                        ticketId = ticket.Key; // Get the ticket ID
                        break; // Exit the loop after finding the first available ticket
                    }
                }

                // Ensure a valid ticket ID was found
                if (ticketId == -1)
                {
                    // This should not happen if TicketsAvailable check above is correct
                    throw new InvalidOperationException("Unexpected error: No available tickets found.");
                }

                // Mark the ticket as booked in the chosen event
                chosenEvent.Tickets[ticketId] = true;

                // Add the booked ticket to the account's list of tickets
                account.NumberOfTickets.Add(new Ticket(ticketId, pricePaid, DateTime.Now, chosenEvent.EventCode));

                // Decrease the number of available tickets in the chosen event
                chosenEvent.TicketsAvailable--;
            }

            //  Record a successful booking transaction
            transactionMng.RecordBook(chosenEvent, ticketNumber, account.Username, true);
            return true;
        }
        public bool UserCancelTicket(Account account, Event eventObj)
        {
            var ticketsToCancel = account.GetTicketsForEvent(eventObj.EventCode);
            if (ticketsToCancel.Count > 0)
            {
                foreach (var ticket in ticketsToCancel)
                {
                    account.NumberOfTickets.Remove(ticket);
                    eventObj.Tickets[ticket.TicketId] = false; // Mark as not booked
                    eventObj.TicketsAvailable++;
                }
                transactionMng.RecordDelete(eventObj, ticketsToCancel.Count, true);
                return true;
            }
            transactionMng.RecordDelete(eventObj, 0, false);
            return false;
        }

        // Admin Add Event
        public Event AdminAddEvent(string name, double ticketPrice, DateTime dateTime, int numOfTickets)
        {
            var newEvent = new Event(eventMng.NextEventId++, name, numOfTickets, ticketPrice, dateTime);
            if (eventMng.AddEvent(newEvent))
            {
                transactionMng.RecordAdd(newEvent, numOfTickets, true);
                return newEvent;
            }
            transactionMng.RecordAdd(newEvent, numOfTickets, false);
            return null;
        }

        // Admin Delete Event
        public bool AdminDeleteEvent(int id)
        {
            var eventObj = eventMng.GetEvent(id);
            if (eventObj != null && eventObj.CanBeUpdatedOrDeleted())
            {
                if (eventMng.DeleteEvent(id))
                {
                    transactionMng.RecordDelete(eventObj, eventObj.Tickets.Count, true);
                    return true;
                }
            }
            transactionMng.RecordDelete(eventObj, 0, false);
            return false;
        }

        // Admin Update Event
        public Event AdminUpdateEvent(int id, string newName, double newTicketPrice, DateTime newDateTime, int newNumOfTickets)
        {
            var eventObj = eventMng.GetEvent(id);
            if (eventObj != null && eventObj.CanBeUpdatedOrDeleted())
            {
                eventObj.Name = newName;
                eventObj.PricePerTicket = newTicketPrice;
                eventObj.DateTime = newDateTime;
                eventObj.TicketsAvailable = newNumOfTickets;
                eventObj.Tickets = new Dictionary<int, bool>();
                for (int i = 1; i <= newNumOfTickets; i++)
                {
                    eventObj.Tickets.Add(i, false);
                }
                transactionMng.RecordUpdate(eventObj, newNumOfTickets, id, true, true);
                return eventObj;
            }
            transactionMng.RecordUpdate(eventObj, 0, id, false, false);
            return null;
        }
    }
}
