using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

// TODO check if the exception throws are really necessary here, or can be handled in the BL
namespace Tourplanner.Server.DAL {
	public class Database {
		private string _connectionString;

		public Database(string connectionString) {
			_connectionString = connectionString;
		}

		// create new sql command
		public DbCommand CreateCommand(string genericCommandText) {
			return new NpgsqlCommand(genericCommandText);
		}

		// declare parameter (without setting value)
		public int DeclareParameter(DbCommand command, string name, DbType type) {
			if(!command.Parameters.Contains(name)) {
				int index = command.Parameters.Add(new NpgsqlParameter(name, type));
				return index;
			}
			throw new ArgumentException(string.Format("Parameter {0} already exists.", name));
		}

		// declare and set parameter (combines DeclareParemter and SetParameter)
		public void DefineParameter(DbCommand command, string name, DbType type, object value) {
			int index = DeclareParameter(command, name, type);
			command.Parameters[index].Value = value;
		}

		// open connection to sql server
		private DbConnection CreateOpenConnection() {
			DbConnection connection = new NpgsqlConnection(this._connectionString);
			connection.Open();

			return connection;
		}

		// execute command, return datareader and close connection
		public IDataReader ExecuteReader(DbCommand command) {
			command.Connection = CreateOpenConnection();
			return command.ExecuteReader(CommandBehavior.CloseConnection);
		}

		// execute command and close connection
		public int ExecuteScalar(DbCommand command) {
			command.Connection = CreateOpenConnection();
			return Convert.ToInt32(command.ExecuteScalar());
		}
	}
}
