using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourplanner.Client.ViewModels {
	class TourDetailsViewModel : BaseViewModel {
		private string _currentFrom;
		private string _currentTo;
		private string _currentTransportType;
		private string _currentDistance;
		private string _currentEstimatedTime;
		private string _currentPopularity;
		private string _currentChildFriendliness;

		public string CurrentFrom {
			get { return _currentFrom; }
			set {
				_currentFrom = value;
				OnPropertyChanged();
			}
		}
		public string CurrentTo {
			get { return _currentTo; }
			set {
				_currentTo = value;
				OnPropertyChanged();
			}
		}
		public string CurrentTransportType {
			get { return _currentTransportType; }
			set {
				_currentTransportType = value;
				OnPropertyChanged();
			}
		}
		public string CurrentDistance {
			get { return _currentDistance; }
			set {
				_currentDistance = value;
				OnPropertyChanged();
			}
		}
		public string CurrentEstimatedTime {
			get { return _currentEstimatedTime; }
			set {
				_currentEstimatedTime = value;
				OnPropertyChanged();
			}
		}
		public string CurrentPopularity {
			get { return _currentPopularity; }
			set {
				_currentPopularity = value;
				OnPropertyChanged();
			}
		}
		public string CurrentChildFriendliness {
			get { return _currentChildFriendliness; }
			set {
				_currentChildFriendliness = value;
				OnPropertyChanged();
			}
		}
		



	}
}
