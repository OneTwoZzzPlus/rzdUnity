using Interfaces;
using UnityEngine;
using View;

namespace DefaultNamespace
{
    public class RootController : MonoBehaviour
    {

        [SerializeField] private WebCam webCam;

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


        public IStateMachine<ViewState> StateMachine => viewStateMachine;
        private ViewStateMachine viewStateMachine;

        private void Awake()
        {
            var rootControllers = FindObjectsOfType<RootController>();
            if (rootControllers.Length > 1 && rootControllers[0] != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            viewStateMachine = new ViewStateMachine(
                new IState<ViewState>[] {
                    new ARState(webCam, TargetTracker),
                    new LibraryState(),
                    new InfoState()
                }, ViewState.AR);
        }
    }
}