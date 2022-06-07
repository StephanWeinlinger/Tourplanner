using NUnit.Framework;

namespace Tourplanner.Server.DAL.Test {
    public class Tests {

		[Test]
		public void testGetFilesystem_shouldProvideSingleton() {
			// Arrange
			Filesystem filesystem1 = DalFactory.GetFilesystem();
			Filesystem filesystem2 = DalFactory.GetFilesystem();

			// Assert
			Assert.AreSame(filesystem1, filesystem2);
		}
	}
}