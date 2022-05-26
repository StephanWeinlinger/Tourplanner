using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using Tourplanner.Server.DAL;
using Tourplanner.Server.DAL.DAO;
using Tourplanner.Server.Response;
using Tourplanner.Shared.Model;

namespace Tourplanner.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TourController : ControllerBase {

	    [HttpGet]
	    public async Task<ActionResult<List<CombinedTour>>> GetCombinedTours() {
		    List<CombinedTour> combinedTours = new List<CombinedTour>();
			// get all tours
		    TourDao tourDao = DalFactory.CreateTourDao();
		    List<Tour> tours = tourDao.GetAllTours();
			// get all logs for each tour
		    foreach(Tour entry in tours) {
				LogDao logDao = DalFactory.CreateLogDao();
				List<Log> logs = logDao.GetAllLogsWithTourId(entry.Id);
				// add entry and logs to combindedTours
				combinedTours.Add(new CombinedTour(entry, logs));
			}
		    return combinedTours;
	    }

	    [HttpPost]
	    public async Task<ActionResult<Tour>> InsertTour(Tour newTour) {
		    if(!ModelState.IsValid) {
			    return BadRequest(ModelState);
		    }
		    TourDao tourDao = DalFactory.CreateTourDao();
			try {
			    Tour tour = tourDao.InsertTour(newTour);
			    return tour;
		    } catch(Exception e) {
			    return BadRequest(new CustomResponse(false, new Dictionary<string, string>{ {"Custom", "Error in database"} }));
		    }
	    }

	    [HttpPut("{id}")]
	    public async Task<ActionResult<Tour>> UpdateTour(int id, Tour newTour) {
		    if(!ModelState.IsValid) {
			    return BadRequest(ModelState);
		    }
			TourDao tourDao = DalFactory.CreateTourDao();
			try {
				Tour tour = tourDao.UpdateTour(id, newTour);
				return tour;
		    } catch(Exception e) {
			    return BadRequest(new CustomResponse(false, new Dictionary<string, string> { { "Custom", "Error in database" } }));
		    }
		}

	    [HttpDelete("{id}")]
	    public async Task<ActionResult> DeleteTour(int id) {
			TourDao tourDao = DalFactory.CreateTourDao();
			tourDao.DeleteTour(id);
			return Ok();
	    }
	}
}
