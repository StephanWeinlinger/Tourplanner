using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourplanner.Shared.Model {
	public class Log {
		public Log() { }
		public Log(DateTime date, string comment, int difficulty, string time, int rating) {
			Date = date;
			Comment = comment;
			Difficulty = difficulty;
			Time = time;
			Rating = rating;
		}
		public Log(int tourId, DateTime date, string comment, int difficulty, string time, int rating) {
			TourId = tourId;
			Date = date;
			Comment = comment;
			Difficulty = difficulty;
			Time = time;
			Rating = rating;
		}
		public Log(int id, int tourId, DateTime date, string comment, int difficulty, string time, int rating) {
			Id = id;
			TourId = tourId;
			Date = date;
			Comment = comment;
			Difficulty = difficulty;
			Time = time;
			Rating = rating;
		}
		public int Id { get; set; }
		// TODO maybe fix
		// not required when importing
		public int TourId { get; set; }
		[Required]
		public DateTime Date { get; set; }
		[Required]
		public string Comment { get; set; }
		[Required]
		public int Difficulty { get; set; }
		[Required]
		public string Time { get; set; }
		[Required]
		public int Rating { get; set; }
	}
}
