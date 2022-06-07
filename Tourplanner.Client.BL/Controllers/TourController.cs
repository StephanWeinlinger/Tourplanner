using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.BL.Controllers {
	public class TourController {
		private ApiHandler _apiHandler;

		public TourController() {
			_apiHandler = BlFactory.GetApiHandler();
		}

		public async Task<(List<CombinedTour>, CustomResponse)> GetCombinedTours(string filter = "") {
			return await _apiHandler.Get<List<CombinedTour>>($"Tour?filter={filter}");
		}

		public async Task<(CombinedTour, CustomResponse)> InsertTour(Tour newTour) {
			var (insertedTour, response) = await _apiHandler.Post<Tour>("Tour", newTour);
			CombinedTour combinedTour = null;
			if(response.Success) {
				combinedTour = new CombinedTour(insertedTour, new List<Log>());
			}
			return (combinedTour, response);
		}

		public async Task<(CombinedTour, CustomResponse)> UpdateTour(int id, Tour newTour) {
			var (updatedTour, response) = await _apiHandler.Put<Tour>($"Tour/{id}", newTour);
			CombinedTour combinedTour = null;
			if(response.Success) {
				combinedTour = new CombinedTour(updatedTour, new List<Log>());
			}
			return (combinedTour, response);
		}

		public async Task<CustomResponse> DeleteTour(int id) {
			return await _apiHandler.Delete($"Tour/{id}");
		}
	}
}