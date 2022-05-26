using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// includes list with logs
namespace Tourplanner.Shared.Model {
	public class CombinedTour {
		public CombinedTour(int id, string name, string description, string from, string to, string transportType, double distance, string time, List<Log> logs) {
			Id = id;
			Name = name;
			Description = description;
			From = from;
			To = to;
			TransportType = transportType;
			Distance = distance;
			Time = time;
			Logs = new List<Log>(logs);
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
		public string Time { get; set; }
		[Required]
		public List<Log> Logs { get; set; }
	}
}
