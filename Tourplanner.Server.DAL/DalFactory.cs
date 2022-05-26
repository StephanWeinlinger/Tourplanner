using System;
using System.Configuration;
using Tourplanner.Server.DAL.DAO;

namespace Tourplanner.Server.DAL {
    public static class DalFactory {
	    private static IDatabase _database;

		// create database if it doesn't exist yet and return it
	    public static IDatabase GetDatabase() {
		    if(_database == null) {
			    string connectionString = ConfigurationManager.ConnectionStrings["PostgresqlConnectionString"].ConnectionString;
			    _database = new Database(connectionString);
		    }

		    return _database;
	    }

	    public static TourDao CreateTourDao() {
		    return new TourDao();
	    }

	    public static LogDao CreateLogDao() {
		    return new LogDao();
	    }
	}
}
