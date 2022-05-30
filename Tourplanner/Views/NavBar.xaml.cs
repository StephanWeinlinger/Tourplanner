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
	}
}
