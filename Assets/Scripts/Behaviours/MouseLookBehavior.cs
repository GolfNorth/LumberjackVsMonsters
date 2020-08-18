using UnityEngine;

namespace LumberjackVsMonsters
{
    public class MouseLookBehavior : MonoBehaviour, IInitializable, ITickable
    {
        [SerializeField] private float sensitivity = 100f;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform cameraTransform;
        private float _xRotation;
        
        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
        
        public void Tick()
        {
            var mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            
            cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            playerTransform.Rotate(Vector3.up * mouseX);
        }
    }
}