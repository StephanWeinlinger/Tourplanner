using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using Tourplanner.Server.DAL.DAO;

namespace Tourplanner.Server.DAL {
    public static class DalFactory {
	    private static Database _database;
	    private static MapQuest _mapQuest;
	    private static Filesystem _filesystem;

		// create database if it doesn't exist yet and return it
	    public static Database GetDatabase() {
		    if(_database == null) {
				Dictionary<string, string> config =
				    JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("../configServer.json"));
			    _database = new Database(config["PostgresqlConnectionString"]);
		    }

		    return _database;
	    }

	    // create mapquest if it doesn't exist yet and return it
	    public static MapQuest GetMapQuest() {
		    if(_mapQuest == null) {
				Dictionary<string, string> config =
					JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("../configServer.json"));
				_mapQuest = new MapQuest(config["MapQuestAPIKey"]);
			}

		    return _mapQuest;
	    }

	    // create filesystem if it doesn't exist yet and return it
	    public static Filesystem GetFilesystem() {
		    if(_filesystem == null) {
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
