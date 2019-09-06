using System;
using System.Linq;
using System.Collections.Generic;

namespace ModasSeedData
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // first create Locations list
            List<Location> Locations = new List<Location>(){
                // Add locations
                new Location { LocationId = 1, Name = "Kitchen"},
                new Location { LocationId = 2, Name = "Living Room"},
                new Location { LocationId = 3, Name = "Front Office"},
                new Location { LocationId = 5, Name = "Basement"},
            };
            // create date object containing current date/time
            DateTime localDate = DateTime.Now;
            // subtract 6 months from date
            DateTime eventDate = localDate.AddMonths(-6);
            // instantiate Random class
            Random rand = new Random();
            // create list to store events (Events)
            List<Event> events = new List<Event>();
            // loop for each day in the range from 6 months ago to today
            while (eventDate < localDate)
            {
                // random between 0 and 5 determines the number of events to occur on a given day
                int num = rand.Next(0, 6);
                // a sorted list will be used to store daily events sorted by date/time - each time an event is added, the list is re-sorted
                SortedList<DateTime, Event> dailyEvents = new SortedList<DateTime, Event>();
                // for loop to generate times for each event
                for (int i = 0; i < num; i++)
                {
                    // random between 0 and 23 for hour of the day
                    int hour = rand.Next(0, 24);
                    // random between 0 and 59 for minute of the day
                    int minute = rand.Next(0, 60);
                    // random between 0 and 59 for seconds of the day
                    int second = rand.Next(0, 60);
                    // random location (use Locations)
                    int loc = rand.Next(0, Locations.Count);
                    // create date/time for event
                    DateTime d = new DateTime(eventDate.Year, eventDate.Month, eventDate.Day, hour, minute, second);
                    // create event from date/time and location
                    Event e = new Event { Flagged = false, Location = Locations[loc], LocationId = Locations[loc].LocationId, TimeStamp = d };
                    // add daily event to sorted list
                    dailyEvents.Add(d, e);
                }

                // loop thru sorted list for daily events
                foreach (var item in dailyEvents)
                {
                    // add daily event to Events
                    events.Add(item.Value);
                }
                // add 1 day to eventDate
                eventDate = eventDate.AddDays(1);
            }
            // loop thru Events
            foreach (Event e in events)
            {
                // display event at console
                Console.WriteLine($"{e.TimeStamp}, {e.Location.Name}");
            }
        }
    }

    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
    }

    public class Event
    {
        public int EventId { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Flagged { get; set; }
        // foreign key for location 
        public int LocationId { get; set; }
        // navigation property
        public Location Location { get; set; }
    }
}
