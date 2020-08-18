using UnityEngine;

namespace LumberjackVsMonsters
{
    public class FacingBehavior : MonoBehaviour, IInitializable, ILateTickable
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool autoInit;
        private UpdateSystem _updateSystem;
        //private GameObject _wrapper;
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

        public void Initialize()
        {
            if (autoInit)
            {
                var mainCamera = Camera.main;
                
                if (mainCamera != null)
                {
                    target = mainCamera.transform;
                }
            }
            
            _isActive = target != null;

            // _wrapper = new GameObject
            // {
            //     name = "Wrapper_" + transform.gameObject.name
            // };
            // _wrapper.transform.position = transform.position;
            // transform.parent = _wrapper.transform;
        }

        public void LateTick()
        {
            if (!_isActive) return;
            
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            rotation *= Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        
            //var rotation = target.transform.rotation;
            //_wrapper.transform.LookAt(_wrapper.transform.position + rotation * Vector3.back, rotation * Vector3.up);
        }
    }
}