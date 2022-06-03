using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tourplanner.Client.BL;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Client.ViewModels;
using Tourplanner.Client.Views;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {

			var searchBarViewModel = new SearchBarViewModel();
			var addLogViewModel = new AddLogViewModel();
			var addTourViewmodel = new AddTourViewmodel();
			var updateLogViewModel = new UpdateLogViewModel();
			var updateTourViewModel = new UpdateTourViewModel();

			InitializeComponent();
			DataContext = new MainViewModel(searchBarViewModel, addLogViewModel, addTourViewmodel, updateLogViewModel, updateTourViewModel);
			
		}
		

		private void OpenAddTour(object sender, RoutedEventArgs e) {
			AddTour NewTour = new AddTour();
			NewTour.Show();
		}
		private void UpdateAddTour(object sender, RoutedEventArgs e) {
			UpdateTour UpdateTour = new UpdateTour();
			UpdateTour.Show();
		}
		private void AddLog(object sender, RoutedEventArgs e) {
			AddLog AddLog = new AddLog();
			AddLog.Show();
		}

		private void UpdateLog(object sender, RoutedEventArgs e) {
			UpdateLog UpdateLog = new UpdateLog();
			UpdateLog.Show();
		}
	}
}
