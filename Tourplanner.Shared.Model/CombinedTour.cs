using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// includes list with logs
namespace Tourplanner.Shared.Model {
	public class CombinedTour {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public string TransportType { get; set; }
		public float Distance { get; set; }
		public string Time { get; set; }
		public List<Log> Logs { get; set; }
	}
}
