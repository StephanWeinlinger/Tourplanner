using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Layout;
using Newtonsoft.Json;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.BL.Controllers {
    public class ReportController {
        private ApiHandler _apiHandler;
        private FileExplorer _fileExplorer;

        public ReportController() {
            _apiHandler = BlFactory.GetApiHandler();
            _fileExplorer = BlFactory.GetFileExplorer();
        }

        public async Task<CustomResponse> GenerateTourReport(int id, string path) {
            // get newest version of tour
            var (tour, response) = await _apiHandler.Get<CombinedTour>($"Tour/{id}");
			if(response.Success) {
	            PdfHandler pdfHandler = BlFactory.CreatePdfHandler($"{path}/tour{id}_report.pdf");
	            pdfHandler.GenerateTourReport(tour);
			}
			return response;
        }

        public async Task<CustomResponse> GenerateSummarizedReport(string path) {
            // get newest version of tours
            var (tours, response) = await _apiHandler.Get<List<CombinedTour>>($"Tour");
            if(response.Success) {
	            PdfHandler pdfHandler = BlFactory.CreatePdfHandler($"{path}/tours_report.pdf");
	            pdfHandler.GenerateSummarizedReport(tours);
			}
            return response;
        }
    }
}