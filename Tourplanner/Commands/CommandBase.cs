using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourplanner.Client.BL;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.Commands {
	public abstract class CommandBase : ICommand {
		public event EventHandler CanExecuteChanged;

		public virtual bool CanExecute(object parameter) {
			return true;
		}

		public abstract void Execute(object parameter);

		public bool CheckError(CustomResponse response) {
			if(!response.Success) {
				string message = response.Errors.ContainsKey("Custom") ? response.Errors["Custom"] : "Unknown Error";
				MessageBox.Show(message, "Tourplanner", MessageBoxButton.OK, MessageBoxImage.Error);
				BlFactory.GetLogger().Warn(message);
				return true;
			}
			return false;
		}
	}
}
