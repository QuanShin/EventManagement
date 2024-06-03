using System;
using System.Collections.Generic;

namespace EventManagementSystem
{
    public class Program
    {
        private static UserUI ui;
        private static Account acc;

        public static void Main(string[] args)
        {
            var eventMng = new EventManager();
            var ticketMng = new TicketManager();
            var transactionMng = new TransactionManager();
            ui = new UserUI(eventMng, ticketMng, transactionMng);

            var adminAccount = new Account(0, "admin", "admin", true);
            ui.AddAccount(adminAccount);
                

            DisplayMenu();
        }

        public static void DisplayMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Event Management System");
                Console.WriteLine("=======================");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Logout");
                Console.WriteLine("4. Display Available Tickets");
                Console.WriteLine("5. Book Ticket");
                Console.WriteLine("6. Cancel Ticket");
                Console.WriteLine("7. Display My Tickets");
                Console.WriteLine("8. Admin: Add Event");
                Console.WriteLine("9. Admin: Delete Event");
                Console.WriteLine("10. Admin: Update Event");
                Console.WriteLine("11. View Transactions");
                Console.WriteLine("12. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AccountLogin(ui);
                        break;
                    case "2":
                        AccountRegister(ui);
                        break;
                    case "3":
                        AccountLogout(acc);
                        break;
                    case "4":
                        DisplayAvailableTickets(ui);
                        break;
                    case "5":
                        UserBookTicket(ui, acc);
                        break;
                    case "6":
                        UserCancelTicket(ui, acc);
                        break;
                    case "7":
                        DisplayTickets(acc);
                        break;
                    case "8":
                        AdminAddEvent(ui, acc);
                        break;
                    case "9":
                        AdminDeleteEvent(ui, acc);
                        break;
                    case "10":
                        AdminUpdateEvent(ui, acc);
                        break;
                    case "11":
                        var transactions = GetAllTransaction(acc, ui);
                        foreach (var transaction in transactions)
                        {
                            Console.WriteLine(transaction);
                        }
                        Console.ReadLine();
                        break;
                    case "12":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        public static void AccountLogin(UserUI ui)
        {
            Console.Write("Enter username: ");
            var username = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();
            acc = ui.AccountLogin(username, password);
            if (acc != null)
            {
                Console.WriteLine("Login successful.");
            }
            else
            {
                Console.WriteLine("Login failed. Please check your username and password.");
            }
            Console.ReadLine();
        }

        public static void AccountRegister(UserUI ui)
        {
            Console.Write("Enter username: ");
            var username = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();
            Console.Write("Are you an admin? (y/n): ");
            var adminOrNot = Console.ReadLine().ToLower() == "y";
            acc = ui.AccountRegister(username, password, adminOrNot);
            if (acc != null)
            {
                Console.WriteLine("Registration successful.");
            }
            else
            {
                Console.WriteLine("Registration failed. Please try again.");
            }
            Console.ReadLine();
        }

        public static void AccountLogout(Account acc)
        {
            acc = null;
            Console.WriteLine("Logout successful.");
            Console.ReadLine();
        }

        public static void DisplayAvailableTickets(UserUI ui)
        {
            Console.Write("Enter event ID: ");
            if (int.TryParse(Console.ReadLine(), out int eventId))
            {
                var tickets = ui.DisplayAvailableTickets(eventId);
                if (tickets.Count > 0)
                {
                    Console.WriteLine("Available Tickets:");
                    foreach (var ticket in tickets)
                    {
                        Console.WriteLine($"Ticket ID: {ticket.TicketId}, Event ID: {ticket.EventCode}");
                    }
                }
                else
                {
                    Console.WriteLine("No available tickets for this event.");
                }
            }
            else
            {
                Console.WriteLine("Invalid event ID.");
            }
            Console.ReadLine();
        }

        public static void UserBookTicket(UserUI ui, Account acc)
        {
            if (acc == null)
            {
                Console.WriteLine("You need to login first.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter event ID: ");
            if (int.TryParse(Console.ReadLine(), out int eventId))
            {
                Console.Write("Enter number of tickets: ");
                if (int.TryParse(Console.ReadLine(), out int ticketNumber))
                {
                    Console.Write("Was the price paid? (y/n): ");
                    var pricePaidInput = Console.ReadLine().ToLower() == "y";
                    if (!pricePaidInput)
                    {
                        Console.WriteLine("Price has not been paid. Booking failed.");

                    }
                    else
                    {
                        var chosenEvent = ui.EventManager.GetEvent(eventId);
                        if (chosenEvent != null)
                        {
                            bool success = ui.UserBookTicket(acc, chosenEvent, ticketNumber, pricePaidInput);
                            if (success)
                            {
                                Console.WriteLine("Tickets booked successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to book tickets. Not enough available tickets.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Event not found.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid number of tickets.");
                }
            }
                else
                {
                    Console.WriteLine("Invalid event ID.");
                }
                Console.ReadLine();
            }

        public static void UserCancelTicket(UserUI ui, Account acc)
        {
            if (acc == null)
            {
                Console.WriteLine("You need to login first.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter event ID: ");
            if (int.TryParse(Console.ReadLine(), out int eventId))
            {
                var chosenEvent = ui.EventManager.GetEvent(eventId);
                if (chosenEvent != null)
                {
                    bool success = ui.UserCancelTicket(acc, chosenEvent);
                    if (success)
                    {
                        Console.WriteLine("Tickets canceled successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to cancel tickets.");
                    }
                }
                else
                {
                    Console.WriteLine("Event not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid event ID.");
            }
            Console.ReadLine();
        }

        public static void DisplayTickets(Account acc)
        {
            if (acc == null)
            {
                Console.WriteLine("You need to login first.");
                Console.ReadLine();
                return;
            }

            var tickets = acc.DisplayTickets();
            Console.WriteLine(tickets);
            Console.ReadLine();
        }

        public static void AdminAddEvent(UserUI ui, Account acc)
        {
            if (acc == null || !acc.AdminOrNot)
            {
                Console.WriteLine("You need to login as an admin first.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter event name: ");
            var name = Console.ReadLine();
            Console.Write("Enter ticket price: ");
            if (double.TryParse(Console.ReadLine(), out double ticketPrice))
            {
                Console.Write("Enter event date (yyyy-mm-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dateTime))
                {
                    Console.Write("Enter number of tickets: ");
                    if (int.TryParse(Console.ReadLine(), out int numOfTickets))
                    {
                        var newEvent = ui.AdminAddEvent(name, ticketPrice, dateTime, numOfTickets);
                        if (newEvent != null)
                        {
                            Console.WriteLine("Event added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to add event.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid number of tickets.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ticket price.");
            }
            Console.ReadLine();
        }

        public static void AdminDeleteEvent(UserUI ui, Account acc)
        {
            if (acc == null || !acc.AdminOrNot)
            {
                Console.WriteLine("You need to login as an admin first.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter event ID: ");
            if (int.TryParse(Console.ReadLine(), out int eventId))
            {
                bool success = ui.AdminDeleteEvent(eventId);
                if (success)
                {
                    Console.WriteLine("Event deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to delete event.");
                }
            }
            else
            {
                Console.WriteLine("Invalid event ID.");
            }
            Console.ReadLine();
        }

        public static void AdminUpdateEvent(UserUI ui, Account acc)
        {
            if (acc == null || !acc.AdminOrNot)
            {
                Console.WriteLine("You need to login as an admin first.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter event ID: ");
            if (int.TryParse(Console.ReadLine(), out int eventId))
            {
                Console.Write("Enter new event name: ");
                var newName = Console.ReadLine();
                Console.Write("Enter new ticket price: ");
                if (double.TryParse(Console.ReadLine(), out double newTicketPrice))
                {
                    Console.Write("Enter new event date (yyyy-mm-dd): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime newDateTime))
                    {
                        Console.Write("Enter new number of tickets: ");
                        if (int.TryParse(Console.ReadLine(), out int newNumOfTickets))
                        {
                            var updatedEvent = ui.AdminUpdateEvent(eventId, newName, newTicketPrice, newDateTime, newNumOfTickets);
                            if (updatedEvent != null)
                            {
                                Console.WriteLine("Event updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to update event.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid number of tickets.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ticket price.");
                }
            }
            else
            {
                Console.WriteLine("Invalid event ID.");
            }
            Console.ReadLine();
        }

        public static List<string> GetAllTransaction(Account acc, UserUI ui)
        {
            return ui.GetAllTransactions();
        }
    }

}