using System.Collections.Generic;
using UnityEngine.Video;

namespace Domini
{
    public enum Resource
    {
        Wood,
        Stone
    }
    
    public class ResourceManager : UnityLifecycle
    {
        private Dictionary<Resource, float> _resources;

        public ResourceManager()
        {
            _resources = new Dictionary<Resource, float>();
        }

        public override void Start()
        {
            InitResources();
        }

        private void InitResources()
        {
            DummyResources();
        }

        // TODO: Remove
        private void DummyResources()
        {
            _resources.Add(Resource.Wood, 100);
            _resources.Add(Resource.Stone, 100);
        }

        public bool UseResourcesIfAvailable(Dictionary<Resource, int> resources)
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
                if (_resources.ContainsKey(resource.Key))
                {
                    var before = _resources[resource.Key];
                    var after = before - resource.Value;
                    _resources[resource.Key] = after;
                    GameManager.Instance.Log($"Used {resource.Value} {resource.Key.ToString()}, now having {after}");
                }
            }

            return true;
        }

        public bool CheckResourceAvailability(Resource resource, int amount)
        {
            return _resources.ContainsKey(resource) && _resources[resource] >= amount;
        }
    }
}