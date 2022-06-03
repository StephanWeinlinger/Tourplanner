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
using System.Windows.Shapes;
using Tourplanner.Client.ViewModels;

namespace Tourplanner.Client.Views {
	/// <summary>
	/// Interaktionslogik für UpdateTour.xaml
	/// </summary>
	public partial class UpdateTour : Window {
		public UpdateTour() {
			InitializeComponent();
			DataContext = new UpdateTourViewModel();
		}
	}
	}

