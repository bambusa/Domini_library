using System;
using System.Collections.Generic;

namespace Domini.Buildings
{
    public class BuildingManager
    {
        private Dictionary<BuildingType, BuildingData> _buildingData;
        private Dictionary<BuildingType, List<Building>> _buildings;

        public BuildingManager()
        {
            _buildings = new Dictionary<BuildingType, List<Building>>();
            InitBuildingData();
        }

        private void InitBuildingData()
        {
            _buildingData = new Dictionary<BuildingType, BuildingData>();
            _buildingData.Add(BuildingType.Woodjack, new BuildingData(BuildingType.Woodjack));
            _buildingData.Add(BuildingType.Stonecutter, new BuildingData(BuildingType.Stonecutter));
        }

        public BuildingData GetBuildingData(BuildingType buildingType)
        {
            if (!_buildingData.ContainsKey(buildingType))
                throw new Exception("BuildingData not initialized");
            return _buildingData[buildingType];
        }

        public void CheatBuilding(BuildingType buildingType, int amount)
        {
            if (!_buildings.ContainsKey(buildingType))
                _buildings.Add(buildingType, new List<Building>());
            else
                _buildings[buildingType] = new List<Building>();
                
            for (int i = 0; i < amount; i++)
            {
                _buildings[buildingType].Add(new Building(_buildingData[buildingType]));
            }
        }

        public void Build(BuildingType buildingType)
        {
            var buildingData = _buildingData[buildingType];
            var resourcesNeeded = buildingData.BuildingCosts;
            if (GameManager.Instance.ResourceManager.UseResourcesIfAvailable(resourcesNeeded))
            {
                AddBuilding(buildingType);
                GameManager.Instance.Log($"Built new {buildingData.Name}, now having {GetBuildingCount(buildingType)}");
            }
            else
            {
                GameManager.Instance.Log("Not enough resources");
            }
        }

        private void AddBuilding(BuildingType buildingType)
        {
            if (!_buildings.ContainsKey(buildingType))
            {
                _buildings.Add(buildingType, new List<Building>());
            }

            _buildings[buildingType].Add(new Building(_buildingData[buildingType]));
        }

        public int GetBuildingCount(BuildingType buildingType)
        {
            if (!_buildings.ContainsKey(buildingType))
                return 0;
            return _buildings[buildingType].Count;
        }
    }
}