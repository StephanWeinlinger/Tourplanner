using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourplanner.Client.Commands;
using Tourplanner.Client.Views;

namespace Tourplanner.Client.ViewModels {
	public class ModifyLogViewModel : ViewModelBase {
		public ICommand CurrentCommand { get; }
		public MainViewModel MainViewModel;
		public ModifyLogView ModifyLogView;

		public ModifyLogViewModel(MainViewModel mainViewModel, ModifyLogView modifyLogView) {
			MainViewModel = mainViewModel;
			ModifyLogView = modifyLogView;
			ModifyLogView.DataContext = this;
			// set tourId
			_tourId = Int32.Parse(mainViewModel.CurrentTour.Id);
			// set button text
			_buttonText = "Insert Log";
			// set command (insert or update)
			CurrentCommand = new AddLogCommand(this);
		}

		public ModifyLogViewModel(MainViewModel mainViewModel, LogViewModel logViewModel, ModifyLogView modifyLogView) {
			MainViewModel = mainViewModel;
			ModifyLogView = modifyLogView;
			ModifyLogView.DataContext = this;
			// initialize fields with values
			_id = Int32.Parse(logViewModel.Id);
			_tourId = Int32.Parse(logViewModel.TourId);
			_date = DateTime.Parse(logViewModel.Date);
			_comment = logViewModel.Comment;
			_difficulty = Int32.Parse(logViewModel.Difficulty);
			_time = logViewModel.Time;
			_rating = Int32.Parse(logViewModel.Rating);
			// set button text
			_buttonText = "Update Log";
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
	}
}
