using UnityEngine;

namespace LumberjackVsMonsters
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Camera camera;
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
            _dataVault.SetValue(camera);
            _dataVault.SetValue(gameObject);
            _dataVault.SetValue(transform);
            _behaviors = GetComponents<MonoBehaviour>();
            _behaviorVault = new Vault<MonoBehaviour>(_behaviors);
            _updateSystem = Locator.GetSystem<UpdateSystem>();
        }

        private void OnEnable()
        {
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