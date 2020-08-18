using System;
using UnityEngine;

namespace LumberjackVsMonsters
{
    public class DoorBehavior : MonoBehaviour, IInitializable, ITickable
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform newPosition;
        [SerializeField] private GameObject insideCollider;
        [SerializeField] private GameObject outsideCollider;
        private BoxCollider _collider;
        private AudioSource _audio;
        private bool _isNear;
        private bool _isOpened;

        public event Action DoorOpened;

        private void OnTriggerEnter(Collider other)
        {
            _isNear = other.gameObject == characterController.gameObject;
        }
        
        private void OnTriggerExit(Collider other)
        {
            _isNear = other.gameObject == characterController.gameObject ? false : _isNear;
        }

        public void Initialize()
        {
            _audio = GetComponent<AudioSource>();
            _collider = GetComponent<BoxCollider>();
            insideCollider.SetActive(true);
            outsideCollider.SetActive(false);
        }

        public void Tick()
        {
            if (_isOpened || !_isNear || !Input.GetButtonDown("Interact")) return;

            _audio.Play();
            
            _collider.enabled = false;
            _isOpened = true;

            insideCollider.SetActive(false);
            outsideCollider.SetActive(true);
            
            characterController.Move(newPosition.position - characterController.transform.position);
            
            DoorOpened?.Invoke();
        }
    }
}