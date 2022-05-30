using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tourplanner.Client;
using Tourplanner.Client.ViewModels;

namespace Tourplanner {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

		private void App_OnStartup(object sender, StartupEventArgs e) {
			var searchBarViewModel = new SearchBarViewModel();
			var tourListViewModel = new TourListViewModel();
			var tourLogsViewModel = new TourLogsViewModel();
			var navBarViewModel = new NavBarViewModel();
			var addLogViewModel = new AddLogViewModel();
			var addTourViewmodel = new AddTourViewmodel();
			var updateLogViewModel = new UpdateLogViewModel();

			//MVVM:
			var wnd = new MainWindow {
				DataContext = new MainViewModel(searchBarViewModel, tourListViewModel, tourLogsViewModel, navBarViewModel, addLogViewModel, addTourViewmodel, updateLogViewModel)
			};

			wnd.Show();
				
			
		}

	}
}
