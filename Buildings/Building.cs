namespace Domini.Buildings
{
    public class Building
    {
        public BuildingData BuildingData { get; private set; }

        public Building(BuildingData buildingData)
        {
            BuildingData = buildingData;
        }
    }
}