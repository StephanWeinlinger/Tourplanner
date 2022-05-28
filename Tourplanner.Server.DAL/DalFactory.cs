using System;
using System.Configuration;
using Tourplanner.Server.DAL.DAO;

namespace Tourplanner.Server.DAL {
    public static class DalFactory {
	    private static Database _database;
	    private static MapQuest _mapQuest;
	    private static Filesystem _filesystem;

		// create database if it doesn't exist yet and return it
	    public static Database GetDatabase() {
		    if(_database == null) {
			    string connectionString = ConfigurationManager.ConnectionStrings["PostgresqlConnectionString"].ConnectionString;
			    _database = new Database(connectionString);
		    }

		    return _database;
	    }

	    // create mapquest if it doesn't exist yet and return it
	    public static MapQuest GetMapQuest() {
		    if(_mapQuest == null) {
			    string key = ConfigurationManager.ConnectionStrings["MapQuestAPIKey"].ConnectionString;
			    _mapQuest = new MapQuest(key);
		    }

		    return _mapQuest;
	    }

	    // create filesystem if it doesn't exist yet and return it
	    public static Filesystem GetFilesystem() {
		    if(_filesystem == null) {
			    string key = ConfigurationManager.ConnectionStrings["MapQuestAPIKey"].ConnectionString;
			    _filesystem = new Filesystem();
		    }

		    return _filesystem;
	    }

		public static TourDao CreateTourDao() {
		    return new TourDao();
	    }

	    public static LogDao CreateLogDao() {
		    return new LogDao();
	    }
	}
}
