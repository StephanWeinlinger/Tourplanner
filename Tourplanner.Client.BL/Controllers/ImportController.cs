using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.BL.Controllers {
    public class ImportController {
        private ApiHandler _apiHandler;
        private FileExplorer _fileExplorer;

        public ImportController() {
            _apiHandler = BlFactory.GetApiHandler();
            _fileExplorer = BlFactory.GetFileExplorer();
        }

        public async Task<List<CombinedTour>> ImportTours() {
			// select file
			string path = _fileExplorer.SelectFile();
            // get data from export file
            List<CombinedTour> tours = JsonConvert.DeserializeObject<List<CombinedTour>>(File.ReadAllText(@path));
            // send to api
            return await _apiHandler.Post<List<CombinedTour>>("Import", tours);
        }

        public async Task<bool> ExportTours() {
			// select folder
			string path = _fileExplorer.SelectFolder();
			// get newest version of tours
			List<CombinedTour> tours = await _apiHandler.Get<List<CombinedTour>>($"Tour");
            // generate export file
            File.WriteAllText(@$"{path}/tourplanner.json", JsonConvert.SerializeObject(tours));
            return true;
        }
    }
}