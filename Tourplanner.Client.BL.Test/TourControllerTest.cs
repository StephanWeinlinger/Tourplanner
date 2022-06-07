using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Tourplanner.Client.BL.Controllers;
using Tourplanner.Shared.Model;

namespace Tourplanner.Client.BL.Test {
    public class TestTourController {
	    private TourController _tourController;

        [SetUp]
        public void Setup() {
	        _tourController = new TourController();
        }

        [Test]
        public void TestGetCombinedTours_shouldGetCombinedToursAndResponse() {
			// Act
			var (combinedTours, response) = Task.Run<(List<CombinedTour>, CustomResponse)>(async () => await _tourController.GetCombinedTours()).Result;
			// Assert
			Assert.IsNotNull(combinedTours);
			Assert.IsNotNull(response);
        }

        [Test]
        public void TestInsertTour_shouldGetCombinedTourAndResponse() {
	        // Arrange
	        Tour newTour = new Tour("Test", "Test", "Absdorf", "Tulln", "Car");
	        // Act
	        var (combinedTour, response) = Task.Run<(CombinedTour, CustomResponse)>(async () => await _tourController.InsertTour(newTour)).Result;
	        // Assert
	        Assert.IsNotNull(combinedTour);
	        Assert.IsNotNull(response);
        }

        [Test]
        public void TestUpdateTour_shouldGetCombinedTourAndResponse() {
	        // Arrange
	        Tour newTour = new Tour("Test", "Test", "Absdorf", "Tulln", "Car");
	        var (combinedTourInsert, responseInsert) = Task.Run<(CombinedTour, CustomResponse)>(async () => await _tourController.InsertTour(newTour)).Result;
	        Tour updatedTour = new Tour("Test", "Test", "Absdorf", "Tulln", "Bicycle");
			// Act
			var (combinedTour, response) = Task.Run<(CombinedTour, CustomResponse)>(async () => await _tourController.UpdateTour(combinedTourInsert.Id, updatedTour)).Result;
	        // Assert
	        Assert.IsNotNull(combinedTour);
	        Assert.IsNotNull(response);
        }

        [Test]
        public void TestDeleteTour_shouldGetResponse() {
	        // Arrange
	        Tour newTour = new Tour("Test", "Test", "Absdorf", "Tulln", "Car");
	        var (combinedTourInsert, responseInsert) = Task.Run<(CombinedTour, CustomResponse)>(async () => await _tourController.InsertTour(newTour)).Result;
	        // Act
	        var response = Task.Run<CustomResponse>(async () => await _tourController.DeleteTour(combinedTourInsert.Id)).Result;
	        // Assert
	        Assert.IsNotNull(response);
        }

        [Test]
        public void TestInsertTour_shouldGetBadResponseBecauseOfTransportType() {
	        // Arrange
	        Tour newTour = new Tour("Test", "Test", "Absdorf", "Tulln", "Train");
	        // Act
	        var (combinedTour, response) = Task.Run<(CombinedTour, CustomResponse)>(async () => await _tourController.InsertTour(newTour)).Result;
	        // Assert
	        Assert.False(response.Success);
        }

        [Test]
        public void TestUpdateTour_shouldGetBadResponseBecauseOfTransportType() {
			// Arrange
			Tour newTour = new Tour("Test", "Test", "Absdorf", "Tulln", "Car");
			var (combinedTourInsert, responseInsert) = Task.Run<(CombinedTour, CustomResponse)>(async () => await _tourController.InsertTour(newTour)).Result;
			Tour updatedTour = new Tour("Test", "Test", "Absdorf", "Tulln", "Train");
	        // Act
	        var (combinedTour, response) = Task.Run<(CombinedTour, CustomResponse)>(async () => await _tourController.UpdateTour(combinedTourInsert.Id, updatedTour)).Result;
	        // Assert
	        Assert.False(response.Success);
        }

		[Test]
        public void TestUpdateTour_shouldGetBadResponseBecauseOfId() {
	        // Arrange
	        Tour updatedTour = new Tour("Test", "Test", "Absdorf", "Tulln", "Bicycle");
	        // Act
	        var (combinedTour, response) = Task.Run<(CombinedTour, CustomResponse)>(async () => await _tourController.UpdateTour(-1, updatedTour)).Result;
	        // Assert
	        Assert.False(response.Success);
        }
	}
}