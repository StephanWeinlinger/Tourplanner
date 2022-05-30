using System;
using System.Collections.Generic;
using System.Globalization;
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
			AddList(new List<string>() { $"{tour.From} -> {tour.To}", tour.TransportType, $"{tour.Distance} km", tour.Time });
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
			List<string> headers = new List<string>() {
				"Name",
				"AV Time",
				"AV Difficulty",
				"AV Rating",
			};
			List<List<string>> items = new List<List<string>>();
			foreach(CombinedTour tour in tours) {
				double totalTime = 0;
				int totalDifficulty = 0;
				int totalRating = 0;
				foreach(Log entry in tour.Logs) {
					totalTime += new TimeSpan(int.Parse(entry.Time.Split(':')[0]),
						int.Parse(entry.Time.Split(':')[1]),
						int.Parse(entry.Time.Split(':')[2])).TotalSeconds;
					totalDifficulty += entry.Difficulty;
					totalRating += entry.Rating;
				}
				if(tour.Logs.Count != 0) {
					TimeSpan averageTime = TimeSpan.FromSeconds((long) totalTime / tour.Logs.Count);
					items.Add(new List<string>() {
						tour.Name,
						String.Format("{0}:{1}:{2}", (int) averageTime.TotalHours, averageTime.Minutes, averageTime.Seconds),
						(totalDifficulty / tour.Logs.Count).ToString(),
						(totalRating / tour.Logs.Count).ToString()
					});
				} else {
					items.Add(new List<string>() {
						tour.Name,
						"00:00:00",
						"0",
						"0"
					});
				}
			}
			AddTable(headers, items);
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
