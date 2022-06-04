using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// includes list with logs
namespace Tourplanner.Shared.Model {
	public class CombinedTour {

		public event PropertyChangedEventHandler? PropertyChanged;
		public CombinedTour displaytour;
		public Log displayLog;

		public CombinedTour(CombinedTour displaytour) {
			this.displaytour = displaytour;
			
		}
		public int TourID {
			get => Id; set {
				Id = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TourID)));
			}
		}

		public string Title {
			get => Name; set {
				Name = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
			}
		}
		public string TourDescription {
			get => Description; set {
				Description = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TourDescription)));
			}
		}
		public string TourFrom {
			get => From; set {
				From = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TourFrom)));
			}
		}
		public string TourTo {
			get => To; set {
				To = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TourTo)));
			}
		}
		public string TourTransportType {
			get => TransportType; set {
				TransportType = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TourTransportType)));
			}
		}
		public double TourDistance {
			get => Distance; set {
				Distance = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TourDistance)));
			}
		}
		public string TourTime {
			get => Time; set {
				Time = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TourTime)));
			}
		}

		





		public CombinedTour() {}
		public CombinedTour(string name, string description, string from, string to, string transportType, List<Log> logs) {
			Name = name;
			Description = description;
			From = from;
			To = to;
			TransportType = transportType;
			Logs = new List<Log>(logs);
		}
		public CombinedTour(Tour tour, List<Log> logs) {
			Id = tour.Id;
			Name = tour.Name;
			Description = tour.Description;
			From = tour.From;
			To = tour.To;
			TransportType = tour.TransportType;
			Distance = tour.Distance;
			Time = tour.Time;
			Logs = new List<Log>(logs);
		}
		public CombinedTour(string name, string description, string from, string to, string transportType) {
			Name = name;
			Description = description;
			From = from;
			To = to;
			TransportType = transportType;
		}
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string From { get; set; }
		[Required]
		public string To { get; set; }
		[Required]
		public string TransportType { get; set; }
		public double Distance { get; set; }
		public string Time { get; set; }
		[Required]
		public List<Log> Logs { get; set; }


		public string GetLogComment() {
			string LogComment = "";
			foreach(Log items in Logs) {
				if(items.TourId == Id) {
					LogComment = items.Comment;
				}
				
			}
			return LogComment;
		}
		public int GetLogDifficulty() {
			int LogDifficulty  = -1;
			foreach(Log items in Logs) {
				if(items.TourId == Id) {
					LogDifficulty = items.Difficulty;
				}

			}
			return LogDifficulty;
		}
		public string GetLogTime() {
			string LogTime = "";
			foreach(Log items in Logs) {
				if(items.TourId == Id) {
					LogTime = items.Time;
				}

			}
			return LogTime;
		}
		public int GetLogRating() {
			int LogRating = -1 ;
			foreach(Log items in Logs) {
				if(items.TourId == Id) {
					LogRating = items.Rating;
				}

			}
			return LogRating;
		}
		public string GetLogDate() {
			string LogComment = "";
			foreach(Log items in Logs) {
				if(items.TourId == Id) {
					LogComment = items.Comment;
				}

			}
			return LogComment;
		}

		public int GetTourPopularity() {
			int Tourpopularity = 0;
			foreach(Log items in Logs) {

				// TODO Berechnung der popularity
				Tourpopularity++;
			}
			return Tourpopularity;
		}

		public int GetTourFriendlyness() {
			int TourFriendlyness = 0;
			foreach(Log items in Logs) {

				// TODO Berechnung der Friendlyness
				TourFriendlyness++;
			}
			return TourFriendlyness;
		}
	}

	
}
