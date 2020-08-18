using UnityEngine;

namespace LumberjackVsMonsters
{
    public class WeaponBehavior : MonoBehaviour, IInitializable, ITickable
    {
        [SerializeField] private GameObject weapon;
        [SerializeField] private DoorBehavior doorBehavior;
        [SerializeField] private Animator animator;
        
        private bool _isArmed;
        private bool _isAttack;
        private static readonly int Attack = Animator.StringToHash("Attack");

        public void Initialize()
        {
            weapon.SetActive(false);
            doorBehavior.DoorOpened += OnDoorOpened;
        }

        private void OnDisable()
        {
            doorBehavior.DoorOpened -= OnDoorOpened;
        }

        private void OnDoorOpened()
        {
            weapon.SetActive(true);
            _isArmed = true;
        }

        public void Tick()
        {
            if (!_isArmed) return;

            if (Input.GetButtonDown("Fire") && !_isAttack)
            {
                _isAttack = true;
            }

            if (Input.GetButtonUp("Fire") && _isAttack)
            {
                _isAttack = false;
            }
            
            animator.SetBool(Attack, _isAttack);
        }
    }
}