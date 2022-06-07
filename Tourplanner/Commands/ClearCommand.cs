using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Client.ViewModels;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.Commands {
	public class ClearCommand : CommandBase {
		private MainViewModel _mainViewModel;

		public ClearCommand(MainViewModel mainViewModel) {
			_mainViewModel = mainViewModel;
		}

		public override void Execute(object parameter) {
			// clear input field
			_mainViewModel.SearchBarViewModel.Filter = "";
			// get tours from database
			TourController tourController = new TourController();
			var (combinedTours, response) = Task.Run<(List<CombinedTour>, CustomResponse)>(async () => await tourController.GetCombinedTours()).Result;
			if(!response.Success) {
				MessageBox.Show(response.Errors.ContainsKey("Custom") ? response.Errors["Custom"] : "Unknown Error", "Tourplanner", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			// clear and add tours to collection
			_mainViewModel.ToursCollection.Clear();
			foreach(CombinedTour combinedTour in combinedTours) {
				_mainViewModel.ToursCollection.Add(new TourViewModel(combinedTour));
			}
		}
	}
}
