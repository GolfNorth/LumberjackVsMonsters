using UnityEngine;

namespace LumberjackVsMonsters
{
    public class MovementBehavior : MonoBehaviour, ITickable
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float jumpHeight = 0.5f;
        [SerializeField] private float groundDistance = 0.1f;
        [SerializeField] private Transform actorTransform;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource footstepsAudio;
        private Vector3 _velocity;
        private bool _isGrounded;
        private static readonly int Walk = Animator.StringToHash("Walk");

        public bool IsGrounded => _isGrounded;

        public Vector3 Velocity => characterController.velocity;

        public void Tick()
        {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
            
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            var move = actorTransform.right * x + actorTransform.forward * z;
            
            characterController.Move(move * speed * Time.deltaTime);

            if (_isGrounded && Input.GetButtonDown("Jump"))
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            _velocity.y += gravity * Time.deltaTime;

            characterController.Move(_velocity * Time.deltaTime);
            
            if (!_isGrounded)
            {
                if (footstepsAudio.isPlaying) footstepsAudio.Stop();
                
                animator.SetBool(Walk, false);
            }
            else if (x != 0 || z != 0)
            {
                if (!footstepsAudio.isPlaying) footstepsAudio.Play();
                
                animator.SetBool(Walk, true);
            }
            else
            {
                if (footstepsAudio.isPlaying) footstepsAudio.Stop();
                
                animator.SetBool(Walk, false);
            }
        }
    }
}