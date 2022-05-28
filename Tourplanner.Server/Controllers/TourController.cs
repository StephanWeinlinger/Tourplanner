﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
	    public async Task<ActionResult<List<CombinedTour>>> GetCombinedTours(string filter) {
		    List<CombinedTour> combinedTours = new List<CombinedTour>();
		    TourDao tourDao = DalFactory.CreateTourDao();
		    List<Tour> tours = new List<Tour>();
			// check if request includes a filter
			if(filter == null) {
				// get all tours
				tours = tourDao.GetAllTours();
			} else {
				// get filtered tours
				tours = tourDao.GetAllToursWithFilter(filter.ToLower());
			}
		    
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

		    try {
			    // get information from mapquest
			    MapQuest mapQuest = DalFactory.GetMapQuest();
			    MapQuestInformationResponse response =
				    await mapQuest.GetInformation(newTour.From, newTour.To, newTour.TransportType);
				newTour.Distance = response.Distance;
				newTour.Time = response.FormattedTime;

				// insert tour in database
			    TourDao tourDao = DalFactory.CreateTourDao();
			    Tour tour = tourDao.InsertTour(newTour);

				// get image link from mapquest
				string url = mapQuest.GetMap(response.SessionId);
				// download image to filesystem
				Filesystem filesystem = DalFactory.GetFilesystem();
				filesystem.DownloadImage(url, tour.Id);

			    return tour;
		    } catch(HttpRequestException e) {
			    return BadRequest(new CustomResponse(false, new Dictionary<string, string> { { "Custom", e.Message } }));
			} catch(Exception e) {
			    return BadRequest(new CustomResponse(false, new Dictionary<string, string>{ {"Custom", "Error in database"} }));
		    }
	    }

	    [HttpPut("{id}")]
	    public async Task<ActionResult<Tour>> UpdateTour(int id, Tour newTour) {
		    if(!ModelState.IsValid) {
			    return BadRequest(ModelState);
		    }

			try {
				// get information from mapquest
				MapQuest mapQuest = DalFactory.GetMapQuest();
				MapQuestInformationResponse response =
					await mapQuest.GetInformation(newTour.From, newTour.To, newTour.TransportType);
				newTour.Distance = response.Distance;
				newTour.Time = response.FormattedTime;

				// update tour in database
				TourDao tourDao = DalFactory.CreateTourDao();
				Tour tour = tourDao.UpdateTour(id, newTour);

				// get image link from mapquest
				string url = mapQuest.GetMap(response.SessionId);
				// download image to filesystem
				Filesystem filesystem = DalFactory.GetFilesystem();
				filesystem.DownloadImage(url, tour.Id);

				return tour;
			} catch(HttpRequestException e) {
				return BadRequest(new CustomResponse(false, new Dictionary<string, string> { { "Custom", e.Message } }));
			} catch(Exception e) {
				return BadRequest(new CustomResponse(false, new Dictionary<string, string> { { "Custom", "Error in database" } }));
			}
		}

	    [HttpDelete("{id}")]
	    public async Task<ActionResult> DeleteTour(int id) {
			TourDao tourDao = DalFactory.CreateTourDao();
			tourDao.DeleteTour(id);

			Filesystem filesystem = DalFactory.GetFilesystem();
			filesystem.RemoveImage(id);
			filesystem.RemoveAllImages();
			return Ok();
	    }
	}
}
