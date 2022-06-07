using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

		private readonly ErrorsViewModel _errorsViewModel;

		public ModifyTourViewModel(MainViewModel mainViewModel, ModifyTourView modifyTourView) {
			MainViewModel = mainViewModel;
			ModifyTourView = modifyTourView;
			ModifyTourView.DataContext = this;
			// set default TransportType
			_transportType = "Car";
			// set button text
			_buttonText = "Insert Tour";
			// set command (insert or update)
			CurrentCommand = new AddTourCommand(this);

			_errorsViewModel = new ErrorsViewModel();
			_errorsViewModel.ErrorsChanged += OnErrorsChanged;
		}

		public ModifyTourViewModel(MainViewModel mainViewModel, TourViewModel tourViewModel, ModifyTourView modifyTourView) {
			_errorsViewModel = new ErrorsViewModel();
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
			// set command (insert or update)
			CurrentCommand = new UpdateTourCommand(this);

			_errorsViewModel = new ErrorsViewModel();
			_errorsViewModel.ErrorsChanged += OnErrorsChanged;
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

		public bool CanSubmit => !HasErrors;

		public bool HasErrors => _errorsViewModel.HasErrors;

		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		public IEnumerable GetErrors(string propertyName) {
			return _errorsViewModel.GetErrors(propertyName);
		}

		private void OnErrorsChanged(object sender, DataErrorsChangedEventArgs e) {
			ErrorsChanged?.Invoke(this, e);
			OnPropertyChanged(nameof(CanSubmit));
		}
	}
}
