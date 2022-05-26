﻿using Microsoft.AspNetCore.Mvc;
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
				combinedTours.Add(new CombinedTour(
					entry.Id, entry.Name, entry.Description, entry.From, entry.To, entry.TransportType, entry.Distance, entry.Time, logs
					));
			}
		    return combinedTours;
	    }

	    [HttpPost]
	    public async Task<ActionResult<Tour>> InsertTour(Tour newTour) {
		    TourDao tourDao = DalFactory.CreateTourDao();
		    Tour tour = tourDao.InsertTour(newTour);
		    return tour;
	    }

	    [HttpPut("{id}")]
	    public async Task<ActionResult<Tour>> UpdateTour(int id, Tour newTour) {
			TourDao tourDao = DalFactory.CreateTourDao();
			Tour tour = tourDao.UpdateTour(id, newTour);
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
