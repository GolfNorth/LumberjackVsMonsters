using UnityEngine;

namespace LumberjackVsMonsters
{
    public class Monster : MonoBehaviour, ITickable, IInitializable
    {
        [SerializeField] private Transform hitCheck;
        [SerializeField] private float hitRadius = 0.25f;
        [SerializeField] private float damagePerSecond;
        [SerializeField] private LayerMask playerOrHouseMask;
        private ParticleSystem _particleSystem;
        private MeshRenderer _meshRenderer;
        private AudioSource _audioSource;
        private MeshCollider _meshCollider;
        private bool _isDying;
        private float _dyingTimer;
        
        public void ApplyHit(float damage)
        {
            _particleSystem.Play();
            _audioSource.Stop();
            _meshRenderer.enabled = false;
            _meshCollider.enabled = false;
            _isDying = true;
        }

        public void Tick()
        {
            if (_isDying)
            {
                _dyingTimer += Time.deltaTime;
                
                if (_dyingTimer > _particleSystem.main.startLifetime.constant) 
                    Destroy(gameObject);
            }
            else
            {
                var colliders = Physics.OverlapSphere(hitCheck.position, hitRadius, playerOrHouseMask);

                for (var i = 0; i < colliders.Length; i++)
                {
                    colliders[i].gameObject.SendMessage("ApplyHit", damagePerSecond * Time.deltaTime);
                }
            }
        }

        public void Initialize()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _audioSource = GetComponent<AudioSource>();
            _meshCollider = GetComponent<MeshCollider>();
        }
        
        
    }
}