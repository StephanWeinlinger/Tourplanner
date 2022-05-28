using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Tourplanner.Shared.Model;

namespace Tourplanner.Server.DAL.DAO {
	public class TourDao {
		private const string _sqlGetTourById =
			"SELECT t.\"Id\", t.\"Name\", t.\"Description\", t.\"From\", t.\"To\", tt.\"Type\" as \"TransportType\", t.\"Distance\", t.\"Time\" FROM \"Tour\" t JOIN \"TransportType\" tt ON t.\"TransportType\" = tt.\"Id\" WHERE t.\"Id\" = @Id";
		private const string _sqlGetAllTours = "SELECT t.\"Id\", t.\"Name\", t.\"Description\", t.\"From\", t.\"To\", tt.\"Type\" as \"TransportType\", t.\"Distance\", t.\"Time\" FROM \"Tour\" t JOIN \"TransportType\" tt ON t.\"TransportType\" = tt.\"Id\"";
		private const string _sqlGetAllToursWithFilter = "SELECT t.\"Id\", t.\"Name\", t.\"Description\", t.\"From\", t.\"To\", tt.\"Type\" as \"TransportType\", t.\"Distance\", t.\"Time\" FROM \"Tour\" t JOIN \"TransportType\" tt ON t.\"TransportType\" = tt.\"Id\" WHERE LOWER(t.\"Name\") LIKE @Filter OR LOWER(t.\"Description\") LIKE @Filter OR EXISTS(SELECT * FROM \"Log\" l WHERE l.\"TourId\" = t.\"Id\" AND LOWER(l.\"Comment\") LIKE @Filter)";
		private const string _sqlInsertTour = "INSERT INTO \"Tour\" (\"Name\", \"Description\", \"From\", \"To\", \"TransportType\", \"Distance\", \"Time\") VALUES (@Name, @Description, @From, @To, @TransportType, @Distance, @Time) RETURNING \"Id\"";
		private const string _sqlUpdateTour = "UPDATE \"Tour\" SET \"Name\" = @Name, \"Description\" = @Description, \"From\" = @From, \"To\" = @To, \"TransportType\" = @TransportType, \"Distance\" = @Distance, \"Time\" = @Time WHERE \"Id\" = @Id RETURNING \"Id\"";
		private const string _sqlDeleteTour = "DELETE FROM \"Tour\" WHERE \"Id\" = @Id";
		private const string _sqlDeleteAllTours = "DELETE FROM \"Tour\"";

		private Database _database;
		private Dictionary<string, int> _transportType;

		public TourDao() {
			_database = DalFactory.GetDatabase();
			_transportType = new Dictionary<string, int> {
				{ "Car", 1 },
				{ "Bicycle", 2 },
				{ "Walk", 3 }
			};
		}

		public Tour GetTourById(int id) {
			DbCommand command = _database.CreateCommand(_sqlGetTourById);
			_database.DefineParameter(command, "Id", DbType.Int32, id);

			Tour tour = new Tour();
			using(IDataReader reader = _database.ExecuteReader(command)) {
				tour = ReadTour(reader);
			}
			return tour;
		}

		public List<Tour> GetAllTours() {
			DbCommand command = _database.CreateCommand(_sqlGetAllTours);

			List<Tour> tours = new List<Tour>();
			// using using, otherwise reader has to be closed manually
			using(IDataReader reader = _database.ExecuteReader(command)) {
				tours = ReadTours(reader);
			}
			return tours;
		}

		public List<Tour> GetAllToursWithFilter(string filter) {
			DbCommand command = _database.CreateCommand(_sqlGetAllToursWithFilter);
			_database.DefineParameter(command, "Filter", DbType.String, $"%{filter}%");

			List<Tour> tours = new List<Tour>();
			using(IDataReader reader = _database.ExecuteReader(command)) {
				tours = ReadTours(reader);
			}
			return tours;
		}

		public Tour InsertTour(Tour newTour) {
			DbCommand command = _database.CreateCommand(_sqlInsertTour);
			_database.DefineParameter(command, "Name", DbType.String, newTour.Name);
			_database.DefineParameter(command, "Description", DbType.String, newTour.Description);
			_database.DefineParameter(command, "From", DbType.String, newTour.From);
			_database.DefineParameter(command, "To", DbType.String, newTour.To);
			_database.DefineParameter(command, "TransportType", DbType.Int32, _transportType[newTour.TransportType]);
			_database.DefineParameter(command, "Distance", DbType.Double, newTour.Distance);
			_database.DefineParameter(command, "Time", DbType.String, newTour.Time);

			Tour tour = GetTourById(_database.ExecuteScalar(command));
			return tour;
		}

		// also able to update the id
		public Tour UpdateTour(int id, Tour updatedTour) {
			DbCommand command = _database.CreateCommand(_sqlUpdateTour);
			_database.DefineParameter(command, "Name", DbType.String, updatedTour.Name);
			_database.DefineParameter(command, "Description", DbType.String, updatedTour.Description);
			_database.DefineParameter(command, "From", DbType.String, updatedTour.From);
			_database.DefineParameter(command, "To", DbType.String, updatedTour.To);
			_database.DefineParameter(command, "TransportType", DbType.Int32, _transportType[updatedTour.TransportType]);
			_database.DefineParameter(command, "Distance", DbType.Double, updatedTour.Distance);
			_database.DefineParameter(command, "Time", DbType.String, updatedTour.Time);
			_database.DefineParameter(command, "Id", DbType.Int32, id);

			Tour tour = GetTourById(_database.ExecuteScalar(command));
			return tour;
		}

		public bool DeleteTour(int id) {
			DbCommand command = _database.CreateCommand(_sqlDeleteTour);
			_database.DefineParameter(command, "Id", DbType.Int32, id);
			_database.ExecuteScalar(command);
			// TODO maybe try out what ExecuteScalar returns if nothing gets returned
			return true;
		}

		// used if import was successful (mapquest part)
		public bool DeleteAllTours() {
			DbCommand command = _database.CreateCommand(_sqlDeleteAllTours);
			_database.ExecuteScalar(command);
			// TODO maybe try out what ExecuteScalar returns if nothing gets returned
			return true;
		}

		private Tour ReadTour(IDataReader reader) {
			Tour tour = new Tour();
			if(reader != null) {
				while(reader.Read()) {
					tour = Read(reader);
				}
			}
			return tour;
		}

		private List<Tour> ReadTours(IDataReader reader) {
			List<Tour> tours = new List<Tour>();
			if(reader != null) {
				while(reader.Read()) {
					tours.Add(Read(reader));
				}
			}
			return tours;
		}

		private Tour Read(IDataReader reader) {
			return new Tour(
				(int) reader["Id"],
				(string) reader["Name"],
				(string) reader["Description"],
				(string) reader["From"],
				(string) reader["To"],
				(string) reader["TransportType"],
				(double) reader["Distance"],
				(string) reader["Time"]
			);
		}

	}
}
