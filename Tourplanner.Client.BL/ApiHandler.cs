using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.BL {
	public class ApiHandler {
		private HttpClient _client;

		public ApiHandler() {
			_client = new HttpClient();
			// TODO replace with config file
			_client.BaseAddress = new Uri("https://localhost:44314/api/");
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		// TODO check if necessary
		~ApiHandler() {
			_client.Dispose();
		}

		public async Task<T> Get<T>(string url) {
			HttpResponseMessage response = await _client.GetAsync(url);
			if(!response.IsSuccessStatusCode) {
				CustomResponse errorResponse = await response.Content.ReadAsAsync<CustomResponse>();
				throw new ArgumentException(errorResponse.Errors.ContainsKey("Custom") ? errorResponse.Errors["Custom"] : "Unkown Error");
			}

			return await response.Content.ReadAsAsync<T>();
		}

        public async Task<T> Post<T>(string url, T newEntry) {
            HttpResponseMessage response = await _client.PostAsJsonAsync(url, newEntry);
            if (!response.IsSuccessStatusCode) {
                CustomResponse errorResponse = await response.Content.ReadAsAsync<CustomResponse>();
                throw new ArgumentException(errorResponse.Errors.ContainsKey("Custom") ? errorResponse.Errors["Custom"] : "Unkown Error");
            }

            return await response.Content.ReadAsAsync<T>();
		}

        public async Task<T> Put<T>(string url, T newEntry) {
            HttpResponseMessage response = await _client.PutAsJsonAsync(url, newEntry);
            if (!response.IsSuccessStatusCode) {
                CustomResponse errorResponse = await response.Content.ReadAsAsync<CustomResponse>();
                throw new ArgumentException(errorResponse.Errors.ContainsKey("Custom") ? errorResponse.Errors["Custom"] : "Unkown Error");
            }

            return await response.Content.ReadAsAsync<T>();
		}

		public async Task<bool> Delete(string url) {
            HttpResponseMessage response = await _client.DeleteAsync(url);
            if (!response.IsSuccessStatusCode) {
                CustomResponse errorResponse = await response.Content.ReadAsAsync<CustomResponse>();
                throw new ArgumentException(errorResponse.Errors.ContainsKey("Custom") ? errorResponse.Errors["Custom"] : "Unkown Error");
            }

            return true;
        }
	}
}
