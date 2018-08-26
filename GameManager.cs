using System;
using System.Collections.Generic;
using Domini.Buildings;
using Domini.Resources;
using UnityEngine;

namespace Domini
{
    public class GameManager
    {

        public ResourceManager ResourceManager;
        public BuildingManager BuildingManager;
        
        private static readonly GameManager _instance = new GameManager();

        private List<IGameLoop> _lifecycles;
        static GameManager(){}
        private GameManager(){}
        private float _lastUpdateTime;

        public static GameManager Instance => _instance;

        public void Start()
        {
            _lifecycles = new List<IGameLoop>();
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

        public void Update(float time, float deltaTime)
        {
            var secondsPassed = (long) Math.Floor(time - _lastUpdateTime);
            _lastUpdateTime = time;
            for (int i = 0; i < _lifecycles.Count; i++)
            {
                _lifecycles[i].Update(time, deltaTime, secondsPassed);
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