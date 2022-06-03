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
	class AddLogViewModel : BaseViewModel {
		
		private DateTime _date;
		private string _comment;
		private int _difficulty;
		private string _time;
		private int _rating;

		private string _popularity;
		private string _childfriendlyness;

		public ICommand AddLogDB { get; set; }

		public DateTime LogDate {
			get { return _date; }
			set {
				_date = value;
				OnPropertyChanged();
			}
		}

			public string LogComment {
			get { return _comment; }
			set {
				_comment = value;
				OnPropertyChanged();
			}
		}

		public int LogDifficulty {
			get { return _difficulty; }
			set {
				_difficulty = value;
				OnPropertyChanged();
			}
		}

		public string LogTime {
			get { return _time; }
			set {
				_time = value;
				OnPropertyChanged();
			}
		}

		public int LogRating {
			get { return _rating; }
			set {
				_rating = value;
				OnPropertyChanged();
			}
		}

		public void AddLog() {
			LogController logcontroller = new LogController();
			Log NewLog = new Log(50,LogDate, LogComment, LogDifficulty, LogTime, LogRating);
			Task.Run<Log>(async () => await logcontroller.InsertLog(NewLog));
			MessageBox.Show("Log was sucsessfully added", "Add your Log");
		}

		public AddLogViewModel() {
			AddLogDB = new RelayCommand((sender) => {
				AddLog();
			});
		}
	}
	
}
