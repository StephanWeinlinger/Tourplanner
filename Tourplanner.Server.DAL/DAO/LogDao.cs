using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourplanner.Shared.Model;

namespace Tourplanner.Server.DAL.DAO {
	public class LogDao {
		private const string _sqlGetLogById = "SELECT * FROM \"Log\" WHERE \"Id\" = @Id";
		private const string _sqlGetAllLogsWithTourId = "SELECT * FROM \"Log\" WHERE \"TourId\" = @TourId";
		private const string _sqlInsertLog = "INSERT INTO \"Log\" (\"TourId\", \"Date\", \"Comment\", \"Difficulty\", \"Time\", \"Rating\") VALUES (@TourId, @Date, @Comment, @Difficulty, @Time, @Rating) RETURNING \"Id\"";
		private const string _sqlUpdateLog = "UPDATE \"Log\" SET \"TourId\" = @TourId, \"Date\" = @Date, \"Comment\" = @Comment, \"Difficulty\" = @Difficulty, \"Time\" = @Time, \"Rating\" = @Rating WHERE \"Id\" = @Id RETURNING \"Id\"";
		private const string _sqlDeleteLog = "DELETE FROM \"Log\" WHERE \"Id\" = @Id";

		private Database _database;

		public LogDao() {
			_database = DalFactory.GetDatabase();
		}

		public Log GetLogById(int id) {
			DbCommand command = _database.CreateCommand(_sqlGetLogById);
			_database.DefineParameter(command, "Id", DbType.Int32, id);

			Log log = new Log();
			using(IDataReader reader = _database.ExecuteReader(command)) {
				log = ReadLog(reader);
			}
			return log;
		}

		public List<Log> GetAllLogsWithTourId(int id) {
			DbCommand command = _database.CreateCommand(_sqlGetAllLogsWithTourId);
			_database.DefineParameter(command, "TourId", DbType.Int32, id);

			List<Log> logs = new List<Log>();
			// using using, otherwise reader has to be closed manually
			using(IDataReader reader = _database.ExecuteReader(command)) {
				logs = ReadLogs(reader);
			}
			return logs;
		}

		public Log InsertLog(Log newLog) {
			DbCommand command = _database.CreateCommand(_sqlInsertLog);
			_database.DefineParameter(command, "TourId", DbType.Int32, newLog.TourId);
			_database.DefineParameter(command, "Date", DbType.Date, newLog.Date.Date);
			_database.DefineParameter(command, "Comment", DbType.String, newLog.Comment);
			_database.DefineParameter(command, "Difficulty", DbType.Int32, newLog.Difficulty);
			_database.DefineParameter(command, "Time", DbType.String, newLog.Time);
			_database.DefineParameter(command, "Rating", DbType.Int32, newLog.Rating);

			Log log = GetLogById(_database.ExecuteScalar(command));
			return log;
		}

		// also able to update the id
		public Log UpdateLog(int id, Log updatedLog) {
			DbCommand command = _database.CreateCommand(_sqlUpdateLog);
			_database.DefineParameter(command, "TourId", DbType.Int32, updatedLog.TourId);
			_database.DefineParameter(command, "Date", DbType.DateTime, updatedLog.Date.Date);
			_database.DefineParameter(command, "Comment", DbType.String, updatedLog.Comment);
			_database.DefineParameter(command, "Difficulty", DbType.Int32, updatedLog.Difficulty);
			_database.DefineParameter(command, "Time", DbType.String, updatedLog.Time);
			_database.DefineParameter(command, "Rating", DbType.Int32, updatedLog.Rating);
			_database.DefineParameter(command, "Id", DbType.Int32, id);

			Log log = GetLogById(_database.ExecuteScalar(command));
			return log;
		}

		public bool DeleteLog(int id) {
			DbCommand command = _database.CreateCommand(_sqlDeleteLog);
			_database.DefineParameter(command, "Id", DbType.Int32, id);
			_database.ExecuteScalar(command);
			return true;
		}

		private Log ReadLog(IDataReader reader) {
			Log log = new Log();
			if(reader != null) {
				while(reader.Read()) {
					log = Read(reader);
				}
			}
			return log;
		}

		private List<Log> ReadLogs(IDataReader reader) {
			List<Log> logs = new List<Log>();
			if(reader != null) {
				while(reader.Read()) {
					logs.Add(Read(reader));
				}
			}
			return logs;
		}

		private Log Read(IDataReader reader) {
			return new Log(
				(int) reader["Id"],
				(int) reader["TourId"],
				DateTime.Parse(reader["Date"].ToString()),
				(string) reader["Comment"],
				(int) reader["Difficulty"],
				(string) reader["Time"],
				(int) reader["Rating"]
			);
		}
	}
}
