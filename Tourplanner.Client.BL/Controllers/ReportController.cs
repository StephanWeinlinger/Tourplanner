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

        public async Task<bool> GenerateTourReport(int id) {
			// get folder
			string path = _fileExplorer.SelectFolder();
            // get newest version of tour
            CombinedTour tour = await _apiHandler.Get<CombinedTour>($"Tour/{id}");
            PdfHandler pdfHandler = BlFactory.CreatePdfHandler($"{path}/tour{id}_report.pdf");
            pdfHandler.GenerateTourReport(tour);
            return true;
        }

        public async Task<bool> GenerateSummarizedReport(string path) {
            // get newest version of tours
            List<CombinedTour> tours = await _apiHandler.Get<List<CombinedTour>>($"Tour");
            PdfHandler pdfHandler = BlFactory.CreatePdfHandler($"{path}/tours_report.pdf");
            pdfHandler.GenerateSummarizedReport(tours);
            return true;
        }
    }
}