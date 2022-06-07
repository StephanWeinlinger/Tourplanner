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
		    if(!ModelState.IsValid) {
			    return BadRequest(ModelState);
		    }
		    if(newLog.Difficulty < 1 || newLog.Difficulty > 5 || newLog.Rating < 1 || newLog.Rating > 5) {
			    return BadRequest(new CustomResponse(false, new Dictionary<string, string> { { "Custom", "Difficulty and rating have to be between 1-5" } }));
			}
		    LogDao logDao = DalFactory.CreateLogDao();
		    try {
			    Log log = logDao.InsertLog(newLog);
			    return log;
		    } catch(Exception e) {
			    return BadRequest(new CustomResponse(false, new Dictionary<string, string> { { "Custom", "Error in database" } }));
		    }
	    }

	    [HttpPut("{id}")]
	    public async Task<ActionResult<Log>> UpdateLog(int id, Log updatedLog) {
		    if(!ModelState.IsValid) {
			    return BadRequest(ModelState);
		    }
			if(updatedLog.Difficulty < 1 || updatedLog.Difficulty > 5 || updatedLog.Rating < 1 || updatedLog.Rating > 5) {
				return BadRequest(new CustomResponse(false, new Dictionary<string, string> { { "Custom", "Difficulty and rating have to be between 1-5" } }));
		    }
			LogDao logDao = DalFactory.CreateLogDao();
			try {
				Log log = logDao.UpdateLog(id, updatedLog);
				if(log == null) {
					return BadRequest(new CustomResponse(false, new Dictionary<string, string> { { "Custom", "Id or TourId is invalid" } }));
				}
				return log;
			} catch(Exception e) {
				return BadRequest(new CustomResponse(false, new Dictionary<string, string> { { "Custom", "Error in database" } }));
			}
		}

	    [HttpDelete("{id}")]
	    public async Task<ActionResult> DeleteLog(int id) {
		    LogDao logDao = DalFactory.CreateLogDao();
		    logDao.DeleteLog(id);
			return Ok();
	    }
	}
}
