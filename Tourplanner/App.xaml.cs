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
			var addLogViewModel = new AddLogViewModel();
			var updateLogViewModel = new UpdateLogViewModel();
			var addTourViewmodel = new AddTourViewmodel();
			var updateTourViewModel = new UpdateTourViewModel();

			//MVVM:
			var wnd = new MainWindow {
				DataContext = new MainViewModel(searchBarViewModel, addLogViewModel, addTourViewmodel, updateLogViewModel, updateTourViewModel),
				AddTourUI = { DataContext = addTourViewmodel },
				//UpdateTourUI = { DataContext = updateTourViewModel }
			};

			wnd.Show();
				
			
		}

	}
}
