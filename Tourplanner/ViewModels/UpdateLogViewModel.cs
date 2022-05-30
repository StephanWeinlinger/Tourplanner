using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourplanner.Client.ViewModels {
	class UpdateLogViewModel : BaseViewModel {
		private string _currentDate;
		private string _currentComment;
		private string _currentDifficulty;
		private string _currentTime;
		private string _currentRating;

		private string _currentPopularity;
		private string _currentChildFriendlyness;

		public string CurrentLogDate {
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

		public string CurrentLogDifficulty {
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

		public string CurrentLogRating {
			get { return _currentRating; }
			set {
				_currentRating = value;
				OnPropertyChanged();
			}
		}

	}
}
