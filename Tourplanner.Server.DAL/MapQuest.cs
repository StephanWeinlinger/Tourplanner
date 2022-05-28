using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Tourplanner.Shared.Model;

namespace Tourplanner.Server.DAL {
	public class MapQuest {
		private HttpClient _client;
		private string _key;
		private Dictionary<string, string> _routeType;

		public MapQuest(string key) {
			_client = new HttpClient();
			_client.BaseAddress = new Uri("https://www.mapquestapi.com/");
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/jpeg"));
			_key = key;
			_routeType = new Dictionary<string, string> {
				{ "Car", "fastest"},
				{ "Bicycle", "bicycle"},
				{ "Walk", "pedestrian" }
			};
		}

		// TODO check if necessary
		~MapQuest() {
			_client.Dispose();
		}

		public async Task<MapQuestInformationResponse> GetInformation(string from, string to, string transportType) {
			HttpResponseMessage response = await _client.GetAsync($"directions/v2/route?key={_key}&from={from}&to={to}&routeType={_routeType[transportType]}&unit=k");
			if(!response.IsSuccessStatusCode) {
				throw new HttpRequestException("Error when sending request!");
			}
			string jsonResponse = await response.Content.ReadAsStringAsync();
			JObject parsedResponse = JObject.Parse(jsonResponse);
			// check if route has been found (since mapquest returns 200 OK, even if no route has been found)
			if(Convert.ToInt32(parsedResponse["info"]["statuscode"].ToString()) != 0) {
				throw new HttpRequestException("Either from or to is not existing!");
			}
			MapQuestInformationResponse informationResponse =
				new MapQuestInformationResponse(Convert.ToDouble(parsedResponse["route"]["distance"].ToString()), parsedResponse["route"]["formattedTime"].ToString(), parsedResponse["route"]["sessionId"].ToString());
			return informationResponse;
		}

		public string GetMap(string session) {
			return $"https://www.mapquestapi.com/staticmap/v5/map?key={_key}&session={session}&size=640,480";
		}
	}
}
