using System;
using System.Collections.Generic;
using Domini.Buildings;
using UnityEngine;

namespace Domini
{
    public class GameManager
    {
        private static readonly GameManager _instance = new GameManager();

        private List<UnityLifecycle> _lifecycles;

        public ResourceManager ResourceManager;
        public BuildingManager BuildingManager;
        
        static GameManager(){}
        private GameManager(){}

        public static GameManager Instance => _instance;

        public void Start()
        {
            _lifecycles = new List<UnityLifecycle>();
            InitManager();
            InitLifecycles();
            for (int i = 0; i < _lifecycles.Count; i++)
            {
                _lifecycles[i].Start();
            }
        }

        private void InitManager()
        {
            ResourceManager = new ResourceManager();
            BuildingManager = new BuildingManager();
        }

        private void InitLifecycles()
        {
            _lifecycles.Add(ResourceManager);
        }

        public void Update()
        {
            for (int i = 0; i < _lifecycles.Count; i++)
            {
                _lifecycles[i].Update();
            }
        }

        public void Log(string message)
        {
            if (!UnitTestDetector.IsInUnitTest)
                Debug.Log(message);
            Console.WriteLine(message);
        }
    }
}