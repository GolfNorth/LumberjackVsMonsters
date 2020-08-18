using UnityEngine;

namespace LumberjackVsMonsters
{
    public static class Locator
    {
        private static readonly Vault<BaseSystem> SystemVault;

        static Locator()
        {
            SystemVault = new Vault<BaseSystem>();
        }
        
        public static void SetSystem<T>(T value) where T : class
        {
            SystemVault.SetValue(value as T);
        }
        
        public static T GetSystem<T>() where T : class
        {
            return SystemVault.GetValue<T>();
        }
    }
}