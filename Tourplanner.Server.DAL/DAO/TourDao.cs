using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourplanner.Shared.Model;

namespace Tourplanner.Server.DAL.DAO {
	public class TourDao {
		private const string _sqlGetAllTours = "SELECT * FROM \"Tour\"";
		private const string _sqlInsertTour = "INSERT INTO \"Tour\" (\"Name\", \"Description\", \"From\", \"To\", \"TransportType\", \"Distance\", \"Time\") VALUES (@Name, @Description, @From, @To, @TransportType, @Distance, @Time)";
		private const string _sqlUpdateTour = "UPDATE \"Tour\" SET \"Name\" = @Name, \"Description\" = @Description, \"From\" = @From, \"To\" = @To, \"TranportType\" = @TransportType, \"Distance\" = @Distance, \"Time\" = @Time WHERE \"Id\" = @Id";
		private const string _sqlDeleteTour = "DELETE FROM \"Tour\" WHERE \"Id\" = @Id";
		private const string _sqlDeleteAllTours = "DELETE FROM \"Tour\"";

		public IEnumerable<Tour> GetAllTours() {
			return null;
		}

		public Tour InsertTour() {
			return null;
		}

		public Tour UpdateTour() {
			return null;
		}

		public int DeleteTour() {
			return 0;
		}

		// used if import was successful (mapquest part)
		public int DeleteAllTours() {
			return 0;
		}

	}
}
