using UnityEngine;

namespace LumberjackVsMonsters
{
    public class DogBehaviour : MonoBehaviour
    {
        [SerializeField] private AudioSource audio;

        private void OnTriggerEnter(Collider other)
        {
            audio.Play();
        }

        private void OnTriggerExit(Collider other)
        {
            audio.Stop();
        }
    }
}