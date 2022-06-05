using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourplanner.Client.Commands {
	public class EventCommand : CommandBase {
		private readonly Action<object> _execute;

		public EventCommand(Action<object> execute) {
			_execute = execute;
		}

		public override void Execute(object parameter) {
			_execute.Invoke(parameter);
		}
	}
}
