using System;
using System.Collections.Generic;

public class EventManager
{
    // Private field
    private Dictionary<int, Event> EventDict;
    private int nextEventId;

    // Constructor
    public EventManager()
    {
        EventDict = new Dictionary<int, Event>();
        nextEventId = 1;
    }

    public int NextEventId
    {
        get { return nextEventId; }
        set { nextEventId = value; }
    }
    public Event GetEvent(int eventId)
    {
        if (EventDict.TryGetValue(eventId, out var eventObj))
        {
            return eventObj;
        }
        return null;
    }

    // Method to add an event
    public bool AddEvent(Event eventObj)
    {
        if (EventDict.ContainsKey(eventObj.EventCode))
        {
            return false; // Event already exists
        }

        EventDict[eventObj.EventCode] = eventObj;
        return true;
    }

    // Method to delete an event
    public bool DeleteEvent(int ID)
    {
        if (!EventDict.ContainsKey(ID))
        {
            return false; // Event does not exist
        }

        var eventToDelete = EventDict[ID];

        if (!eventToDelete.CanBeUpdatedOrDeleted())
        {
            return false; // Some tickets are already booked
        }

        EventDict.Remove(ID);
        return true;
    }

    // Method to update an event
    public bool UpdateEvent(int ID, string newName, int newTicketsAvailable, double newPricePerTicket, DateTime newDateTime)
    {
        if (!EventDict.ContainsKey(ID))
        {
            return false; // Event does not exist
        }

        var eventToUpdate = EventDict[ID];

        if (!eventToUpdate.CanBeUpdatedOrDeleted())
        {
            return false; // Some tickets are already booked
        }

        eventToUpdate.Name = newName;
        eventToUpdate.TicketsAvailable = newTicketsAvailable;
        eventToUpdate.PricePerTicket = newPricePerTicket;
        eventToUpdate.DateTime = newDateTime;

        // Update the tickets dictionary
        eventToUpdate.Tickets.Clear();
        for (int i = 1; i <= newTicketsAvailable; i++)
        {
            eventToUpdate.Tickets.Add(i, false);
        }
        return true;
    }

    // Method to display all events
    public List<Event> DisplayListOfEvents()
    {
        return new List<Event>(EventDict.Values);
    }
}
