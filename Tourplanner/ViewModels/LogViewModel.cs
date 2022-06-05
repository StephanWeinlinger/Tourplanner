using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.ViewModels {
	public class LogViewModel : ViewModelBase {
		private readonly Log _log;

		public string Id => _log.Id.ToString();
		public string TourId => _log.TourId.ToString();
		public string Date => _log.Date.ToShortDateString();
		public string Comment => _log.Comment;
		public string Difficulty => _log.Difficulty.ToString();
		public string Time => _log.Time;
		public string Rating => _log.Rating.ToString();

		public LogViewModel(Log log) {
			_log = log;
		}
	}
}
