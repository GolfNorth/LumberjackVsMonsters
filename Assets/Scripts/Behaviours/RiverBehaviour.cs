using UnityEngine;

namespace LumberjackVsMonsters
{
    public class RiverBehaviour : MonoBehaviour, ITickable
    {
        [SerializeField] private Transform actorTransform;
        
        public void Tick()
        {
            var position = transform.position;
            position.x = actorTransform.position.x;

            transform.position = position;

        }
    }
}