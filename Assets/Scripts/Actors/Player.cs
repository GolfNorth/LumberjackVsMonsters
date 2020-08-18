using UnityEngine;

namespace LumberjackVsMonsters
{
    public class Player : MonoBehaviour, IInitializable
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int healthRecovery = 2;
        [SerializeField] private AudioSource weaponAudio;
        private BehaviorInitializer _behaviorInitializer;
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

        public void Hit()
        {
            weaponAudio.Play();
        }
    }
}