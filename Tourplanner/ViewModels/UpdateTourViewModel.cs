using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.ViewModels {
	class UpdateTourViewModel : BaseViewModel{
		private string _tourNameInput;
		private string _tourDescriptionInput;
		private string _fromInput;
		private string _toInput;
		private string _transportType;

		public ICommand UpdateTourDB { get; set; }

		public string TourTitle {
			get { return _tourNameInput; }
			set {
				_tourNameInput = value;
				OnPropertyChanged();
			}
		}
		public string TourDescription {
			get { return _tourDescriptionInput; }
			set {
				_tourDescriptionInput = value;
				OnPropertyChanged();
			}
		}
		public string FromInput {
			get { return _fromInput; }
			set {
				_fromInput = value;
				OnPropertyChanged();
			}
		}
		public string ToInput {
			get { return _toInput; }
			set {
				_toInput = value;
				OnPropertyChanged();
			}
		}
		public string TransportationType {
			get { return _transportType = "Car"; }
			set {
				_transportType = "Car";
				OnPropertyChanged();
			}
		}
		public void UpdateTour() {
			TourController tourcontroller = new TourController();
			Tour NewTour = new Tour(TourTitle, TourDescription, FromInput, ToInput, TransportationType);
			Task.Run<CombinedTour>(async () => await tourcontroller.UpdateTour(53,NewTour));
			MessageBox.Show("Tour was sucsessfully updated", "Update your Tour");

		}

		public UpdateTourViewModel() {
			UpdateTourDB = new RelayCommand((sender) => {
				MessageBox.Show("Siu", "Update your Tour");

				UpdateTour();
			});
		}

	}
}
