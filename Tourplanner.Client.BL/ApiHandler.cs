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
		private ILoggerWrapper _logger;

		public ApiHandler(string url) {
			_client = new HttpClient();
			_client.BaseAddress = new Uri(url);
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_logger = BlFactory.GetLogger();
		}

		~ApiHandler() {
			_client.Dispose();
		}

		public async Task<(T, CustomResponse)> Get<T>(string url) {
			HttpResponseMessage response = await _client.GetAsync(url);
			CustomResponse errorResponse = new CustomResponse(true, new Dictionary<string, string>());
			if(!response.IsSuccessStatusCode) {
				errorResponse = await response.Content.ReadAsAsync<CustomResponse>();
				foreach(KeyValuePair<string, string> entry in errorResponse.Errors) {
					_logger.Error($"{entry.Key}: {entry.Value}");
				}
			}
			return (await response.Content.ReadAsAsync<T>(), errorResponse);
		}

		public async Task<(T, CustomResponse)> Post<T>(string url, T newEntry) {
			HttpResponseMessage response = await _client.PostAsJsonAsync(url, newEntry);
			CustomResponse errorResponse = new CustomResponse(true, new Dictionary<string, string>());
			if(!response.IsSuccessStatusCode) {
				errorResponse = await response.Content.ReadAsAsync<CustomResponse>();
				foreach(KeyValuePair<string, string> entry in errorResponse.Errors) {
					_logger.Error($"{entry.Key}: {entry.Value}");
				}
			}
			return (await response.Content.ReadAsAsync<T>(), errorResponse);
		}

		public async Task<(T, CustomResponse)> Put<T>(string url, T newEntry) {
			HttpResponseMessage response = await _client.PutAsJsonAsync(url, newEntry);
			CustomResponse errorResponse = new CustomResponse(true, new Dictionary<string, string>());
			if(!response.IsSuccessStatusCode) {
				errorResponse = await response.Content.ReadAsAsync<CustomResponse>();
				foreach(KeyValuePair<string, string> entry in errorResponse.Errors) {
					_logger.Error($"{entry.Key}: {entry.Value}");
				}
			}

			return (await response.Content.ReadAsAsync<T>(), errorResponse);
		}

		public async Task<CustomResponse> Delete(string url) {
			HttpResponseMessage response = await _client.DeleteAsync(url);
			CustomResponse errorResponse = new CustomResponse(true, new Dictionary<string, string>());
			if(!response.IsSuccessStatusCode) {
				errorResponse = await response.Content.ReadAsAsync<CustomResponse>();
				foreach(KeyValuePair<string, string> entry in errorResponse.Errors) {
					_logger.Error($"{entry.Key}: {entry.Value}");
				}
			}

			return errorResponse;
		}
	}
}