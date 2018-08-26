using System.Collections.Generic;

namespace Domini.Resources
{
    public class ResourceStorage
    {
        private Dictionary<ResourceType, long> _resourceStorage;

        public ResourceStorage()
        {
            _resourceStorage = new Dictionary<ResourceType, long>();
        }

        public long GetResourceAmount(ResourceType resourceType)
        {
            if (!_resourceStorage.ContainsKey(resourceType))
                return 0;
            return _resourceStorage[resourceType];
        }

        public bool UseResourcesIfAvailable(Dictionary<ResourceType, long> resources)
        {
            // Check availability
            foreach (var resource in resources)
            {
                if (!CheckResourceAvailability(resource.Key, resource.Value))
                {
                    GameManager.Instance.Log("Not enough resources available");
                    return false;
                }
            }

            // Remove used resources
            foreach (var resource in resources)
            {
                if (_resourceStorage.ContainsKey(resource.Key))
                {
                    var before = _resourceStorage[resource.Key];
                    var after = before - resource.Value;
                    _resourceStorage[resource.Key] = after;
                    GameManager.Instance.Log($"Used {resource.Value} {resource.Key.ToString()}, now having {after}");
                }
            }

            return true;
        }

        public bool CheckResourceAvailability(ResourceType resourceType, long amount)
        {
            return _resourceStorage.ContainsKey(resourceType) &&
                   _resourceStorage[resourceType] >= amount;
        }

        public void AddResources(Dictionary<ResourceType, long> resources)
        {
            foreach (var resource in resources)
            {
                AddResource(resource.Key, resource.Value);
            }
        }

        public void AddResource(ResourceType resourceType, long amount)
        {
            if (!_resourceStorage.ContainsKey(resourceType))
            {
                _resourceStorage.Add(resourceType, amount);
            }
            else
            {
                _resourceStorage[resourceType] = amount;
            }
        }
    }
}