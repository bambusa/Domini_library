using System;
using Domini;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class ResourceManagerTests
    {
        [Test]
        public void ResourceManager()
        {
            var gameManager = TestUtils.InitGameManager();
            foreach (var resourceType in (ResourceType[]) Enum.GetValues(typeof(ResourceType)))
            {
                Assert.DoesNotThrow(() => 
                    Assert.NotNull(gameManager.ResourceManager.GetResourceData(resourceType))
                );
            }
        }

        [Test]
        public void ResourceManagerCheats()
        {
            var gameManager = TestUtils.InitGameManager();
            TestUtils.CheatFullResourceStorage();
            foreach (var resourceType in (ResourceType[]) Enum.GetValues(typeof(ResourceType)))
            {
                var amount = gameManager.ResourceManager.GetResourceAmount(resourceType);
                Assert.That(amount, Is.EqualTo(TestUtils.CheatFullResourceAmount));
            }
            
            TestUtils.CheatEmptyResourceStorage();
            foreach (var resourceType in (ResourceType[]) Enum.GetValues(typeof(ResourceType)))
            {
                var amount = gameManager.ResourceManager.GetResourceAmount(resourceType);
                Assert.That(amount, Is.EqualTo(0));
            }
        }
    }
}