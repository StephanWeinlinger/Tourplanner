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
	public class DeleteLogCommand : CommandBase {
		private MainViewModel _mainViewModel;

		public DeleteLogCommand(MainViewModel mainViewModel) {
			_mainViewModel = mainViewModel;
		}

		public override void Execute(object parameter) {
			if(_mainViewModel.CurrentLog == null) {
				string message = "No log was selected!";
				MessageBox.Show(message, "Tourplanner", MessageBoxButton.OK, MessageBoxImage.Error);
				BlFactory.GetLogger().Warn(message);
				return;
			}
			LogController logController = new LogController();
			// remove log in database
			CustomResponse response = Task.Run<CustomResponse>(async () => await logController.DeleteLog(Int32.Parse(_mainViewModel.CurrentLog.Id))).Result;
			if(CheckError(response)) {
				return;
			}
			// remove log from collection
			_mainViewModel.CurrentTour.LogsCollection.Remove(_mainViewModel.CurrentLog);
		}
	}
}
