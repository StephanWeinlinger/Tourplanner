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
	class UpdateLogViewModel : BaseViewModel {
		
		private DateTime _currentDate;
		private string _currentComment;
		private int _currentDifficulty;
		private string _currentTime;
		private int _currentRating;

		public ICommand UpdateLogDB { get; set; }

		private string _currentPopularity;
		private string _currentChildFriendlyness;

		public DateTime CurrentLogDate {
			get { return _currentDate; }
			set {
				_currentDate = value;
				OnPropertyChanged();
			}
		}

		public string CurrentLogComment {
			get { return _currentComment; }
			set {
				_currentComment = value;
				OnPropertyChanged();
			}
		}

		public int CurrentLogDifficulty {
			get { return _currentDifficulty; }
			set {
				_currentDifficulty = value;
				OnPropertyChanged();
			}
		}

		public string CurrentLogTime {
			get { return _currentTime; }
			set {
				_currentTime = value;
				OnPropertyChanged();
			}
		}

		public int CurrentLogRating {
			get { return _currentRating; }
			set {
				_currentRating = value;
				OnPropertyChanged();
			}
		}

		public void UpdateLog() {
			LogController logcontroller = new LogController();
			Log NewLog = new Log(50, CurrentLogDate, CurrentLogComment, CurrentLogDifficulty, CurrentLogTime, CurrentLogRating);
			Task.Run<Log>(async () => await logcontroller.UpdateLog(31,NewLog));
			MessageBox.Show("Log was sucsessfully updated", "Update your Log");
		}

		public UpdateLogViewModel() {
			UpdateLogDB = new RelayCommand((sender) => {
				UpdateLog();
			});
		}
	}
}
