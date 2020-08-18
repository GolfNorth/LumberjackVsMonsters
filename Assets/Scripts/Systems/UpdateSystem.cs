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
            private readonly HashSet<ITickable> _ticks = new HashSet<ITickable>();
            private readonly HashSet<ILateTickable> _lateTicks = new HashSet<ILateTickable>();
            private readonly HashSet<IFixedTickable> _fixedTicks = new HashSet<IFixedTickable>();

            public HashSet<ITickable> Ticks => _ticks;

            public HashSet<ILateTickable> LateTicks => _lateTicks;

            public HashSet<IFixedTickable> FixedTicks => _fixedTicks;

            public void StartNewCoroutine(IEnumerator method)
            {
                StartCoroutine(method);
            }

            private void Update()
            {
                foreach (var tickable in _ticks)
                {
                    tickable.Tick();
                }
            }

            private void LateUpdate()
            {
                foreach (var tickable in _lateTicks)
                {
                    tickable.LateTick();
                }
            }

            private void FixedUpdate()
            {
                foreach (var tickable in _fixedTicks)
                {
                    tickable.FixedTick();
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