using System;
using UnityEngine;

namespace LumberjackVsMonsters
{
    public class DialogBehavior : MonoBehaviour, ITickable
    {
        [SerializeField] private AudioSource wifeAudio;
        [SerializeField] private AudioSource daughterAudio;
        [SerializeField] private AudioSource sonAudio;
        [SerializeField] private float wifeDelay = 2;
        [SerializeField] private float daughterDelay = 1;
        [SerializeField] private float sonDelay = 1;
        private bool _wifeSaid;
        private bool _daughterSaid;
        private bool _sonSaid;

        public event Action DialogEnded;

        public void Tick()
        {
            if (_wifeSaid && _daughterSaid && _sonSaid) return;
            
            if (!_wifeSaid && !wifeAudio.isPlaying)
            {
                wifeAudio.PlayDelayed(wifeDelay);
                _wifeSaid = true;
            }
            
            if (_wifeSaid && !_daughterSaid && !wifeAudio.isPlaying && !daughterAudio.isPlaying)
            {
                daughterAudio.PlayDelayed(daughterDelay);
                _daughterSaid = true;
            }
            
            if (_daughterSaid && !_sonSaid && !daughterAudio.isPlaying && !sonAudio.isPlaying)
            {
                sonAudio.PlayDelayed(sonDelay);
                _sonSaid = true;
                
                DialogEnded?.Invoke();
            }
        }
    }
}