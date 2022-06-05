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
	public class ExportCommand : CommandBase {
		public override void Execute(object parameter) {
			// select folder
			FileExplorer fileExplorer = BlFactory.GetFileExplorer();
			string path = fileExplorer.SelectFolder();
			// get current tours from database
			ImportController importController = new ImportController();
			bool success = Task.Run<bool>(async () => await importController.ExportTours(path)).Result;
		}
	}
}
