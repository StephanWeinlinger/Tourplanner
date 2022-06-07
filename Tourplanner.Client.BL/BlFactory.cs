using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tourplanner.Client.BL.Controllers;

namespace Tourplanner.Client.BL {
	public static class BlFactory {
		private static ApiHandler _apiHandler;
		private static FileExplorer _fileExplorer;

		public static ApiHandler GetApiHandler() {
			if(_apiHandler == null) {
				Dictionary<string, string> config =
					JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("../../../../configClient.json"));
				_apiHandler = new ApiHandler(config["APIUrl"]);
			}

			return _apiHandler;
		}

		public static FileExplorer GetFileExplorer() {
			if(_fileExplorer == null) {
				_fileExplorer = new FileExplorer();
			}

			return _fileExplorer;
		}

		public static PdfHandler CreatePdfHandler(string path) {
			Dictionary<string, string> config =
				JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("../../../../configClient.json"));
			return new PdfHandler(path, config["ImageUrl"]);
        }
	}
}
