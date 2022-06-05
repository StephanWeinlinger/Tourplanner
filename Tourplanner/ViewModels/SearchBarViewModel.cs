using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourplanner.Client.Commands;

namespace Tourplanner.Client.ViewModels {
	public class SearchBarViewModel : ViewModelBase {
		public ICommand SearchCommand { get; }
		public ICommand ClearCommand { get; }

		public event EventHandler<string> OnSearchClicked;
		public event EventHandler<string> OnClearClicked;

		private string _filter;
		public string Filter {
			get {
				return _filter;
			}
			set {
				_filter = value;
				OnPropertyChanged(nameof(Filter));
			}
		}

		public SearchBarViewModel() {
			SearchCommand = new EventCommand((_) => {
				OnSearchClicked?.Invoke(this, Filter);
			});
			ClearCommand = new EventCommand((_) => {
				OnClearClicked?.Invoke(this, "");
			});
		}
	}
}
