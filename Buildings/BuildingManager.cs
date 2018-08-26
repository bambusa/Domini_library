using System;
using System.Collections.Generic;

namespace Domini.Buildings
{
    public class BuildingManager : IGameLoop
    {
        private Dictionary<BuildingType, BuildingData> _buildingData;
        private Dictionary<BuildingType, List<Building>> _buildings;

        public BuildingManager()
        {
            _buildings = new Dictionary<BuildingType, List<Building>>();
            InitBuildingData();
        }

        public void Start()
        {
        }

        /// <summary>
        /// Calculate production (input / output) of all existing buildings
        /// </summary>
        public void Update(float time, float deltaTime, long secondsPassed)
        {
            if (secondsPassed > 0)
            {
                foreach (var buildingsOfType in _buildings.Values)
                {
                    foreach (var building in buildingsOfType)
                    {
                        var input = building.BuildingData.Input;
                        if (secondsPassed > 1)
                        {
                            foreach (var f in input)
                            {
                                input[f.Key] = f.Value * secondsPassed;
                            }
                        }

                        var resourcesAvailable =
                            GameManager.Instance.ResourceManager.ResourceStorage.UseResourcesIfAvailable(input);
                        if (resourcesAvailable)
                        {
                            var output = building.BuildingData.Output;
                            if (secondsPassed > 1)
                            {
                                foreach (var l in output)
                                {
                                    output[l.Key] = l.Value * secondsPassed;
                                }
                            }
                            GameManager.Instance.ResourceManager.ResourceStorage.AddResources(output);
                        }
                    }
                }
            }
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
            if (GameManager.Instance.ResourceManager.ResourceStorage.UseResourcesIfAvailable(resourcesNeeded))
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