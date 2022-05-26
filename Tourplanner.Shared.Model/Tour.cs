using System;
using System.ComponentModel.DataAnnotations;

namespace Tourplanner.Shared.Model {
    public class Tour {
	    public Tour() { }
	    public Tour(string name, string description, string from, string to, string transportType, double distance, string time) {
		    Name = name;
		    Description = description;
		    From = from;
		    To = to;
		    TransportType = transportType;
		    Distance = distance;
		    Time = time;
	    }
	    public Tour(int id, string name, string description, string from, string to, string transportType, double distance, string time) {
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
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string From { get; set; }
		[Required]
		public string To { get; set; }
		[Required]
		public string TransportType { get; set; }
		[Required]
		public double Distance { get; set; }
		[Required]
		public string Time { get; set; } // MapQuest returns it as a string and postgresql datetime only supports HH up to 24
	}
}
