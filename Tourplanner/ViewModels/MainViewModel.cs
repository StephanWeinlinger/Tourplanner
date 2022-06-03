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

		public ObservableCollection<CombinedTour> Tours { get; set; }

		public ICommand AboutClicked { get; } //

		public ICommand AddTour { get; }
		public ICommand UpdateTour { get; }
		public ICommand DeleteTour { get; }

		public ICommand AddTourLog { get; }
		public ICommand UpdateTourLog { get; }
		public ICommand DeleteTourLog { get; }

		public ICommand CreateRaportSelected { get; }
		public ICommand CreateRaportSummarized { get; } //
		public ICommand Import { get; } //
		public ICommand Export { get; } //
		



		public MainViewModel(SearchBarViewModel _searchBarViewModel, AddLogViewModel _addLogViewModel, AddTourViewmodel _addTourViewmodel, UpdateLogViewModel _updateLogViewModel, UpdateTourViewModel _updateTourViewModel) {


			/*List<CombinedTour> tours = new List<CombinedTour>();
			ToursBL = new TourController();
			tours = Task.Run<List<CombinedTour>>(async () => await ToursBL.GetCombinedTours()).Result;
			Tours = new ObservableCollection<CombinedTour>(tours);*/


			//initialize of Sub-ViewModels
			searchBarViewModel = _searchBarViewModel;
			addLogViewModel = _addLogViewModel;
			addTourViewmodel = _addTourViewmodel;
			updateLogViewModel = _updateLogViewModel;
			updateTourViewModel = _updateTourViewModel;

			DeleteTourLog = new RelayCommand((sender) => {
				LogController logcontroller = new LogController();

				Task.Run<bool>(async () => await logcontroller.DeleteLog(31));
				MessageBox.Show("Log was sucsessfully deleted", "Delete your Log");
			});
			
			DeleteTour = new RelayCommand((sender) => {
				TourController tourcontroller = new TourController();
				Task.Run<bool>(async () => await tourcontroller.DeleteTour(53));
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

	}
}
