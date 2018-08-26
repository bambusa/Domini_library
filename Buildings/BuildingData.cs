using System.Collections.Generic;
using Domini.Resources;

namespace Domini.Buildings
{
    public enum BuildingType
    {
        Woodjack,
        Stonecutter
    }
    
    public class BuildingData
    {
        public BuildingType BuildingType { get; private set; }
        public string Name { get; private set; }
        public int ResourceTier { get; private set; }
        public Dictionary<ResourceType, long> BuildingCosts => _buildingCosts;
        public Dictionary<ResourceType, long> Input => _input;
        public Dictionary<ResourceType, long> Output => _output;
        
        private Dictionary<ResourceType, long> _buildingCosts;
        private Dictionary<ResourceType, long> _input;
        private Dictionary<ResourceType, long> _output;

        public BuildingData(BuildingType buildingType)
        {
            InitResourceStats(buildingType);
        }

        private void InitResourceStats(BuildingType buildingType)
        {
            _buildingCosts = new Dictionary<ResourceType, long>();
            _input = new Dictionary<ResourceType, long>();
            _output = new Dictionary<ResourceType, long>();
            
            switch (buildingType)
            {
                    case BuildingType.Woodjack:
                        BuildingType = buildingType;
                        Name = "Woodjack";
                        ResourceTier = 1;
                        _buildingCosts.Add(ResourceType.Wood, 100);
                        _output.Add(ResourceType.Wood, 1);
                        break;
                    case BuildingType.Stonecutter:
                        BuildingType = buildingType;
                        Name = "Stonecutter";
                        ResourceTier = 1;
                        _buildingCosts.Add(ResourceType.Stone, 100);
                        _output.Add(ResourceType.Stone, 1);
                        break;
            }
        }
    }
}