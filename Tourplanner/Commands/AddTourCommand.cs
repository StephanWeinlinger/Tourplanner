using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Client.ViewModels;
using Tourplanner.Client.Views;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.Commands {
	public class AddTourCommand : CommandBase {
		private ModifyTourViewModel _modifyTourViewModel;

		public AddTourCommand(ModifyTourViewModel modifyTourViewModel) {
			_modifyTourViewModel = modifyTourViewModel;
		}

		public override void Execute(object parameter) {
			// add tour
			TourController tourController = new TourController();
			// insert tour in database
			var (newTour, response) = Task.Run<(CombinedTour, CustomResponse)>(async () => await tourController.InsertTour(new Tour(_modifyTourViewModel.Title, _modifyTourViewModel.Description, _modifyTourViewModel.From, _modifyTourViewModel.To, _modifyTourViewModel.TransportType))).Result;
			if(CheckError(response)) {
				return;
			}
			// add tour to collection
			_modifyTourViewModel.MainViewModel.ToursCollection.Add(new TourViewModel(newTour));
			_modifyTourViewModel.ModifyTourView.Close();
		}
	}
}
