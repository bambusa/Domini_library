using System.Collections.Generic;

namespace Domini.Buildings
{
    public enum Building
    {
        Woodjack,
        Stonecutter
    }
    
    public class BuildingManager
    {
        private Dictionary<Building, int> _buildings;

        public BuildingManager()
        {
            _buildings = new Dictionary<Building, int>();
            DummyBuldings();
        }

        // TODO: Remove
        private void DummyBuldings()
        {
            _buildings.Add(Building.Woodjack, 2);
            _buildings.Add(Building.Stonecutter, 1);
        }

        public void Build(Building building)
        {
            switch (building)
            {
                    case Building.Woodjack:
                        var resourcesNeeded = new Dictionary<Resource, int>();
                        resourcesNeeded.Add(Resource.Wood, 100);
                        if (GameManager.Instance.ResourceManager.UseResourcesIfAvailable(resourcesNeeded))
                        {
                            AddBuilding(Building.Woodjack);
                            GameManager.Instance.Log($"Built new Woodjack, now having {_buildings[Building.Woodjack]}");
                        }
                        else
                        {
                            GameManager.Instance.Log("Not enough resources");
                        }
                        break;
            }
        }

        private void AddBuilding(Building building)
        {
            if (_buildings.ContainsKey(building))
            {
                var before = _buildings[building];
                var after = before += 1;
                _buildings[building] = after;
            }
            else
            {
                _buildings.Add(building, 1);
            }
        }
    }
}