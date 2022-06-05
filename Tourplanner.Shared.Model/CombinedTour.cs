using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// includes list with logs
namespace Tourplanner.Shared.Model {
	public class CombinedTour {
		public CombinedTour() { }

		public CombinedTour(string name, string description, string from, string to, string transportType,
			List<Log> logs) {
			Name = name;
			Description = description;
			From = from;
			To = to;
			TransportType = transportType;
			Logs = new List<Log>(logs);
		}

		public CombinedTour(Tour tour, List<Log> logs) {
			Id = tour.Id;
			Name = tour.Name;
			Description = tour.Description;
			From = tour.From;
			To = tour.To;
			TransportType = tour.TransportType;
			Distance = tour.Distance;
			Time = tour.Time;
			Logs = new List<Log>(logs);
		}

		public CombinedTour(string name, string description, string from, string to, string transportType) {
			Name = name;
			Description = description;
			From = from;
			To = to;
			TransportType = transportType;
		}

		public int Id { get; set; }
		[Required] public string Name { get; set; }
		[Required] public string Description { get; set; }
		[Required] public string From { get; set; }
		[Required] public string To { get; set; }
		[Required] public string TransportType { get; set; }
		public double Distance { get; set; }
		public string Time { get; set; }
		[Required] public List<Log> Logs { get; set; }
	}
}
