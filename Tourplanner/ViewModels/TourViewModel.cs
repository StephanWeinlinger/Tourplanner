using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		public string Distance => _tour.Distance.ToString();
		public string Time => _tour.Time;
		public string Popularity { get; set; }
		public string ChildFriendliness { get; set; }
		public string ImageLink { get; set; }

		public TourViewModel(CombinedTour combinedTour) {
			_tour = new Tour(combinedTour.Id, combinedTour.Name, combinedTour.Description, combinedTour.From, combinedTour.To, combinedTour.TransportType, combinedTour.Distance, combinedTour.Time);
			LogsCollection = new ObservableCollection<LogViewModel>();
			foreach(Log entry in combinedTour.Logs) {
				LogsCollection.Add(new LogViewModel(entry));
			}
			Popularity = "1";
			ChildFriendliness = "1";
			ImageLink = $"https://localhost:44314/Static/{combinedTour.Id}.jpeg";
		}
	}
}
