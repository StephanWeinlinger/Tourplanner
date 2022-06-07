using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tourplanner.Client;
using Tourplanner.Client.BL;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Client.ViewModels;
using Tourplanner.Client.Views;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
	    protected override void OnStartup(StartupEventArgs e) {
		    base.OnStartup(e);

			TourController tourController = new TourController();
			var (combinedTours, response) = Task.Run<(List<CombinedTour>, CustomResponse)>(async () => await tourController.GetCombinedTours()).Result;
		    if(!response.Success) {
			    MessageBoxResult result = MessageBox.Show("Connection to database could not be established! Do you want to continue?", "Tourplanner", MessageBoxButton.YesNo,
				    MessageBoxImage.Error);
			    if(result == MessageBoxResult.No) {
					BlFactory.GetLogger().Fatal("Shutdown by user");
					Shutdown();
					return;
			    }
		    }
			SearchBarViewModel searchBarViewModel = new SearchBarViewModel();
		    MainViewModel mainViewModel = new MainViewModel(searchBarViewModel, combinedTours);

			MainWindow = new MainWindow() {
			    DataContext = mainViewModel,
				SearchBarView = {DataContext = searchBarViewModel}
		    };

		    MainWindow.Show();
	    }
    }
}
