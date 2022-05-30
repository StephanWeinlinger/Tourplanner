using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourplanner.Client.ViewModels {
	class AddLogViewModel : BaseViewModel {
		
		private string _date;
		private string _comment;
		private string _difficulty;
		private string _time;
		private string _rating;

		private string _popularity;
		private string _childfriendlyness;

		public string LogDate {
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

		public string LogDifficulty {
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

		public string LogRating {
			get { return _rating; }
			set {
				_rating = value;
				OnPropertyChanged();
			}
		}
	}
	
}
