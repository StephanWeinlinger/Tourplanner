using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.BL.Controllers {
    public class PdfHandler {

	    private Document _document;
	    private string _baseUrl;

        public PdfHandler(string path) {
            PdfWriter writer = new PdfWriter(path);
            PdfDocument pdf = new PdfDocument(writer);
            _document = new Document(pdf);
			// TODO replace with config file
			_baseUrl = "https://localhost:44314/Static/";

		}

        public void GenerateTourReport(CombinedTour tour) {
			AddParagraph(tour.Name, tour.Description);
			AddList(new List<string>() {$"{tour.From} -> {tour.To}", tour.TransportType, $"{tour.Distance} km", tour.Time});
			List<string> headers = new List<string>() {
				"Date",
				"Total Time",
				"Difficulty",
				"Rating",
				"Comment"
			};
			List<List<string>> items = new List<List<string>>();
			foreach(Log entry in tour.Logs) {
				items.Add(new List<string>() {
					entry.Date.Date.ToString(),
					entry.Time,
					entry.Difficulty.ToString(),
					entry.Rating.ToString(),
					entry.Comment
				});
			}
			AddTable(headers, items);
			AddImage(new Uri($"{_baseUrl}{tour.Id}.jpeg"));
			_document.Close();
		}

        public void GenerateSummarizedReport(List<CombinedTour> tours) {

	        _document.Close();
		}

        private void AddParagraph(string header, string text = null) {
	        Paragraph newHeader = new Paragraph(header)
		        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
		        .SetFontSize(14)
		        .SetBold()
		        .SetFontColor(ColorConstants.BLACK);
	        _document.Add(newHeader);
	        if(text != null) {
				_document.Add(new Paragraph(text));
	        }
		}

        private void AddList(List<string> items) {
	        List list = new List()
		        .SetSymbolIndent(12)
		        .SetListSymbol("\u2022")
		        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
	        foreach(string entry in items) {
		        list.Add(new ListItem(entry));
	        }
	        _document.Add(list);
		}

        private void AddTable(List<string> headers, List<List<string>> items) {
	        Table table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
	        foreach(string entry in headers) {
				table.AddHeaderCell(new Cell().Add(new Paragraph(entry)).SetBold().SetBackgroundColor(ColorConstants.GRAY));
	        }
	        foreach(List<string> row in items) {
		        foreach(string entry in row) {
					table.AddCell(entry);
		        }
		        table.StartNewRow();
	        }
	        table.SetBackgroundColor(ColorConstants.WHITE);
	        _document.Add(table);
		}

        private void AddImage(Uri url) {
	        ImageData imageData = ImageDataFactory.Create(url);
	        _document.Add(new Image(imageData));
		}
    }
}
