using System.Threading.Tasks;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.ViewModels {
	class AddTourViewmodel : BaseViewModel  {

		private string _tourNameInput;
		private string _tourDescriptionInput;
		private string _fromInput;
		private string _toInput;
		private string _transportType;

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
			get { return _transportType; }
			set {
				_transportType = value;
				OnPropertyChanged();
			}
		}
		public void AddTour() {
			TourController tourcontroller = new TourController();
			Tour NewTour = new Tour(TourTitle, TourDescription, FromInput, ToInput,"car");
			Task.Run<CombinedTour>(async () => await tourcontroller.InsertTour(NewTour));

		}
		// Add Tour von BL mit variablen 
	}
}