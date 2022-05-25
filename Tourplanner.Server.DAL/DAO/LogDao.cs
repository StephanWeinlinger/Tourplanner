using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourplanner.Shared.Model;

namespace Tourplanner.Server.DAL.DAO {
	public class LogDao {
		private const string _sqlGetAllLogsWithTourId = "SELECT * FROM \"Log\" WHERE \"TourId\" = @TourId";
		private const string _sqlInsertLogWithTourId = "INSERT INTO \"Log\" (\"TourId\", \"Date\", \"Comment\", \"Difficulty\", \"Time\", \"Rating\") VALUES (@TourId, @Date, @Comment, @Difficulty, @Time, @Rating)";
		private const string _sqlUpdateLog = "UPDATE \"Log\" SET \"TourId\" = @TourId, \"Date\" = @Date, \"Comment\" = @Comment, \"Difficulty\" = @Difficulty, \"Time\" = @Time, \"Rating\" = @Rating WHERE \"Id\" = @Id";
		private const string _sqlDeleteLog = "DELETE FROM \"Log\" WHERE \"Id\" = @Id";

		public IEnumerable<Log> GetAllLogsWithTourId() {
			return null;
		}

		public Tour InsertLogWithTourId() {
			return null;
		}

		public Tour UpdateLog() {
			return null;
		}

		public int DeleteLog() {
			return 0;
		}
	}
}
