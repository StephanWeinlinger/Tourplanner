using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourplanner.Shared.Model {
	public class MapQuestInformationResponse {
		public MapQuestInformationResponse(double distance, string formattedTime, string sessionId) {
			Distance = distance;
			FormattedTime = formattedTime;
			SessionId = sessionId;
		}
		public double Distance { get; set; }
		public string FormattedTime { get; set; }
		public string SessionId { get; set; }
	}
}
