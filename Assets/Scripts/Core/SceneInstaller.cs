using UnityEngine;

namespace LumberjackVsMonsters
{
    public class SceneInstaller : MonoBehaviour
    {
        private void Awake()
        {
            Locator.ClearSystems();
            Locator.SetSystem(new UpdateSystem());
        }
    }
}