using UnityEngine;

namespace LumberjackVsMonsters
{
    public class BehaviorInitializer : MonoBehaviour
    {
        private MonoBehaviour[] _behaviors;
        private Vault<MonoBehaviour> _behaviorVault;
        private UpdateSystem _updateSystem;

        public Vault<MonoBehaviour> BehaviorVault => _behaviorVault;

        private void Awake()
        {
            _behaviors = GetComponents<MonoBehaviour>();
            _behaviorVault = new Vault<MonoBehaviour>(_behaviors);
            _updateSystem = Locator.GetSystem<UpdateSystem>();
        }

        private void OnEnable()
        {
            foreach (var behavior in _behaviors)
            {
                if (behavior == this) continue;
                
                _updateSystem.Add(behavior);
            }
        }

        private void OnDisable()
        {
            foreach (var behavior in _behaviors)
            {
                if (behavior == this) continue;
                
                _updateSystem.Remove(behavior);
            }
        }

        private void OnDestroy()
        {
            OnDisable();
        }
    }
}