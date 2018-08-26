using System;
using Domini.Buildings;
using Domini.Resources;
using NUnit.Framework;

namespace TestProject {
    [TestFixture]
    public class BuildingManagerTests {
        [Test]
        public void BuildingManager()
        {
            var gameManager = TestUtils.InitGameManager();
            foreach (var buildingType in (BuildingType[]) Enum.GetValues(typeof(BuildingType)))
            {
                Assert.DoesNotThrow(() => 
                    Assert.NotNull(gameManager.BuildingManager.GetBuildingData(buildingType))
                );
            }
        }

        [Test]
        public void BuildingManagerCheats()
        {
            var gameManager = TestUtils.InitGameManager();
            TestUtils.CheatAllBuildings();
            foreach (var buildingType in (BuildingType[]) Enum.GetValues(typeof(BuildingType)))
            {
                var amount = gameManager.BuildingManager.GetBuildingCount(buildingType);
                Assert.That(amount, Is.EqualTo(1));
            }
            
            TestUtils.CheatNoBuildings();
            foreach (var buildingType in (BuildingType[]) Enum.GetValues(typeof(BuildingType)))
            {
                var amount = gameManager.BuildingManager.GetBuildingCount(buildingType);
                Assert.That(amount, Is.EqualTo(0));
            }
        }
        
        [Test]
        public void Build() {
            var gameManager = TestUtils.InitGameManager();
            
            TestUtils.CheatEmptyResourceStorage();
            foreach (var buildingType in (BuildingType[]) Enum.GetValues(typeof(BuildingType)))
            {
                var before = gameManager.BuildingManager.GetBuildingCount(buildingType);
                Assert.That(before, Is.EqualTo(0));
                gameManager.BuildingManager.Build(buildingType);
                var after = gameManager.BuildingManager.GetBuildingCount(buildingType);
                Assert.That(after, Is.EqualTo(0));
            }

            TestUtils.CheatFullResourceStorage();
            TestUtils.CheatNoBuildings();
            foreach (var buildingType in (BuildingType[]) Enum.GetValues(typeof(BuildingType)))
            {
                var before = gameManager.BuildingManager.GetBuildingCount(buildingType);
                Assert.That(before, Is.EqualTo(0));
                gameManager.BuildingManager.Build(buildingType);
                var after = gameManager.BuildingManager.GetBuildingCount(buildingType);
                Assert.That(after, Is.EqualTo(1));
            }
        }

        [Test]
        public void ResourceProduction()
        {
            var gameManager = TestUtils.InitGameManager();
            TestUtils.CheatEmptyResourceStorage();
            Assert.That(gameManager.ResourceManager.ResourceStorage.GetResourceAmount(ResourceType.Stone), Is.EqualTo(0));
            Assert.That(gameManager.ResourceManager.ResourceStorage.GetResourceAmount(ResourceType.Wood), Is.EqualTo(0));
            
            gameManager.BuildingManager.CheatBuilding(BuildingType.Stonecutter, 1);
            gameManager.BuildingManager.CheatBuilding(BuildingType.Woodjack, 1);
            gameManager.BuildingManager.Update(1, 1, 1);
            Assert.That(gameManager.ResourceManager.ResourceStorage.GetResourceAmount(ResourceType.Stone), Is.GreaterThan(0));
            Assert.That(gameManager.ResourceManager.ResourceStorage.GetResourceAmount(ResourceType.Wood), Is.GreaterThan(0));
        }
    }
}