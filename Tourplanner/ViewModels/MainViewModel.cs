using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.ViewModels {
	class MainViewModel : BaseViewModel {

		
		private readonly SearchBarViewModel _searchBarViewModel;
		private readonly TourListViewModel _tourListViewModel;
		private readonly TourDetailsViewModel _tourDetailsViewModel;
		private readonly TourLogsViewModel _tourLogsViewModel;
		private readonly NavBarViewModel _navBarViewModel;

		public ObservableCollection<List<CombinedTour>> Tours { get; set; }

		public MainViewModel(SearchBarViewModel _searchBarViewModel, TourListViewModel _tourListViewModel, TourDetailsViewModel _tourDetailsViewModel, TourLogsViewModel _tourLogsViewModel, NavBarViewModel _navBarViewModel) {


			// fetch tours from database 


			//initialize of Sub-ViewModels
			this._searchBarViewModel = _searchBarViewModel;
			this._navBarViewModel = _navBarViewModel;
			this._tourListViewModel = _tourListViewModel;
			this._tourDetailsViewModel = _tourDetailsViewModel;
			this._tourLogsViewModel = _tourLogsViewModel;
			
			// observeble collection übergeben google 

		}

	}
}
