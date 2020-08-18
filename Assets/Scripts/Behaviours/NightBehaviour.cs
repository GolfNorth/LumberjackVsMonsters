using UnityEngine;

namespace LumberjackVsMonsters
{
    public class NightBehaviour : MonoBehaviour, IInitializable
    {
        [SerializeField] private DoorBehavior doorBehavior;
        private AudioSource _audio;
        
        public void Initialize()
        {
            _audio = GetComponent<AudioSource>();
            doorBehavior.DoorOpened += OnDoorOpened;
        }

        private void OnDisable()
        {
            doorBehavior.DoorOpened -= OnDoorOpened;
        }
        
        private void OnDoorOpened()
        {
            _audio.Play();
        }
    }
}