using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.BL.Controllers {
    public class LogController {
        private ApiHandler _apiHandler;

        public LogController() {
            _apiHandler = BlFactory.GetApiHandler();
        }

        public async Task<Log> InsertLog(Log newLog) {
            return await _apiHandler.Post<Log>("Log", newLog);
        }

        public async Task<Log> UpdateLog(int id, Log newLog) {
            return await _apiHandler.Put<Log>($"Log/{id}", newLog);
        }

        public async Task<bool> DeleteLog(int id) {
            bool response = await _apiHandler.Delete($"Log/{id}");
            return response;
        }
    }
}