using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using Tourplanner.Server.DAL;
using Tourplanner.Server.DAL.DAO;
using Tourplanner.Shared.Model;

namespace Tourplanner.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase {

	    [HttpPost]
	    public async Task<ActionResult<Log>> InsertLog(Log newLog) {
		    LogDao logDao = DalFactory.CreateLogDao();
		    Log log = logDao.InsertLog(newLog);
		    return log;
	    }

	    [HttpPut("{id}")]
	    public async Task<ActionResult<Log>> UpdateLog(int id, Log updatedLog) {
		    LogDao logDao = DalFactory.CreateLogDao();
		    Log log = logDao.UpdateLog(id, updatedLog);
		    return log;
		}

	    [HttpDelete("{id}")]
	    public async Task<ActionResult> DeleteLog(int id) {
		    LogDao logDao = DalFactory.CreateLogDao();
		    logDao.DeleteLog(id);
			return NoContent();
	    }
	}
}
