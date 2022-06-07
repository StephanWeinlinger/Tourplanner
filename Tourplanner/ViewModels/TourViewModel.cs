using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.ViewModels {
	public class TourViewModel : ViewModelBase {
		private readonly Tour _tour;

		public ObservableCollection<LogViewModel> LogsCollection;
		public IEnumerable<LogViewModel> Logs => LogsCollection;

		public string Id => _tour.Id.ToString();
		public string Name => _tour.Name;
		public string Description => _tour.Description;
		public string From => _tour.From;
		public string To => _tour.To;
		public string TransportType => _tour.TransportType;
		public string Distance => $"{_tour.Distance.ToString()} km";
		public string Time => _tour.Time;
		public string Popularity { get; set; }
		public string ChildFriendliness { get; set; }
		public string ImageLink { get; set; }

		public TourViewModel(CombinedTour combinedTour) {
			_tour = new Tour(combinedTour.Id, combinedTour.Name, combinedTour.Description, combinedTour.From, combinedTour.To, combinedTour.TransportType, combinedTour.Distance, combinedTour.Time);
			LogsCollection = new ObservableCollection<LogViewModel>();
			int totalDifficulty = 0;
			foreach(Log entry in combinedTour.Logs) {
				LogsCollection.Add(new LogViewModel(entry));
				totalDifficulty += entry.Difficulty;
			}
			// calculate popularity
			Popularity = combinedTour.Logs.Count.ToString();
			// calculate child friendliness
			ChildFriendliness = "0";
			if(totalDifficulty > 0) {
				ChildFriendliness = (totalDifficulty / combinedTour.Logs.Count).ToString();
			}
			LogsCollection.CollectionChanged += LogsCollection_CollectionChanged;
			// get image url
			Dictionary<string, string> config =
				JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("../../../../configClient.json"));
			ImageLink = $"{config["ImageUrl"]}{combinedTour.Id}.jpeg";
		}

		// reevalute value if collection changes
		private void LogsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
			int totalDifficulty = 0;
			foreach(LogViewModel entry in LogsCollection) {
				totalDifficulty += Int32.Parse(entry.Difficulty);
			}
			// calculate popularity
			Popularity = LogsCollection.Count.ToString();
			// calculate child friendliness
			ChildFriendliness = "0";
			if(totalDifficulty > 0) {
				ChildFriendliness = (totalDifficulty / LogsCollection.Count).ToString();
			}
		}
	}
}
