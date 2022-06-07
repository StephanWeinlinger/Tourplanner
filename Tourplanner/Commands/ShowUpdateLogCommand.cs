﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourplanner.Client.ViewModels;
using Tourplanner.Client.Views;

namespace Tourplanner.Client.Commands {
	public class ShowUpdateLogCommand : CommandBase {
		private MainViewModel _mainViewModel;

		public ShowUpdateLogCommand(MainViewModel mainViewModel) {
			_mainViewModel = mainViewModel;
		}

		public override void Execute(object parameter) {
			if(_mainViewModel.CurrentLog == null) {
				MessageBox.Show("No log was selected!", "Tourplanner", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			ModifyLogView modifyLogView = new ModifyLogView();
			ModifyLogViewModel modifyLogViewModel = new ModifyLogViewModel(_mainViewModel, _mainViewModel.CurrentLog, modifyLogView);
			modifyLogView.Show();
		}
	}
}
