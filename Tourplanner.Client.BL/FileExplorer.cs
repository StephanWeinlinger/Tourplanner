using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Tourplanner.Client.BL {
	public class FileExplorer {
		public string SelectFolder() {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if(openFileDialog.ShowDialog() == true) {
				return openFileDialog.FileName;
			}
			return null;
		}

		public string SelectFile() {
			using FolderBrowserDialog dialog = new FolderBrowserDialog {
				Description = "Select Folder", UseDescriptionForTitle = true, ShowNewFolderButton = true
			};
			if(dialog.ShowDialog() == DialogResult.OK) {
				return dialog.SelectedPath;
			}
			return null;
		}
	}
}
