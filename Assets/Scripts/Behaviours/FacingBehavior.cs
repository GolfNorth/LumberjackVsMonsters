using UnityEngine;

namespace LumberjackVsMonsters
{
    public class FacingBehavior : MonoBehaviour, IInitializable, ILateTickable
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool autoInit;
        private UpdateSystem _updateSystem;
        private GameObject _wrapper;
        private bool _isActive;

        public Transform Target
        {
            get => target;
            set => target = value;
        }

        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        public bool AutoInit
        {
            get => autoInit;
            set => autoInit = value;
        }

        public void Awake()
        {
            _updateSystem = Locator.GetSystem<UpdateSystem>();
        }

        public void Initialize()
        {
            if (autoInit)
            {
                var mainCamera = Camera.main;
                
                if (mainCamera != null)
                {
                    target = mainCamera.transform;
                    _isActive = true;
                }
            }

            _wrapper = new GameObject
            {
                name = "Wrapper_" + transform.gameObject.name
            };
            _wrapper.transform.position = transform.position;
            transform.parent = _wrapper.transform;
        }

        private void OnEnable()
        {
            _updateSystem.Add(this);
        }

        private void OnDisable()
        {
            _updateSystem.Remove(this);
        }

        public void LateTick()
        {
            if (!_isActive) return;
        
            var rotation = target.transform.rotation;
            _wrapper.transform.LookAt(_wrapper.transform.position + rotation * Vector3.back, rotation * Vector3.up);
        }
    }
}