using System;
using System.Configuration;

namespace Tourplanner.Server.DAL {
    public static class DalFactory {
	    private static IDatabase _database;

	    static DalFactory() {

	    }

		// create database if it doesn't exist yet and return it
	    public static IDatabase GetDatabase() {
		    if(_database == null) {
			    string configuration = ConfigurationManager.AppSettings["PostgresqlConnectionString"];
			    _database = new Database(configuration);
		    }

		    return _database;
	    }
    }
}
