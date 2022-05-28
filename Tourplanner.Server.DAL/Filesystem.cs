using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tourplanner.Server.DAL {
	public class Filesystem {
		private WebClient _client;

		public Filesystem() {
			_client = new WebClient();
		}

		~Filesystem() {
			_client.Dispose();
		}

		public void DownloadImage(string url, int id) {
			_client.DownloadFile(url, $"Static/{id}.jpeg");
		}

		public void RemoveImage(int id) {
			File.Delete($"Static/{id}.jpeg");
		}

		public void RemoveAllImages() {
			string[] paths = Directory.GetFiles("Static");
			foreach(string path in paths) {
				File.Delete(path);
			}
		}
	}
}
