using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tourplanner.Server.Response {
	public class CustomResponse {
		public CustomResponse(bool success, List<string> errors) {
			Success = success;
			Errors = new List<string>(errors);
		}
		public bool Success { get; set; }
		public List<string> Errors { get; set; }
	}
}
