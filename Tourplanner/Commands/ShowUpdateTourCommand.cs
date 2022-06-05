using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourplanner.Client.ViewModels;
using Tourplanner.Client.Views;

namespace Tourplanner.Client.Commands {
	public class ShowUpdateTourCommand : CommandBase {
		private MainViewModel _mainViewModel;

		public ShowUpdateTourCommand(MainViewModel mainViewModel) {
			_mainViewModel = mainViewModel;
		}

		public override void Execute(object parameter) {
			ModifyTourView modifyTourView = new ModifyTourView();
			ModifyTourViewModel modifyTourViewModel = new ModifyTourViewModel(_mainViewModel, _mainViewModel.CurrentTour, modifyTourView);
			modifyTourView.Show();
		}
	}
}
