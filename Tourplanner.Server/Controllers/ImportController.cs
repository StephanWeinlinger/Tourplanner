﻿using Microsoft.AspNetCore.Mvc;
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
		// TODO maybe add exception handling, even though it shouldn't throw them, since ids are set by database
		[HttpPost]
		public async Task<ActionResult<List<CombinedTour>>> ImportTours(List<CombinedTour> newCombinedTours) {
			if(!ModelState.IsValid) {
				return BadRequest(ModelState);
			}
			
			// delete all db entries
			TourDao tourDao1 = DalFactory.CreateTourDao();
			tourDao1.DeleteAllTours();
			// TODO delete all images

			List<CombinedTour> combinedTours = new List<CombinedTour>();
			// map over each entry and query mapquest, add tour and log to database and add image
			foreach(CombinedTour entry in newCombinedTours) {
				// query mapquest, on error skip (continue)
				// TODO mapquest
				// add to db, shouldn't cause error
				TourDao tourDao2 = new TourDao();
				// insert using distance and time from mapquest
				Tour newTour = tourDao2.InsertTour(new Tour(entry.Name, entry.Description, entry.From, entry.To, entry.TransportType, entry.Distance, entry.Time));
				// add to combinedTours for return
				combinedTours.Add(new CombinedTour(newTour, new List<Log>()));
				foreach(Log logEntry in entry.Logs) {
					LogDao logDao1 = new LogDao();
					// insert using TourId of newTour
					Log newLog = logDao1.InsertLog(new Log(newTour.Id, logEntry.Date, logEntry.Comment,
						logEntry.Difficulty, logEntry.Time, logEntry.Rating));
					// add to combinedTours for return
					combinedTours[combinedTours.Count].Logs.Add(newLog);
				}
				// add image, shouldn't cause error
				// TODO add image
			}
			return combinedTours;
		}
	}
}
