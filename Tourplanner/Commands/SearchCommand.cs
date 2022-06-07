using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourplanner.Client.BL;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Client.ViewModels;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.Commands {
	public class SearchCommand : CommandBase {
		private MainViewModel _mainViewModel;

		public SearchCommand(MainViewModel mainViewModel) {
			_mainViewModel = mainViewModel;
		}

		public override void Execute(object parameter) {
			// get tours from database
			TourController tourController = new TourController();
			var (combinedTours, response) = Task.Run<(List<CombinedTour>, CustomResponse)>(async () => await tourController.GetCombinedTours(_mainViewModel.SearchBarViewModel.Filter)).Result;
			if(CheckError(response)) {
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
