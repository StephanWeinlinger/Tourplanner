using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourplanner.Client.ViewModels;
using Tourplanner.Client.Views;

namespace Tourplanner.Client.Commands {
	public class ShowAddLogCommand : CommandBase {
		private MainViewModel _mainViewModel;

		public ShowAddLogCommand(MainViewModel mainViewModel) {
			_mainViewModel = mainViewModel;
		}

		public override void Execute(object parameter) {
			ModifyLogView modifyLogView = new ModifyLogView();
			ModifyLogViewModel modifyLogViewModel = new ModifyLogViewModel(_mainViewModel, modifyLogView);
			modifyLogView.Show();
		}
	}
}
