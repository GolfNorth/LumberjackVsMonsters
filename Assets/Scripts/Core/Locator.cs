using UnityEngine;

namespace LumberjackVsMonsters
{
    public static class Locator
    {
        private static Vault<BaseSystem> SystemVault;

        static Locator()
        {
            ClearSystems();
        }
        
        public static void SetSystem<T>(T value) where T : class
        {
            SystemVault.SetValue(value as T);
        }
        
        public static T GetSystem<T>() where T : class
        {
            return SystemVault.GetValue<T>();
        }

        public static void ClearSystems()
        {
            SystemVault = new Vault<BaseSystem>();
        }
    }
}