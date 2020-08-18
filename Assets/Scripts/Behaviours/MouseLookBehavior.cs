using UnityEngine;

namespace LumberjackVsMonsters
{
    public class MouseLookBehavior : MonoBehaviour, IInitializable, ITickable
    {
        [SerializeField] private float sensitivity = 100f;
        [SerializeField] private Transform actorTransform;
        [SerializeField] private Transform cameraTransform;
        private float _xRotation;
        
        public void Initialize()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        public void Tick()
        {
            var mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            
            cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            actorTransform.Rotate(Vector3.up * mouseX);
        }
    }
}