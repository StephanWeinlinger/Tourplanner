using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourplanner.Client.BL;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Client.ViewModels;
using Tourplanner.Client.Views;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.Commands {
	public class DeleteTourCommand : CommandBase {
		private MainViewModel _mainViewModel;

		public DeleteTourCommand(MainViewModel mainViewModel) {
			_mainViewModel = mainViewModel;
		}

		public override void Execute(object parameter) {
			if(_mainViewModel.CurrentTour == null) {
				string message = "No tour was selected!";
				MessageBox.Show(message, "Tourplanner", MessageBoxButton.OK, MessageBoxImage.Error);
				BlFactory.GetLogger().Warn(message);
				return;
			}
			TourController tourController = new TourController();
			// remove tour in database
			CustomResponse response = Task.Run<CustomResponse>(async () => await tourController.DeleteTour(Int32.Parse(_mainViewModel.CurrentTour.Id))).Result;
			if(CheckError(response)) {
				return;
			}
			// remove tour from collection
			_mainViewModel.ToursCollection.Remove(_mainViewModel.CurrentTour);
		}
	}
}
