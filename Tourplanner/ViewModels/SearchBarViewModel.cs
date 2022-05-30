using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourplanner.Client.ViewModels {
	class SearchBarViewModel : BaseViewModel {

		private string _searchText;

		// CustomCommands Searchcomand

		public string SearchText {
			get => _searchText;
			set {
				_searchText = value;
				OnPropertyChanged();
			}
		}

		public SearchBarViewModel() {
			// eingabe validieren
			// searchcommand ausführen
		}

	}
}
