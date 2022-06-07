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
	public class AddLogCommand : CommandBase {
		private ModifyLogViewModel _modifyLogViewModel;

		public AddLogCommand(ModifyLogViewModel modifyLogViewModel) {
			_modifyLogViewModel = modifyLogViewModel;
		}

		public override void Execute(object parameter) {
			// add log
			LogController logController = new LogController();
			// insert log in database
			var (newLog, response) = Task.Run<(Log, CustomResponse)>(async () => await logController.InsertLog(new Log(_modifyLogViewModel.TourId, _modifyLogViewModel.Date, _modifyLogViewModel.Comment, _modifyLogViewModel.Difficulty, _modifyLogViewModel.Time, _modifyLogViewModel.Rating))).Result;
			if(CheckError(response)) {
				return;
			}
			// add log to collection
			_modifyLogViewModel.MainViewModel.CurrentTour.LogsCollection.Add(new LogViewModel(newLog));
			_modifyLogViewModel.ModifyLogView.Close();
		}
	}
}
