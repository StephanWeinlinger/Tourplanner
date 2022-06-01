using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.ViewModels {
	class MainViewModel : BaseViewModel {

		
		private readonly SearchBarViewModel _searchBarViewModel;
		private readonly TourLogsViewModel _tourLogsViewModel;
		private readonly AddLogViewModel _addLogViewModel;
		private readonly AddTourViewmodel _addTourViewmodel;
		private readonly UpdateLogViewModel _updateLogViewModel;

		private TourController ToursBL;

		public ObservableCollection<CombinedTour> Tours { get; set; }


		public MainViewModel(SearchBarViewModel _searchBarViewModel, TourLogsViewModel _tourLogsViewModel,
			 AddLogViewModel _addLogViewModel, AddTourViewmodel _addTourViewmodel, UpdateLogViewModel _updateLogViewModel) {


			// fetch tours from database 
			List<CombinedTour> tours = new List<CombinedTour>();
			tours = Task.Run<List<CombinedTour>>(async () => await ToursBL.GetCombinedTours()).Result;
			Tours = new ObservableCollection<CombinedTour>(tours);



			//initialize of Sub-ViewModels
			this._searchBarViewModel = _searchBarViewModel;

			this._tourLogsViewModel = _tourLogsViewModel;

		}
			// observeble collection übergeben google 

		private string _currentFrom;
		private string _currentTo;
		private string _currentTransportType;
		private string _currentDistance;
		private string _currentEstimatedTime;
		private string _currentPopularity;
		private string _currentChildFriendliness;


		// Current Tour properties ##################################################################################

		/*public string CurrentFrom {
			get { return _currentFrom; }
			set {
				_currentFrom = value;
				OnPropertyChanged();
			}
		}
		public string CurrentTo {
			get { return _currentTo; }
			set {
				_currentTo = value;
				OnPropertyChanged();
			}
		}
		public string CurrentTransportType {
			get { return _currentTransportType; }
			set {
				_currentTransportType = value;
				OnPropertyChanged();
			}
		}
		public string CurrentDistance {
			get { return _currentDistance; }
			set {
				_currentDistance = value;
				OnPropertyChanged();
			}
		}
		public string CurrentEstimatedTime {
			get { return _currentEstimatedTime; }
			set {
				_currentEstimatedTime = value;
				OnPropertyChanged();
			}
		}
		public string CurrentPopularity {
			get { return _currentPopularity; }
			set {
				_currentPopularity = value;
				OnPropertyChanged();
			}
		}
		public string CurrentChildFriendliness {
			get { return _currentChildFriendliness; }
			set {
				_currentChildFriendliness = value;
				OnPropertyChanged();
			}
		}

	} */ // Current Tour properties ##################################################################################

	}
}
