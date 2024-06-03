using System;
using System.Collections.Generic;
using System.Linq;

public class Event
{
    private int eventCode;
    private string name = string.Empty;
    private int ticketsAvailable;
    private double pricePerTicket;
    private DateTime dateTime;
    private Dictionary<int, bool> tickets = new Dictionary<int, bool>(); // Ticket ID and whether it is booked

    public int EventCode
    {
        get { return eventCode; }
        set { eventCode = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int TicketsAvailable
    {
        get { return ticketsAvailable; }
        set { ticketsAvailable = value; }
    }

    public double PricePerTicket
    {
        get { return pricePerTicket; }
        set { pricePerTicket = value; }
    }

    public DateTime DateTime
    {
        get { return dateTime; }
        set { dateTime = value; }
    }

    public Dictionary<int, bool> Tickets
    {
        get { return tickets; }
        set { tickets = value; }
    }

    public Event(int eventCode, string name, int ticketsAvailable, double pricePerTicket, DateTime dateTime)
    {
        EventCode = eventCode;
        Name = name;
        TicketsAvailable = ticketsAvailable;
        PricePerTicket = pricePerTicket;
        DateTime = dateTime;
        Tickets = new Dictionary<int, bool>();
        for (int i = 1; i <= ticketsAvailable; i++)
        {
            Tickets.Add(i, false); // Initialize all tickets as not booked
        }
    }

    public bool CanBeUpdatedOrDeleted()
    {
        // Iterate through each value in the Tickets dictionary
        foreach (bool ticketIsBooked in Tickets.Values)
        {
            // Check if the current ticket is booked
            if (ticketIsBooked)
            {
                // If any ticket is booked, return false
                return false;
            }
        }

        // If no tickets are booked, return true
        return true;
    }
}
