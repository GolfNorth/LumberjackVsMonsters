using UnityEngine;
using UnityEngine.AI;

namespace LumberjackVsMonsters
{
    public class HouseAttackBehaviour : MonoBehaviour, IInitializable, ITickable
    {
        private NavMeshAgent _navMeshAgent;
        private Transform _playerTransform;
        private Transform _targetTransform;
        private bool _isTargetPlayer;

        public void Initialize()
        {
            _playerTransform = GameObject.Find("Player").transform;
            _targetTransform = GameObject.Find("NewActorPosition").transform;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.destination = _targetTransform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            _isTargetPlayer = other.gameObject == _playerTransform.gameObject;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject != _playerTransform.gameObject) return;

            _isTargetPlayer = false;
            _navMeshAgent.destination = _targetTransform.position;
        }

        public void Tick()
        {
            if (_isTargetPlayer)
                _navMeshAgent.destination = _playerTransform.position;
        }
    }
}