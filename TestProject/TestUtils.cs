using System;
using Domini;
using Domini.Buildings;

namespace TestProject
{
    public static class TestUtils
    {
        public static readonly float CheatFullResourceAmount = 9999;
            
        public static GameManager InitGameManager()
        {
            var gameManager = GameManager.Instance;
            gameManager.Start();
            return gameManager;
        }

        public static void CheatEmptyResourceStorage()
        {
            foreach (var resourceType in (ResourceType[]) Enum.GetValues(typeof(ResourceType)))
            {
                GameManager.Instance.ResourceManager.CheatResources(resourceType, 0);
            }
        }

        public static void CheatFullResourceStorage()
        {
            foreach (var resourceType in (ResourceType[]) Enum.GetValues(typeof(ResourceType)))
            {
                GameManager.Instance.ResourceManager.CheatResources(resourceType, CheatFullResourceAmount);
            }
        }

        public static void CheatNoBuildings()
        {
            foreach (var buildingType in (BuildingType[]) Enum.GetValues(typeof(BuildingType)))
            {
                GameManager.Instance.BuildingManager.CheatBuilding(buildingType, 0);
            }
        }

        public static void CheatAllBuildings()
        {
            foreach (var buildingType in (BuildingType[]) Enum.GetValues(typeof(BuildingType)))
            {
                GameManager.Instance.BuildingManager.CheatBuilding(buildingType, 1);
            }
        }
    }
}