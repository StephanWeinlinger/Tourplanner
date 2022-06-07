using NUnit.Framework;

namespace Tourplanner.Client.BL.Test {
    public class TestBlFactory {

        [Test]
        public void testGetApiHandler_shouldProvideSingleton() {
			// Arrange
			ApiHandler apiHandler1 = BlFactory.GetApiHandler();
			ApiHandler apiHandler2 = BlFactory.GetApiHandler();

			// Assert
            Assert.AreSame(apiHandler1, apiHandler2);
        }

        [Test]
        public void testGetFileExplorer_shouldProvideSingleton() {
	        // Arrange
	        FileExplorer fileExplorer1 = BlFactory.GetFileExplorer();
	        FileExplorer fileExplorer2 = BlFactory.GetFileExplorer();

	        // Assert
	        Assert.AreSame(fileExplorer1, fileExplorer2);
        }
	}
}