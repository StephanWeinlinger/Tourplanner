using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourplanner.Client.Commands;
using Tourplanner.Client.Views;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.ViewModels {
	public class ModifyTourViewModel : ViewModelBase {
		public ICommand CurrentCommand { get; }
		public MainViewModel MainViewModel;
		public ModifyTourView ModifyTourView;

		private readonly Dictionary<string, int> _transportTypeMapping = new Dictionary<string, int> {
			{ "Car", 0 },
			{ "Bicycle", 1 },
			{ "Walk", 2}
		};

		public ModifyTourViewModel(MainViewModel mainViewModel, ModifyTourView modifyTourView) {
			MainViewModel = mainViewModel;
			ModifyTourView = modifyTourView;
			ModifyTourView.DataContext = this;
			// set button text
			_buttonText = "Insert Tour";
			// set command (insert or update)
			CurrentCommand = new AddTourCommand(this);
		}

		public ModifyTourViewModel(MainViewModel mainViewModel, TourViewModel tourViewModel, ModifyTourView modifyTourView) {
			MainViewModel = mainViewModel;
			ModifyTourView = modifyTourView;
			ModifyTourView.DataContext = this;
			// initialize fields with values
			_id = Int32.Parse(tourViewModel.Id);
			_title = tourViewModel.Name;
			_description = tourViewModel.Description;
			_from = tourViewModel.From;
			_to = tourViewModel.To;
			_transportType = tourViewModel.TransportType;
			// set button text and transport type index
			_buttonText = "Update Tour";
			_transportTypeIndex = _transportTypeMapping[tourViewModel.TransportType];
			// set command (insert or update)
			CurrentCommand = new UpdateTourCommand(this);
		}

		private int _id;
		public int Id {
			get {
				return _id;
			}
			set {
				_id = value;
				OnPropertyChanged(nameof(Id));
			}
		}

		private string _title;
		public string Title {
			get {
				return _title;
			}
			set {
				_title = value;
				OnPropertyChanged(nameof(Title));
			}
		}

		private string _description;
		public string Description {
			get {
				return _description;
			}
			set {
				_description = value;
				OnPropertyChanged(nameof(Description));
			}
		}

		private string _from;
		public string From {
			get {
				return _from;
			}
			set {
				_from = value;
				OnPropertyChanged(nameof(From));
			}
		}

		private string _to;
		public string To {
			get {
				return _to;
			}
			set {
				_to = value;
				OnPropertyChanged(nameof(To));
			}
		}

		private string _transportType;
		public string TransportType {
			get {
				return _transportType;
			}
			set {
				_transportType = value;
				OnPropertyChanged(nameof(TransportType));
			}
		}

		private int _transportTypeIndex;
		public int TransportTypeIndex {
			get {
				return _transportTypeIndex;
			}
			set {
				_transportTypeIndex = value;
				OnPropertyChanged(nameof(TransportTypeIndex));
			}
		}

		private string _buttonText;
		public string ButtonText {
			get {
				return _buttonText;
			}
			set {
				_buttonText = value;
				OnPropertyChanged(nameof(ButtonText));
			}
		}
	}
}
