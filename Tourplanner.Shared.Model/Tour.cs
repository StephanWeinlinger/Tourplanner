using System;

namespace Tourplanner.Shared.Model {
    public class Tour {
	    public Tour(int id, string name, string description, string from, string to, string transportType, float distance, string time) {
		    Id = id;
		    Name = name;
		    Description = description;
		    From = from;
		    To = to;
		    TransportType = transportType;
		    Distance = distance;
		    Time = time;
	    }
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public string TransportType { get; set; }
		public float Distance { get; set; }
		public string Time { get; set; } // MapQuest returns it as a string and postgresql datetime only supports HH up to 24
    }
}
