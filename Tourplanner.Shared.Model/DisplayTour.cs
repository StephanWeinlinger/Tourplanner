using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourplanner.Shared.Model {
	public class DisplayTour : INotifyPropertyChanged {
		
		public event PropertyChangedEventHandler? PropertyChanged;
		public CombinedTour displaytour;
		public Log displaylog;

		public DisplayTour(CombinedTour displaytour) {
			this.displaytour = displaytour;
		}

		public string Title {
			get => displaytour.Name; set {
				displaytour.Name = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
			}
		}

		public string Description {
			get => displaytour.Description; set {
				displaytour.Description = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
			}
		}

		public string From {
			get => displaytour.From; set {
				displaytour.From = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(From)));
			}
		}

		public string To {
			get => displaytour.To; set {
				displaytour.To = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(To)));
			}
		}

		public string TransportType {
			get => displaytour.TransportType; set {
				displaytour.TransportType = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(To)));
			}
		}

		public double Distance {
			get => displaytour.Distance; set {
				displaytour.Distance = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(To)));
			}
		}
		public string Time {
			get => displaytour.Time; set {
				displaytour.Time = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(To)));
			}
		}

		
		/*public string LogDate {
			get => displaytour.To; set {
				displaytour.To = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(To)));
			}
		}
		
		public string LogDifficulty {
			get => displaytour.To; set {
				displaytour.To = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(To)));
			}
		}
		public string LogTime {
			get => displaytour.To; set {
				displaytour.To = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(To)));
			}
		}
		public string LogRating {
			get => displaytour.To; set {
				displaytour.To = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(To)));
			}
		}*/

	}
}
