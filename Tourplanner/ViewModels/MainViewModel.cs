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
		private readonly TourListViewModel _tourListViewModel;
		//private readonly TourDetailsViewModel _tourDetailsViewModel;
		private readonly TourLogsViewModel _tourLogsViewModel;
		private readonly NavBarViewModel _navBarViewModel;
		private readonly AddLogViewModel _addLogViewModel;
		private readonly AddTourViewmodel _addTourViewmodel;
		private readonly UpdateLogViewModel _updateLogViewModel;

		private TourController ToursBL;

		public ObservableCollection<CombinedTour> Tours { get; set; }
		

		public MainViewModel(SearchBarViewModel _searchBarViewModel, TourListViewModel _tourListViewModel, TourLogsViewModel _tourLogsViewModel,
			NavBarViewModel _navBarViewModel, AddLogViewModel _addLogViewModel, AddTourViewmodel _addTourViewmodel, UpdateLogViewModel _updateLogViewModel) {


			// fetch tours from database 
			List<CombinedTour> tours = new List<CombinedTour>();
			tours = Task.Run<List<CombinedTour>>(async () => await ToursBL.GetCombinedTours()).Result;
			Tours = new ObservableCollection<CombinedTour>(tours);
			


			//initialize of Sub-ViewModels
			this._searchBarViewModel = _searchBarViewModel;
			this._navBarViewModel = _navBarViewModel;
			this._tourListViewModel = _tourListViewModel;
			//this._tourDetailsViewModel = _tourDetailsViewModel;
			this._tourLogsViewModel = _tourLogsViewModel;
			
		
			// observeble collection übergeben google 

		}

	}
}
