﻿using System;
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

		private void Import(object sender, RoutedEventArgs e) {
			ImportController import;
			import = new ImportController();
			Task.Run<List<CombinedTour>>(async () => await import.ImportTours());
			
		}

		private void Export(object sender, RoutedEventArgs e) {
			ImportController export;
			export = new ImportController();
			Task.Run<bool>(async () => await export.ExportTours());

		}

		
	}
}