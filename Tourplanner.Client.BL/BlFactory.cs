using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourplanner.Client.BL.Controllers;

namespace Tourplanner.Client.BL {
	public static class BlFactory {
		private static ApiHandler _apiHandler;
		private static FileExplorer _fileExplorer;

		public static ApiHandler GetApiHandler() {
			if(_apiHandler == null) {
				_apiHandler = new ApiHandler();
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
            return new PdfHandler(path);
        }
	}
}
