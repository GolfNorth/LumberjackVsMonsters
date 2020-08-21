using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LumberjackVsMonsters
{
    public sealed class UpdateSystem : BaseSystem, IDisposable
    {
        private readonly UpdateManagerComponent _component;

        private sealed class UpdateManagerComponent : MonoBehaviour
        {
            private readonly List<ITickable> _ticks = new List<ITickable>();
            private readonly List<ILateTickable> _lateTicks = new List<ILateTickable>();
            private readonly List<IFixedTickable> _fixedTicks = new List<IFixedTickable>();

            public List<ITickable> Ticks => _ticks;

            public List<ILateTickable> LateTicks => _lateTicks;

            public List<IFixedTickable> FixedTicks => _fixedTicks;

            public void StartNewCoroutine(IEnumerator method)
            {
                StartCoroutine(method);
            }

            private void Update()
            {
                for (var i = 0; i < _ticks.Count; i++)
                {
                    _ticks[i]?.Tick();
                }
            }

            private void LateUpdate()
            {
                for (var i = 0; i < _lateTicks.Count; i++)
                {
                    _lateTicks[i]?.LateTick();
                }
            }

            private void FixedUpdate()
            {
                for (var i = 0; i < _fixedTicks.Count; i++)
                {
                    _fixedTicks[i]?.FixedTick();
                }
            }
        }

        public UpdateSystem()
        {
            var go = new GameObject("UpdateSystem");

            _component = go.AddComponent<UpdateManagerComponent>();

            Object.DontDestroyOnLoad(go);
        }

        public void Dispose()
        {
            Object.Destroy(_component.gameObject);
        }

        public void Add(object updatable)
        {
            if (updatable is IInitializable initializable) initializable.Initialize();

            if (updatable is ITickable tick) _component.Ticks.Add(tick);

            if (updatable is ILateTickable lateTick) _component.LateTicks.Add(lateTick);

            if (updatable is IFixedTickable fixedTick) _component.FixedTicks.Add(fixedTick);
        }

        public void Remove(object updatable)
        {
            if (updatable is ITickable tick) _component.Ticks.Remove(tick);

            if (updatable is ILateTickable lateTick) _component.LateTicks.Remove(lateTick);

            if (updatable is IFixedTickable fixedTick) _component.FixedTicks.Remove(fixedTick);
        }

        public void StartCoroutine(IEnumerator method)
        {
            _component.StartNewCoroutine(method);
        }
    }
}