using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourplanner.Client.BL;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Client.ViewModels;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.Commands {
	public class CreateTourReportCommand : CommandBase {
		private MainViewModel _mainViewModel;

		public CreateTourReportCommand(MainViewModel mainViewModel) {
			_mainViewModel = mainViewModel;
		}

		public override void Execute(object parameter) {
			if(_mainViewModel.CurrentTour == null) {
				MessageBox.Show("No tour was selected!", "Tourplanner", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			// select folder
			FileExplorer fileExplorer = BlFactory.GetFileExplorer();
			string path = fileExplorer.SelectFolder();
			if(path == null) {
				return;
			}
			// get tour from database and create report
			ReportController reportController = new ReportController();
			CustomResponse response = Task.Run<CustomResponse>(async () => await reportController.GenerateTourReport(Int32.Parse(_mainViewModel.CurrentTour.Id), path)).Result;
			if(!response.Success) {
				MessageBox.Show(response.Errors.ContainsKey("Custom") ? response.Errors["Custom"] : "Unknown Error", "Tourplanner", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
