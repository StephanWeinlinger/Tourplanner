using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourplanner.Client.BL;
using Tourplanner.Client.ViewModels;
using Tourplanner.Client.Views;

namespace Tourplanner.Client.Commands {
	public class ShowUpdateTourCommand : CommandBase {
		private MainViewModel _mainViewModel;

		public ShowUpdateTourCommand(MainViewModel mainViewModel) {
			_mainViewModel = mainViewModel;
		}

		public override void Execute(object parameter) {
			if(_mainViewModel.CurrentTour == null) {
				string message = "No tour was selected!";
				MessageBox.Show(message, "Tourplanner", MessageBoxButton.OK, MessageBoxImage.Error);
				BlFactory.GetLogger().Warn(message);
				return;
			}
			ModifyTourView modifyTourView = new ModifyTourView();
			ModifyTourViewModel modifyTourViewModel = new ModifyTourViewModel(_mainViewModel, _mainViewModel.CurrentTour, modifyTourView);
			modifyTourView.Show();
		}
	}
}
