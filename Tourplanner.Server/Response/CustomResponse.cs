using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tourplanner.Server.Response {
	public class CustomResponse {
		public CustomResponse(bool success, Dictionary<string, string> errors) {
			Success = success;
			Errors = new Dictionary<string, string>(errors);
		}
		public bool Success { get; set; }
		public Dictionary<string, string> Errors { get; set; }
	}
}
