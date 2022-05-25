using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourplanner.Shared.Model {
	public class Log {
		public int Id { get; set; }
		public int TourId { get; set; }
		public DateTime Date { get; set; }
		public string Comment { get; set; }
		public int Difficulty { get; set; }
		public string Time { get; set; }
		public int Rating { get; set; }
	}
}
