using Domini;
using NUnit.Framework;

namespace TestProject {
    [TestFixture]
    public class GameManagerTests {
        [Test]
        public void UnitTestDetected() {
            Assert.IsTrue(UnitTestDetector.IsInUnitTest);
        }
        
        [Test]
        public void GameManager() {
            var gameManager = TestUtils.InitGameManager();
            Assert.IsNotNull(gameManager);
            Assert.IsNotNull(gameManager.ResourceManager);
            Assert.IsNotNull(gameManager.BuildingManager);
        }
        
        [Test]
        public void GameManagerUpdate(){
            var gameManager = TestUtils.InitGameManager();
            Assert.DoesNotThrow(gameManager.Update);
        }
    }
}