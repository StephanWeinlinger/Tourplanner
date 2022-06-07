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
			_errorsViewModel = new ErrorsViewModel();
			_errorsViewModel.ErrorsChanged += OnErrorsChanged;

			MainViewModel = mainViewModel;
			ModifyTourView = modifyTourView;
			ModifyTourView.DataContext = this;
			// set default TransportType
			TransportType = "Car";
			// set button text
			ButtonText = "Insert Tour";
			// set command (insert or update)
			CurrentCommand = new AddTourCommand(this);

			_errorsViewModel.AddError(nameof(Title), "Title can't be empty");
			_errorsViewModel.AddError(nameof(Description), "Description can't be empty");
			_errorsViewModel.AddError(nameof(From), "From can't be empty");
			_errorsViewModel.AddError(nameof(To), "To can't be empty");
		}

		public ModifyTourViewModel(MainViewModel mainViewModel, TourViewModel tourViewModel, ModifyTourView modifyTourView) {
			_errorsViewModel = new ErrorsViewModel();
			_errorsViewModel.ErrorsChanged += OnErrorsChanged;

			MainViewModel = mainViewModel;
			ModifyTourView = modifyTourView;
			ModifyTourView.DataContext = this;
			// initialize fields with values
			Id = Int32.Parse(tourViewModel.Id);
			Title = tourViewModel.Name;
			Description = tourViewModel.Description;
			From = tourViewModel.From;
			To = tourViewModel.To;
			TransportType = tourViewModel.TransportType;
			// set button text and transport type index
			ButtonText = "Update Tour";
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
				_errorsViewModel.ClearErrors(nameof(Title));
				if(_title == "") {
					_errorsViewModel.AddError(nameof(Title), "Title can't be empty");
				}
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
				_errorsViewModel.ClearErrors(nameof(Description));
				if(_description == "") {
					_errorsViewModel.AddError(nameof(Description), "Description can't be empty");
				}
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
				_errorsViewModel.ClearErrors(nameof(From));
				if(_from == "") {
					_errorsViewModel.AddError(nameof(From), "From can't be empty");
				}
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
				_errorsViewModel.ClearErrors(nameof(To));
				if(_to == "") {
					_errorsViewModel.AddError(nameof(To), "To can't be empty");
				}
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
