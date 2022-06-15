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
	public class UpdateTourCommand : CommandBase {
		private ModifyTourViewModel _modifyTourViewModel;

		public UpdateTourCommand(ModifyTourViewModel modifyTourViewModel) {
			_modifyTourViewModel = modifyTourViewModel;
		}

		public override void Execute(object parameter) {
			// update tour
			TourController tourController = new TourController();
			// update tour in database
			var (updatedTour, response) = Task.Run<(CombinedTour, CustomResponse)>(async () => await tourController.UpdateTour(_modifyTourViewModel.Id, new Tour(_modifyTourViewModel.Title, _modifyTourViewModel.Description, _modifyTourViewModel.From, _modifyTourViewModel.To, _modifyTourViewModel.TransportType))).Result;
			if(CheckError(response)) {
				return;
			}
			// get logs from selected tour
			List<Log> logs = new List<Log>();
			foreach(LogViewModel entry in _modifyTourViewModel.MainViewModel.CurrentTour.Logs) {
				logs.Add(new Log(Int32.Parse(entry.Id), Int32.Parse(entry.TourId), DateTime.Parse(entry.Date), entry.Comment, Int32.Parse(entry.Difficulty), entry.Time, Int32.Parse(entry.Rating)));
			}
			updatedTour.Logs = logs;
			// update tour in collection
			_modifyTourViewModel.MainViewModel.ToursCollection[_modifyTourViewModel.MainViewModel.ToursCollection.IndexOf(_modifyTourViewModel.MainViewModel.ToursCollection.First(entry =>
				Int32.Parse(entry.Id) == _modifyTourViewModel.Id))] = new TourViewModel(updatedTour);
			_modifyTourViewModel.ModifyTourView.Close();
		}
	}
}
