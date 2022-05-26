using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tourplanner.Server.DAL;
using Tourplanner.Server.DAL.DAO;
using Tourplanner.Shared.Model;

namespace Tourplanner.Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TourController : ControllerBase {

	    [HttpGet]
	    public async Task<ActionResult<IEnumerable<CombinedTour>>> GetCombinedTours() {
		    return NotFound();
	    }

	    [HttpPost]
	    public async Task<ActionResult<Tour>> InsertTour(Tour newTour) {
		    TourDao tourDao = DalFactory.CreateTourDao();
		    Tour tour = tourDao.InsertTour(newTour);
		    return tour;
	    }

	    [HttpPut]
	    public async Task<ActionResult<Tour>> UpdateTour(Tour newTour) {
			TourDao tourDao = DalFactory.CreateTourDao();
			Tour tour = tourDao.UpdateTour(newTour);
			return tour;
		}

	    [HttpDelete("{id}")]
	    public async Task<ActionResult> DeleteTour(int id) {
			TourDao tourDao = DalFactory.CreateTourDao();
			tourDao.DeleteTour(id);
			return NoContent();
	    }
	}
}
