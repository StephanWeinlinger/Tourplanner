using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourplanner.Client.BL;
using Tourplanner.Client.ViewModels;
using Tourplanner.Client.Views;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.Commands {
	public class ShowAddLogCommand : CommandBase {
		private MainViewModel _mainViewModel;

		public ShowAddLogCommand(MainViewModel mainViewModel) {
			_mainViewModel = mainViewModel;
		}

		public override void Execute(object parameter) {
			if(_mainViewModel.CurrentTour == null) {
				string message = "No log was selected!";
				MessageBox.Show(message, "Tourplanner", MessageBoxButton.OK, MessageBoxImage.Error);
				BlFactory.GetLogger().Warn(message);
				return;
			}
			ModifyLogView modifyLogView = new ModifyLogView();
			ModifyLogViewModel modifyLogViewModel = new ModifyLogViewModel(_mainViewModel, modifyLogView);
			modifyLogView.Show();
		}
	}
}
