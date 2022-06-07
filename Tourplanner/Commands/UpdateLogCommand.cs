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
	public class UpdateLogCommand : CommandBase {
		private ModifyLogViewModel _modifyLogViewModel;

		public UpdateLogCommand(ModifyLogViewModel modifyLogViewModel) {
			_modifyLogViewModel = modifyLogViewModel;
		}

		public override void Execute(object parameter) {
			// update log
			LogController logController = new LogController();
			// update log in database
			var (updatedLog, response) = Task.Run<(Log, CustomResponse)>(async () => await logController.UpdateLog(_modifyLogViewModel.Id, new Log(_modifyLogViewModel.TourId, _modifyLogViewModel.Date, _modifyLogViewModel.Comment, _modifyLogViewModel.Difficulty, _modifyLogViewModel.Time, _modifyLogViewModel.Rating))).Result;
			if(CheckError(response)) {
				return;
			}
			// update log in collection
			_modifyLogViewModel.MainViewModel.CurrentTour.LogsCollection[_modifyLogViewModel.MainViewModel.CurrentTour.LogsCollection.IndexOf(_modifyLogViewModel.MainViewModel.CurrentTour.LogsCollection.First(entry =>
				Int32.Parse(entry.Id) == _modifyLogViewModel.Id))] = new LogViewModel(updatedLog);
			_modifyLogViewModel.ModifyLogView.Close();
		}
	}
}
