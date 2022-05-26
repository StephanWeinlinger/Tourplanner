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
    public class ImportController : ControllerBase {

		// expects List<CombinedTour> without distance and time
		// if Id <-> TourId or Id from Log have conflicts the entry will be skipped
		// returns GetCombinedTours
		[HttpPost]
		public async Task<ActionResult<List<CombinedTour>>> ImportTours(List<CombinedTour> newCombinedTours) {
			if(!ModelState.IsValid) {
				return BadRequest(ModelState);
			}
			
			// delete all db entries
			TourDao tourDao1 = DalFactory.CreateTourDao();
			tourDao1.DeleteAllTours();
			// TODO delete all file entries

			// map over each entry and query mapquest, add tour and log to database and add image
			foreach(CombinedTour entry in newCombinedTours) {
				// query mapquest, on error continue
				// add to db, on error continue

				// add image, shouldn't cause error
			}

			// get all new combined tours
			// TODO maybe fix copy paste from TourController
			List<CombinedTour> combinedTours = new List<CombinedTour>();
			// get all tours
			TourDao tourDao2 = DalFactory.CreateTourDao();
			List<Tour> tours = tourDao2.GetAllTours();
			// get all logs for each tour
			foreach(Tour entry in tours) {
				LogDao logDao = DalFactory.CreateLogDao();
				List<Log> logs = logDao.GetAllLogsWithTourId(entry.Id);
				// add entry and logs to combindedTours
				combinedTours.Add(new CombinedTour(
					entry.Id, entry.Name, entry.Description, entry.From, entry.To, entry.TransportType, entry.Distance, entry.Time, logs
				));
			}
			return combinedTours;
		}
	}
}
