using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.BL.Test {
    public class TestLogController {
	    private LogController _logController;
	    private TourController _tourController;
	    private int _tourId;

        [SetUp]
        public void Setup() {
	        _logController = new LogController();
	        _tourController = new TourController();
	        Tour newTour = new Tour("Test", "Test", "Absdorf", "Tulln", "Car");
	        var (combinedTour, response) = Task.Run<(CombinedTour, CustomResponse)>(async () => await _tourController.InsertTour(newTour)).Result;
	        _tourId = combinedTour.Id;
        }

		// tour delete should delete log

        [Test]
        public void TestInsertLog_shouldGetLogAndResponse() {
	        // Arrange
	        Log newLog = new Log(_tourId, DateTime.Now, "Test", 3, "20:20:20", 3);
	        // Act
	        var (log, response) = Task.Run<(Log, CustomResponse)>(async () => await _logController.InsertLog(newLog)).Result;
	        // Assert
	        Assert.IsNotNull(log);
	        Assert.IsNotNull(response);
		}

        [Test]
        public void TestUpdateLog_shouldGetLogAndResponse() {
			// Arrange
			Log newLog = new Log(_tourId, DateTime.Now, "Test", 3, "20:20:20", 3);
			var (logInsert, responseInsert) = Task.Run<(Log, CustomResponse)>(async () => await _logController.InsertLog(newLog)).Result;
			Log updatedLog = new Log(_tourId, DateTime.Now, "Test", 3, "20:20:20", 3);
			// Act
			var (log, response) = Task.Run<(Log, CustomResponse)>(async () => await _logController.UpdateLog(logInsert.Id, updatedLog)).Result;
	        // Assert
	        Assert.IsNotNull(log);
	        Assert.IsNotNull(response);
        }

        [Test]
        public void TestDeleteLog_shouldGetResponse() {
			// Arrange
			Log newLog = new Log(_tourId, DateTime.Now, "Test", 3, "20:20:20", 3);
			var (logInsert, responseInsert) = Task.Run<(Log, CustomResponse)>(async () => await _logController.InsertLog(newLog)).Result;
			// Act
			var response = Task.Run<CustomResponse>(async () => await _logController.DeleteLog(logInsert.Id)).Result;
			// Assert
			Assert.IsNotNull(response);
		}

        [Test]
        public void TestInsertLog_shouldGetBadResponseBecauseOfDifficulty() {
			// Arrange
			Log newLog = new Log(_tourId, DateTime.Now, "Test", -1, "20:20:20", 3);
			// Act
			var (log, response) = Task.Run<(Log, CustomResponse)>(async () => await _logController.InsertLog(newLog)).Result;
			// Assert
			Assert.False(response.Success);
		}

        [Test]
        public void TestInsertLog_shouldGetBadResponseBecauseOfRating() {
	        // Arrange
	        Log newLog = new Log(_tourId, DateTime.Now, "Test", 3, "20:20:20", -1);
	        // Act
	        var (log, response) = Task.Run<(Log, CustomResponse)>(async () => await _logController.InsertLog(newLog)).Result;
	        // Assert
	        Assert.False(response.Success);
        }


        [Test]
        public void TestInsertLog_shouldGetBadResponseBecauseOfTourId() {
			// Arrange
			Log newLog = new Log(-1, DateTime.Now, "Test", 3, "20:20:20", 3);
			// Act
			var (log, response) = Task.Run<(Log, CustomResponse)>(async () => await _logController.InsertLog(newLog)).Result;
			// Assert
			Assert.False(response.Success);
		}

		[Test]
        public void TestUpdateLog_shouldGetBadResponseBecauseOfDifficulty() {
	        // Arrange
	        Log newLog = new Log(_tourId, DateTime.Now, "Test", 3, "20:20:20", 3);
	        var (logInsert, responseInsert) = Task.Run<(Log, CustomResponse)>(async () => await _logController.InsertLog(newLog)).Result;
	        Log updatedLog = new Log(_tourId, DateTime.Now, "Test", -1, "20:20:20", 3);
	        // Act
	        var (log, response) = Task.Run<(Log, CustomResponse)>(async () => await _logController.UpdateLog(logInsert.Id, updatedLog)).Result;
			// Assert
			Assert.False(response.Success);
		}

        [Test]
        public void TestUpdateLog_shouldGetBadResponseBecauseOfRating() {
	        // Arrange
	        Log newLog = new Log(_tourId, DateTime.Now, "Test", 3, "20:20:20", 3);
	        var (logInsert, responseInsert) = Task.Run<(Log, CustomResponse)>(async () => await _logController.InsertLog(newLog)).Result;
	        Log updatedLog = new Log(_tourId, DateTime.Now, "Test", 3, "20:20:20", -1);
	        // Act
	        var (log, response) = Task.Run<(Log, CustomResponse)>(async () => await _logController.UpdateLog(logInsert.Id, updatedLog)).Result;
	        // Assert
	        Assert.False(response.Success);
        }

        [Test]
        public void TestUpdateLog_shouldGetBadResponseBecauseOfId() {
	        // Arrange
	        Log updatedLog = new Log(_tourId, DateTime.Now, "Test", 3, "20:20:20", 3);
	        // Act
	        var (log, response) = Task.Run<(Log, CustomResponse)>(async () => await _logController.UpdateLog(-1, updatedLog)).Result;
	        // Assert
	        Assert.False(response.Success);
        }

        [Test]
        public void TestUpdateLog_shouldGetBadResponseBecauseOfTourId() {
			// Arrange
			Log newLog = new Log(_tourId, DateTime.Now, "Test", 3, "20:20:20", 3);
			var (logInsert, responseInsert) = Task.Run<(Log, CustomResponse)>(async () => await _logController.InsertLog(newLog)).Result;
			Log updatedLog = new Log(-1, DateTime.Now, "Test", 3, "20:20:20", 3);
	        // Act
	        var (log, response) = Task.Run<(Log, CustomResponse)>(async () => await _logController.UpdateLog(logInsert.Id, updatedLog)).Result;
	        // Assert
	        Assert.False(response.Success);
        }
	}
}