using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace LumberjackVsMonsters
{
    public class MonstersSpawnBehaviour : MonoBehaviour, IInitializable, ITickable
    {
        [SerializeField] private GameObject spawnPoints;
        [SerializeField] private GameObject[] monsterPrefabs;
        [SerializeField] private DoorBehavior doorBehavior;
        [SerializeField] private float spawnDelay;
        private float _spawnTimer;
        private bool _isActive;
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

        public void Tick()
        {
            _spawnTimer += Time.deltaTime;
            
            if (!_isActive || _spawnTimer < spawnDelay) return;
            
            var monster = Instantiate(GetRandomPrefab());
                
            monster.transform.position = GetRandomPosition();

            _spawnTimer = 0;
        }

        private void OnDisable()
        {
            doorBehavior.DoorOpened -= OnDoorOpened;
        }

        private void OnDoorOpened()
        {
            _isActive = true;
        }
        
        private Vector3 GetRandomPosition()
        {
            var index = _random.Next(_spawnPositions.Count);

            return _spawnPositions[index];
        }
        
        private GameObject GetRandomPrefab()
        {
            var index = _random.Next(monsterPrefabs.Length);

            return monsterPrefabs[index];
        }
    }
}