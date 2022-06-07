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

        public async Task<(List<CombinedTour>, CustomResponse)> ImportTours(string path) {
	        // get data from export file
            List<CombinedTour> tours = JsonConvert.DeserializeObject<List<CombinedTour>>(File.ReadAllText(@path));
            // send to api
            return await _apiHandler.Post<List<CombinedTour>>("Import", tours);
        }

        public async Task<CustomResponse> ExportTours(string path) {
			// get newest version of tours
			var (tours, response) = await _apiHandler.Get<List<CombinedTour>>($"Tour");
            // generate export file
            File.WriteAllText(@$"{path}/tourplanner.json", JsonConvert.SerializeObject(tours));
            return response;
        }
    }
}