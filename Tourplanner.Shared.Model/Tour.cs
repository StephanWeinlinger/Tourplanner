using System;

namespace Tourplanner.Shared.Model {
    public class Tour {
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
