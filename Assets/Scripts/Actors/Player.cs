using System;
using UnityEngine;

namespace LumberjackVsMonsters
{
    public class Player : MonoBehaviour, IInitializable, ITickable
    {
        [SerializeField] private Transform hitCheck;
        [SerializeField] private float hitRadius = 0.25f;
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private float healthRecovery = 2;
        [SerializeField] private AudioSource weaponAudio;
        [SerializeField] private LayerMask monsterMask;
        private BehaviorInitializer _behaviorInitializer;
        private float _health;
        
        public event Action PlayerDied;
        public event Action MonsterHit;

        public float Health
        {
            get => _health;
            set => _health = value;
        }

        public float MaxHealth => maxHealth;

        public void Initialize()
        {
            _health = maxHealth;
        }

        public void Tick()
        {
            _health += healthRecovery * Time.deltaTime;
            _health = _health > maxHealth ? maxHealth : _health;
        }
        
        public void Hit()
        {
            weaponAudio.Play();
            
            var colliders = Physics.OverlapSphere(hitCheck.position, hitRadius, monsterMask, QueryTriggerInteraction.Ignore);

            for (var i = 0; i < colliders.Length; i++)
            {
                colliders[i].gameObject.SendMessage("ApplyHit", 1);
                MonsterHit?.Invoke();
            }
        }

        public void ApplyHit(float damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                PlayerDied?.Invoke();
            }
        }
    }
}