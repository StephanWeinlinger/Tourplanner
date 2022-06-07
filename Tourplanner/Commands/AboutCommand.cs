using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourplanner.Client.BL;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Client.ViewModels;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.Commands {
	public class AboutCommand : CommandBase {
		public override void Execute(object parameter) {
			MessageBox.Show("This software was developed by Stephan Weinlinger and Patrick Friedel", "Tourplanner", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}
}
