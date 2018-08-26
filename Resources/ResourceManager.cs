using System;
using System.Collections.Generic;

namespace Domini.Resources
{
    public class ResourceManager : IGameLoop
    {
        public ResourceStorage ResourceStorage;

        private Dictionary<ResourceType, ResourceData> _resourceData;

        public void Start()
        {
            InitResourceData();
            ResourceStorage = new ResourceStorage();
        }

        public void Update(float time, float deltaTime, long secondsPassed) { }

        private void InitResourceData()
        {
            _resourceData = new Dictionary<ResourceType, ResourceData>();
            foreach (var resourceType in (ResourceType[]) Enum.GetValues(typeof(ResourceType)))
            {
                _resourceData.Add(resourceType, new ResourceData(ResourceType.Wood, resourceType.ToString()));
            }
        }

        public ResourceData GetResourceData(ResourceType resourceType)
        {
            if (!_resourceData.ContainsKey(resourceType))
                throw new Exception("ResourceData not initialized");
            return _resourceData[resourceType];
        }
    }
}