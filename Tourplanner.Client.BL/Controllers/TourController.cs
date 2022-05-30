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

        public async Task<List<CombinedTour>> GetCombinedTours(string filter = "") {
            return await _apiHandler.Get<List<CombinedTour>>($"Tour?filter={filter}");
        }

        // returns CombindedTour, since its easier to add it to the collection that way
        public async Task<CombinedTour> InsertTour(Tour newTour) {
            Tour insertedTour = await _apiHandler.Post<Tour>("Tour", newTour);
            return new CombinedTour(insertedTour, new List<Log>());
        }

        public async Task<CombinedTour> UpdateTour(int id, Tour newTour) {
            Tour updatedTour = await _apiHandler.Put<Tour>($"Tour/{id}", newTour);
            return new CombinedTour(updatedTour, new List<Log>());
        }

        public async Task<bool> DeleteTour(int id) {
            bool response = await _apiHandler.Delete($"Tour/{id}");
            return response;
        }
    }
}