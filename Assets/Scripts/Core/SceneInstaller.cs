using UnityEngine;

namespace LumberjackVsMonsters
{
    public class SceneInstaller : MonoBehaviour
    {
        private void Awake()
        {
            Locator.SetSystem(new UpdateSystem());
        }
    }
}