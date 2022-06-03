using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourplanner.Client.BL;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.ViewModels {
	internal class MainViewModel : BaseViewModel {


		protected SearchBarViewModel searchBarViewModel;
		protected AddLogViewModel addLogViewModel;
		protected AddTourViewmodel addTourViewmodel;
		protected UpdateLogViewModel updateLogViewModel;
		protected UpdateTourViewModel updateTourViewModel;

		private TourController ToursBL;

		public event EventHandler<int> OnSelected;
		public event PropertyChangedEventHandler? PropertyChanged;
		private int selectedIndex;
		private int selectedTourIndex;


		public ObservableCollection<CombinedTour> Tours { get; set; }
		public CombinedTour selectedTour;

		public ICommand AboutClicked { get; } 

		public ICommand AddTour { get; }
		public ICommand UpdateTour { get; }
		public ICommand DeleteTour { get; }

		public ICommand AddTourLog { get; }
		public ICommand UpdateTourLog { get; }
		public ICommand DeleteTourLog { get; }

		public ICommand CreateRaportSelected { get; }
		public ICommand CreateRaportSummarized { get; } 
		public ICommand Import { get; } 
		public ICommand Export { get; }

		// Get the Selected Tour
		public int SelectedIndex {
			get => selectedIndex;
			set {
				selectedIndex = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedIndex)));
				OnSelected?.Invoke(this, selectedIndex);
			}
		}

		public string Title {
			get => selectedTour == null ? "" : selectedTour.Title;
			set {
				if(selectedTour == null)
					return;
				selectedTour.Title = value;
				OnPropertyChanged(nameof(Title));
			}
		}
		public string TourDescription {
			get => selectedTour == null ? "" : selectedTour.TourDescription;
			set {
				if(selectedTour == null)
					return;
				selectedTour.Description = value;
				OnPropertyChanged(nameof(TourDescription));
			}
		}

		public string TourFrom {
			get => selectedTour == null ? "" : selectedTour.TourFrom;
			set {
				if(selectedTour == null)
					return;
				selectedTour.From = value;
				OnPropertyChanged(nameof(TourFrom));
				
			}
		}

		public string TourTo {
			get => selectedTour == null ? "" : selectedTour.TourTo;
			set {
				if(selectedTour == null)
					return;
				selectedTour.To = value;
				OnPropertyChanged(nameof(TourTo));
				
			}
		}
		public string TourTransportationType {
			get => selectedTour == null ? "" : selectedTour.TourTransportType;
			set {
				if(selectedTour == null)
					return;
				selectedTour.TourTransportType = value;
				OnPropertyChanged(nameof(TourTransportationType));
			}
		}

		public string TourTime {
			get => selectedTour == null ? "" : $"{selectedTour.TourTime:hh\\:mm}";
			set {
				if(selectedTour == null)
					return;
				OnPropertyChanged(nameof(TourTime));
			}
}
		/*public double TourDistance {
			get => selectedTour == null ? "" : $"{selectedTour.TourDistance} km";
		}*/





		public MainViewModel(SearchBarViewModel _searchBarViewModel, AddLogViewModel _addLogViewModel, AddTourViewmodel _addTourViewmodel, UpdateLogViewModel _updateLogViewModel, UpdateTourViewModel _updateTourViewModel) {


			List<CombinedTour> tours = new List<CombinedTour>();
			ToursBL = new TourController();
			tours = Task.Run<List<CombinedTour>>(async () => await ToursBL.GetCombinedTours()).Result;
			Tours = new ObservableCollection<CombinedTour>(tours);

			
			//initialize of Sub-ViewModels
			searchBarViewModel = _searchBarViewModel;
			addLogViewModel = _addLogViewModel;
			addTourViewmodel = _addTourViewmodel;
			updateLogViewModel = _updateLogViewModel;
			updateTourViewModel = _updateTourViewModel;


			OnSelected += SelectTour;

			DeleteTourLog = new RelayCommand((sender) => {
				LogController logcontroller = new LogController();

				Task.Run<bool>(async () => await logcontroller.DeleteLog(31));
				MessageBox.Show("Log was sucsessfully deleted", "Delete your Log");
			});
			
			DeleteTour = new RelayCommand((sender) => {
				TourController tourcontroller = new TourController();
				Task.Run<bool>(async () => await tourcontroller.DeleteTour(SelectedIndex));
				MessageBox.Show("Tour was sucsessfully deleted", "Delete your Log");
			});

			CreateRaportSelected = new RelayCommand((sender) => {
				FileExplorer fileExplorer = BlFactory.GetFileExplorer();
				string path = fileExplorer.SelectFolder();
				ReportController reportController = new ReportController();
				bool success = Task.Run<bool>(async () => await reportController.GenerateTourReport(50,path)).Result;
				if(success) {
					MessageBox.Show("Raport was successfully created", "Report");
				} else {
					MessageBox.Show("Failure something went wrong!", "Report");
				}
			});

			CreateRaportSummarized = new RelayCommand((sender) => {
				FileExplorer fileExplorer = BlFactory.GetFileExplorer();
				string path = fileExplorer.SelectFolder();
				ReportController reportController = new ReportController();
				bool success = Task.Run<bool>(async () => await reportController.GenerateSummarizedReport(path)).Result;
				if(success) {
					MessageBox.Show("Raport was successfully created", "Report");
				} else {
					MessageBox.Show("Failure something went wrong!", "Report");
				}
			});

			Import = new RelayCommand((sender) => {
				FileExplorer fileExplorer = BlFactory.GetFileExplorer();
				string path = fileExplorer.SelectFile();
				ImportController importController = new ImportController();
				Tours = new ObservableCollection<CombinedTour>(Task.Run<List<CombinedTour>>(async () => await importController.ImportTours(path)).Result);
			});

			Export = new RelayCommand((sender) => {
				FileExplorer fileExplorer = BlFactory.GetFileExplorer();
				string path = fileExplorer.SelectFolder();
				ImportController importController = new ImportController();
				bool success = Task.Run<bool>(async () => await importController.ExportTours(path)).Result;
				if(success) {
					MessageBox.Show("Export was successfull ", "Export");
				} else {
					MessageBox.Show("Failure something went wrong!", "Export");
				}
			});

			AboutClicked = new RelayCommand((sender) => {
				MessageBox.Show("This project was realised by Patrick Friedel and Stephan Weinlinger", "Tour Planner" );
			});
		}

		// index von A
		private void SelectTour(object? sender, int index) {
			if(index != -1)
				selectedTour = Tours[index];
			else
				selectedTour = null;

			UpdateSelectedTourIndex(sender, index);
			NotifyDetailsChanged();
			//RefreshLogs();
		}

		public void UpdateSelectedTourIndex(object? sender, int index) {
			selectedTourIndex = index;
		}

		private void NotifyDetailsChanged() {
			OnPropertyChanged(nameof(Title));
			OnPropertyChanged(nameof(TourDescription));
			OnPropertyChanged(nameof(TourFrom));
			OnPropertyChanged(nameof(TourTo));
			OnPropertyChanged(nameof(TourTransportationType));
			OnPropertyChanged(nameof(TourTime));
			/*
			OnPropertyChanged(nameof(TourDestination));
			OnPropertyChanged(nameof(TourType));
			
			OnPropertyChanged(nameof(TourDistance));
			OnPropertyChanged(nameof(TourDuration));
			OnPropertyChanged(nameof(TourPopularity));
			OnPropertyChanged(nameof(TourChildFriendliness));
			OnPropertyChanged(nameof(DetailsVisibility));

			FileInfo mapFile = null;
			if(selectedTour != null)
				mapFile = businessLayer.Maps.GetRouteImage(selectedTour.content);
			ImagePath = mapFile == null ? null : new Uri(mapFile.FullName);*/
		}
	}
}
