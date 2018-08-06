using System.Collections.Generic;

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
        public Dictionary<ResourceType, int> BuildingCosts => _buildingCosts;
        public Dictionary<ResourceType, float> Input => _input;
        public Dictionary<ResourceType, float> Output => _output;
        
        private Dictionary<ResourceType, int> _buildingCosts;
        private Dictionary<ResourceType, float> _input;
        private Dictionary<ResourceType, float> _output;

        public BuildingData(BuildingType buildingType)
        {
            InitResource(buildingType);
        }

        private void InitResource(BuildingType buildingType)
        {
            _buildingCosts = new Dictionary<ResourceType, int>();
            _input = new Dictionary<ResourceType, float>();
            _output = new Dictionary<ResourceType, float>();
            
            switch (buildingType)
            {
                    case BuildingType.Woodjack:
                        BuildingType = buildingType;
                        Name = "Woodjack";
                        _buildingCosts.Add(ResourceType.Wood, 100);
                        _output.Add(ResourceType.Wood, 1);
                        break;
                    case BuildingType.Stonecutter:
                        BuildingType = buildingType;
                        Name = "Stonecutter";
                        _buildingCosts.Add(ResourceType.Stone, 100);
                        _output.Add(ResourceType.Stone, 1);
                        break;
            }
        }
    }
}