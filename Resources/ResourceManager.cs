using System;
using System.Collections.Generic;

namespace Domini
{
    public class ResourceManager : UnityLifecycle
    {
        private Dictionary<ResourceType, ResourceData> _resourceData;
        private Dictionary<ResourceType, float> _resourceStorage;

        public ResourceManager()
        {
            _resourceStorage = new Dictionary<ResourceType, float>();
        }

        public override void Start()
        {
            InitResources();
        }

        private void InitResources()
        {
            _resourceData = new Dictionary<ResourceType, ResourceData>();
            _resourceData.Add(ResourceType.Wood, new ResourceData(ResourceType.Wood, "Wood"));
            _resourceData.Add(ResourceType.Stone, new ResourceData(ResourceType.Stone, "Stone"));
        }

        public ResourceData GetResourceData(ResourceType resourceType)
        {
            if (!_resourceData.ContainsKey(resourceType))
                throw new Exception("ResourceData not initialized");
            return _resourceData[resourceType];
        }

        public float GetResourceAmount(ResourceType resourceType)
        {
            if (!_resourceStorage.ContainsKey(resourceType))
                return 0;
            return _resourceStorage[resourceType];
        }
        
        public void CheatResources(ResourceType resourceType, float amount)
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

        public bool UseResourcesIfAvailable(Dictionary<ResourceType, int> resources)
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

        public bool CheckResourceAvailability(ResourceType resourceType, int amount)
        {
            return _resourceStorage.ContainsKey(resourceType) &&
                   _resourceStorage[resourceType] >= amount;
        }
    }
}