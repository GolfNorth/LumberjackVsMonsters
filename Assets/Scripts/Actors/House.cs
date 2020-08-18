using System;
using UnityEngine;

namespace LumberjackVsMonsters
{
    public class House : MonoBehaviour, IInitializable
    {
        [SerializeField] private int maxHealth = 100;
        private int _health;

        public int Health
        {
            get => _health;
            set => _health = value;
        }

        public void Initialize()
        {
            _health = maxHealth;
        }
    }
}