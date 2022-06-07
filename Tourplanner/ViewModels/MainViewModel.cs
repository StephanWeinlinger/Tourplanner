using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Client.Commands;
using Tourplanner.Client.Views;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.ViewModels {
	public class MainViewModel : ViewModelBase {
		public ObservableCollection<TourViewModel> ToursCollection;
		public IEnumerable<TourViewModel> Tours => ToursCollection;

		public ICommand ImportCommand { get; }
		public ICommand ExportCommand { get; }
		public ICommand ShowAddTourCommand { get; }
		public ICommand ShowUpdateTourCommand { get; }
		public ICommand DeleteTourCommand { get; }
		public ICommand ShowAddLogCommand { get; }
		public ICommand ShowUpdateLogCommand { get; }
		public ICommand DeleteLogCommand { get; }
		public ICommand CreateTourReportCommand { get; }
		public ICommand CreateSummarizedReportCommand { get; }
		public ICommand AboutCommand { get; }

		public SearchBarViewModel SearchBarViewModel;

		public MainViewModel(SearchBarViewModel searchBarViewModel, List<CombinedTour> combinedTours) {
			// initialize subviewmodels
			SearchBarViewModel = searchBarViewModel;

			ToursCollection = new ObservableCollection<TourViewModel>();
			
			foreach(CombinedTour combinedTour in combinedTours) {
				ToursCollection.Add(new TourViewModel(combinedTour));
			}

			// initialize commands
			ImportCommand = new ImportCommand(this);
			ExportCommand = new ExportCommand();
			ShowAddTourCommand = new ShowAddTourCommand(this);
			ShowUpdateTourCommand = new ShowUpdateTourCommand(this);
			DeleteTourCommand = new DeleteTourCommand(this);
			ShowAddLogCommand = new ShowAddLogCommand(this);
			ShowUpdateLogCommand = new ShowUpdateLogCommand(this);
			DeleteLogCommand = new DeleteLogCommand(this);
			CreateTourReportCommand = new CreateTourReportCommand(this);
			CreateSummarizedReportCommand = new CreateSummarizedReportCommand(this);
			AboutCommand = new AboutCommand();


			// intialize commands and event listeners for subviews
			ICommand SearchCommand = new SearchCommand(this);
			ICommand ClearCommand = new ClearCommand(this);

			SearchBarViewModel.OnSearchClicked += (_, filter) => {
				SearchCommand.Execute(null); // doesn't need filter, since SearchBarViewModel is public
			};
			SearchBarViewModel.OnClearClicked += (_, filter) => {
				ClearCommand.Execute(null);
			};
		}

		private TourViewModel _currentTour;
		public TourViewModel CurrentTour {
			get {
				return _currentTour;
			}
			set {
				_currentTour = value;
				OnPropertyChanged(nameof(CurrentTour));
			}
		}

		private LogViewModel _currentLog;
		public LogViewModel CurrentLog {
			get {
				return _currentLog;
			}
			set {
				_currentLog = value;
				OnPropertyChanged(nameof(CurrentLog));
			}
		}
	}
}