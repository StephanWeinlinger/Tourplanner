using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
	    public async Task<ActionResult<Tour>> InsertTour() {
		    return NotFound();
	    }

	    [HttpPut]
	    public async Task<ActionResult<Tour>> UpdateTour() {
		    return NotFound();
	    }

	    [HttpDelete]
	    public async Task<ActionResult<Tour>> DeleteTour() {
		    return NotFound();
	    }
	}
}
