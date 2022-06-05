using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tourplanner.Client;
using Tourplanner.Client.ViewModels;
using Tourplanner.Client.Views;

namespace Tourplanner.Client {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
	    protected override void OnStartup(StartupEventArgs e) {
		    SearchBarViewModel searchBarViewModel = new SearchBarViewModel();
		    MainViewModel mainViewModel = new MainViewModel(searchBarViewModel);

			MainWindow = new MainWindow() {
			    DataContext = mainViewModel,
				SearchBarView = {DataContext = searchBarViewModel}
		    };

		    MainWindow.Show();
		    base.OnStartup(e);
	    }
    }
}
