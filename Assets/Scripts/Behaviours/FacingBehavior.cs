using UnityEngine;

namespace LumberjackVsMonsters
{
    public class FacingBehavior : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool isActive;
        [SerializeField] private bool autoInit;
        private GameObject _wrapper;

        public Transform Target
        {
            get => target;
            set => target = value;
        }

        public bool IsActive
        {
            get => isActive;
            set => isActive = value;
        }

        public bool AutoInit
        {
            get => autoInit;
            set => autoInit = value;
        }

        private void Awake(){
            if (autoInit)
            {
                var mainCamera = Camera.main;
                
                if (mainCamera != null)
                {
                    target = mainCamera.transform;
                    isActive = true;
                }
            }

            _wrapper = new GameObject
            {
                name = "Wrapper_" + transform.gameObject.name
            };
            _wrapper.transform.position = transform.position;
            transform.parent = _wrapper.transform;
        }
    
        private void LateUpdate()
        {
            if (!isActive) return;
        
            var rotation = target.transform.rotation;
            _wrapper.transform.LookAt(_wrapper.transform.position + rotation * Vector3.back, rotation * Vector3.up);
        }
    }
}