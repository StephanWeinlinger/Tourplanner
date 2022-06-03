﻿using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Client.ViewModels;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.ViewModels {
	internal class AddTourViewmodel : BaseViewModel {

		private string _tourNameInput;
		private string _tourDescriptionInput;
		private string _fromInput;
		private string _toInput;
		private string _transportType;

		public ICommand AddTourtoDB { get; set; }

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
		
		public void AddTour() {
			TourController tourcontroller = new TourController();
			Tour NewTour = new Tour(TourTitle, TourDescription, FromInput, ToInput, TransportationType);
			Task.Run<CombinedTour>(async () => await tourcontroller.InsertTour(NewTour));
			MessageBox.Show("Tour was sucsessfully added", "Add your Tour");

		}

		
		public AddTourViewmodel() {
			AddTourtoDB = new RelayCommand((sender) => {
				AddTour();
			});
		}
	}
}