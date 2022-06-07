using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tourplanner.Client.BL;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Client.ViewModels;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.Commands {
	public class CreateSummarizedReportCommand : CommandBase {
		private MainViewModel _mainViewModel;

		public CreateSummarizedReportCommand(MainViewModel mainViewModel) {
			_mainViewModel = mainViewModel;
		}

		public override void Execute(object parameter) {
			// select folder
			FileExplorer fileExplorer = BlFactory.GetFileExplorer();
			string path = fileExplorer.SelectFolder();
			if(path == null) {
				return;
			}
			// get tours from database and generate report
			ReportController reportController = new ReportController();
			CustomResponse response = Task.Run<CustomResponse>(async () => await reportController.GenerateSummarizedReport(path)).Result;
			CheckError(response);
		}
	}
}
