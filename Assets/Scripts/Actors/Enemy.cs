using UnityEngine;

namespace LumberjackVsMonsters
{
    public sealed class Enemy : MonoBehaviour
    {
        [SerializeField] private BaseData[] data;
        private MonoBehaviour[] _behaviors;
        private Vault<BaseData> _dataVault;
        private Vault<MonoBehaviour> _behaviorVault;
        private UpdateSystem _updateSystem;

        public Vault<BaseData> DataVault => _dataVault;

        public Vault<MonoBehaviour> BehaviorVault => _behaviorVault;

        private void Awake()
        {
            _dataVault = new Vault<BaseData>(data);
            _behaviors = GetComponents<MonoBehaviour>();
            _behaviorVault = new Vault<MonoBehaviour>(_behaviors);
            _updateSystem = Locator.GetSystem<UpdateSystem>();
            
            foreach (var behavior in _behaviors)
            {
                _updateSystem.Add(behavior);
            }
        }

        private void OnDisable()
        {
            foreach (var behavior in _behaviors)
            {
                _updateSystem.Remove(behavior);
            }
        }
    }
}