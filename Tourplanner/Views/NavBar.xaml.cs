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
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.Views {
	/// <summary>
	/// Interaktionslogik für NavBar.xaml
	/// </summary>
	public partial class NavBar : UserControl {
		public NavBar() {
			InitializeComponent();
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

		private void CreateSummarizedReport(object sender, RoutedEventArgs e) {
			// select folder
			FileExplorer fileExplorer = BlFactory.GetFileExplorer();
			string path = fileExplorer.SelectFolder();
			ReportController reportController = new ReportController();
			Task.Run<bool>(async () => await reportController.GenerateSummarizedReport(path));
		}

		private void Import(object sender, RoutedEventArgs e) {
			// select file
			FileExplorer fileExplorer = BlFactory.GetFileExplorer();
			string path = fileExplorer.SelectFile();
			ImportController importController = new ImportController();
			Task.Run<List<CombinedTour>>(async () => await importController.ImportTours(path));
		}

		private void Export(object sender, RoutedEventArgs e) {
			// select folder
			FileExplorer fileExplorer = BlFactory.GetFileExplorer();
			string path = fileExplorer.SelectFolder();
			ImportController importController = new ImportController();
			Task.Run<bool>(async () => await importController.ExportTours(path));
		}

		
	}
}