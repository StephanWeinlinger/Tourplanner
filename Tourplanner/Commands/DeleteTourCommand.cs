using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			TourController tourController = new TourController();
			// remove tour in database
			Task.Run<bool>(async () => await tourController.DeleteTour(Int32.Parse(_mainViewModel.CurrentTour.Id)));
			// remove tour from collection
			_mainViewModel.ToursCollection.Remove(_mainViewModel.CurrentTour);
		}
	}
}
