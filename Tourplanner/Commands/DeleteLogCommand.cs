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
	public class DeleteLogCommand : CommandBase {
		private MainViewModel _mainViewModel;

		public DeleteLogCommand(MainViewModel mainViewModel) {
			_mainViewModel = mainViewModel;
		}

		public override void Execute(object parameter) {
			LogController logController = new LogController();
			// remove log in database
			Task.Run<bool>(async () => await logController.DeleteLog(Int32.Parse(_mainViewModel.CurrentLog.Id)));
			// remove log from collection
			_mainViewModel.CurrentTour.LogsCollection.Remove(_mainViewModel.CurrentLog);
		}
	}
}
