using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourplanner.Client.BL;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Client.ViewModels;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.Commands {
	public class ImportCommand : CommandBase {
		private MainViewModel _mainViewModel;

		public ImportCommand(MainViewModel mainViewModel) {
			_mainViewModel = mainViewModel;
		}

		public override void Execute(object parameter) {
			// select file
			FileExplorer fileExplorer = BlFactory.GetFileExplorer();
			string path = fileExplorer.SelectFile();
			if(path == null) {
				return;
			}
			// add tours to database
			ImportController importController = new ImportController();
			List<CombinedTour> combinedTours = Task.Run<List<CombinedTour>>(async () => await importController.ImportTours(path)).Result;
			// clear and add tours to collection
			_mainViewModel.ToursCollection.Clear();
			foreach(CombinedTour combinedTour in combinedTours) {
				_mainViewModel.ToursCollection.Add(new TourViewModel(combinedTour));
			}
		}
	}
}
