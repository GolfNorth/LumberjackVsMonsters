using System;
using UnityEngine;

namespace LumberjackVsMonsters
{
    public class ZoneBehavior : MonoBehaviour, ITickable
    {
        [SerializeField] private float outsideTime = 3;
        private float _timer;
        private bool _isOutside;

        public event Action PlayerOutside;
        public event Action PlayerInside;
        public event Action PlayerEscaped;

        public float Timer => _timer;

        public bool IsOutside => _isOutside;

        private void OnTriggerEnter(Collider other)
        {
            _isOutside = false;
            PlayerInside?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            _timer = outsideTime;
            _isOutside = true;
            PlayerOutside?.Invoke();
        }

        public void Tick()
        {
            if (!_isOutside) return;

            _timer -= Time.deltaTime;
            
            if (_timer <= 0) PlayerEscaped?.Invoke();
        }
    }
}