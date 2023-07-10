using Assets.Scripts;
using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class RootController : MonoBehaviour
    {
        public static RootController instance;
        [SerializeField] private FakeTargetTracker fakeTargetTracker;
        [SerializeField] private RealTargetTracker realTargetTracker;

#if UNITY_EDITOR
        public ITargetTracker targetTracker => fakeTargetTracker;
#else
        public ITargetTracker targetTracker => realTargetTracker;
#endif


        private void Awake()
        {
            var rootControllers = FindObjectsOfType<RootController>();
            if (rootControllers.Length > 1 && rootControllers[0] != this)
                Destroy(gameObject);
            instance = this;
        }
    }
}