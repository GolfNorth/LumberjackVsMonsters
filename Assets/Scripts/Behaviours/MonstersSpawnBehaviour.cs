using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace LumberjackVsMonsters
{
    public class MonstersSpawnBehaviour : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject spawnPoints;
        [SerializeField] private GameObject[] monsterPrefabs;
        [SerializeField] private DoorBehavior doorBehavior;
        private Random _random;
        private readonly List<Vector3> _spawnPositions = new List<Vector3>();
        
        public void Initialize()
        {
            _random = new Random();
            
            foreach (var spawnTransform in spawnPoints.GetComponentsInChildren<Transform>())
            {
                if (spawnTransform.position == Vector3.zero) continue;
                
                _spawnPositions.Add(spawnTransform.position);
            }

            doorBehavior.DoorOpened += OnDoorOpened;
        }
        
        private void OnDisable()
        {
            doorBehavior.DoorOpened -= OnDoorOpened;
        }

        private void OnDoorOpened()
        {
            for (var i = 0; i < 20; i++)
            {
                var monster = Instantiate(GetRandomPrefab());
                
                monster.transform.position = GetRandomPosition();
            }
        }

        public Vector3 GetRandomPosition()
        {
            var index = _random.Next(_spawnPositions.Count);

            return _spawnPositions[index];
        }
        
        public GameObject GetRandomPrefab()
        {
            var index = _random.Next(monsterPrefabs.Length);

            return monsterPrefabs[index];
        }
    }
}