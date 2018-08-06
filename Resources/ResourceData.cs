namespace Domini
{
    public enum ResourceType
    {
        Wood,
        Stone
    }
    
    public class ResourceData
    {
        public ResourceType ResourceType { get; private set; }
        public string Name { get; private set; }

        public ResourceData(ResourceType resourceType, string name)
        {
            ResourceType = resourceType;
            Name = name;
        }
    }
}