using Interfaces;
using UnityEngine;
using View;

namespace DefaultNamespace
{
    public class RootController : MonoBehaviour
    {
        public static RootController Instance;
        [SerializeField] private FakeTargetTracker fakeTargetTracker;
        [SerializeField] private RealTargetTracker realTargetTracker;

#if UNITY_EDITOR
        public ITargetTracker TargetTracker => fakeTargetTracker;
#else
        public ITargetTracker TargetTracker => realTargetTracker;
#endif

        [SerializeField] private WindowController windowController;
        public IWindowController WindowController => windowController;

        private void Awake()
        {
            var rootControllers = FindObjectsOfType<RootController>();
            if (rootControllers.Length > 1 && rootControllers[0] != this)
                Destroy(gameObject);
            Instance = this;
        }
    }
}