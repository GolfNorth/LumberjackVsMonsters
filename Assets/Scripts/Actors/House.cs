using System;
using UnityEngine;

namespace LumberjackVsMonsters
{
    public class House : MonoBehaviour, IInitializable
    {
        [SerializeField] private float maxHealth = 100;
        private float _health;

        public event Action HouseDestroyed;

        public float Health
        {
            get => _health;
            set => _health = value;
        }

        public void Initialize()
        {
            _health = maxHealth;
        }
        
        public void ApplyHit(float damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                HouseDestroyed?.Invoke();
            }
        }
    }
}