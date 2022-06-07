using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourplanner.Client.Commands;
using Tourplanner.Client.Views;

namespace Tourplanner.Client.ViewModels {
	public class ModifyLogViewModel : ViewModelBase {
		public ICommand CurrentCommand { get; }
		public MainViewModel MainViewModel;
		public ModifyLogView ModifyLogView;

		private readonly ErrorsViewModel _errorsViewModel;

		public ModifyLogViewModel(MainViewModel mainViewModel, ModifyLogView modifyLogView) {
			_errorsViewModel = new ErrorsViewModel();
			_errorsViewModel.ErrorsChanged += OnErrorsChanged;

			MainViewModel = mainViewModel;
			ModifyLogView = modifyLogView;
			ModifyLogView.DataContext = this;
			// set tourId
			TourId = Int32.Parse(mainViewModel.CurrentTour.Id);
			// set default rating, difficulty and date
			Rating = 3;
			Difficulty = 3;
			Date = DateTime.Now;
			// set button text
			ButtonText = "Insert Log";
			// set command (insert or update)
			CurrentCommand = new AddLogCommand(this);

			_errorsViewModel = new ErrorsViewModel();
			_errorsViewModel.ErrorsChanged += OnErrorsChanged;
			_errorsViewModel.AddError(nameof(Comment), "Comment can't be empty");
			_errorsViewModel.AddError(nameof(Time), "Time can't be empty");
		}

		public ModifyLogViewModel(MainViewModel mainViewModel, LogViewModel logViewModel, ModifyLogView modifyLogView) {
			_errorsViewModel = new ErrorsViewModel();
			_errorsViewModel.ErrorsChanged += OnErrorsChanged;

			MainViewModel = mainViewModel;
			ModifyLogView = modifyLogView;
			ModifyLogView.DataContext = this;
			// initialize fields with values
			Id = Int32.Parse(logViewModel.Id);
			TourId = Int32.Parse(logViewModel.TourId);
			Date = DateTime.Parse(logViewModel.Date);
			Comment = logViewModel.Comment;
			Difficulty = Int32.Parse(logViewModel.Difficulty);
			Time = logViewModel.Time;
			Rating = Int32.Parse(logViewModel.Rating);
			// set button text
			ButtonText = "Update Log";
			// set command (insert or update)
			CurrentCommand = new UpdateLogCommand(this);
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

		private int _tourId;
		public int TourId {
			get {
				return _tourId;
			}
			set {
				_tourId = value;
				OnPropertyChanged(nameof(TourId));
			}
		}

		private DateTime _date;
		public DateTime Date {
			get {
				return _date;
			}
			set {
				_date = value;
				OnPropertyChanged(nameof(Date));
			}
		}

		private string _comment;
		public string Comment {
			get {
				return _comment;
			}
			set {
				_comment = value;
				_errorsViewModel.ClearErrors(nameof(Comment));
				if(_comment == "") {
					_errorsViewModel.AddError(nameof(Comment), "Comment can't be empty");
				}
				OnPropertyChanged(nameof(Comment));
			}
		}

		private int _difficulty;
		public int Difficulty {
			get {
				return _difficulty;
			}
			set {
				_difficulty = value;
				OnPropertyChanged(nameof(Difficulty));
			}
		}

		private string _time;
		public string Time {
			get {
				return _time;
			}
			set {
				_time = value;
				_errorsViewModel.ClearErrors(nameof(Time));
				if(_time == "") {
					_errorsViewModel.AddError(nameof(Time), "Time can't be empty");
				}
				Regex pattern = new Regex(@"^[0-9]{2}:[0-5][0-9]:[0-5][0-9]$");
				if(!pattern.Match(_time).Success) {
					_errorsViewModel.AddError(nameof(Time), "Time is not in the right format");
				}
				OnPropertyChanged(nameof(Time));
			}
		}

		private int _rating;
		public int Rating {
			get {
				return _rating;
			}
			set {
				_rating = value;
				OnPropertyChanged(nameof(Rating));
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
