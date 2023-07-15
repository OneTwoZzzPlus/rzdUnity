using Data;
using Interfaces;
using Model;
using UnityEngine;
using View;

namespace DefaultNamespace
{
    public class RootController : MonoBehaviour
    {
        public static RootController Instance;

        [SerializeField] private WebCam webCam;
        [SerializeField] private FakeTargetTracker fakeTargetTracker;
        [SerializeField] private RealTargetTracker realTargetTracker;
        [SerializeField] private WindowController windowController;
        [SerializeField] private SignDataRegistry signDataRegistry;

        private TargetModel targetModel = new TargetModel();


        public IWindowController WindowController => windowController;
#if UNITY_EDITOR
        public ITargetTracker TargetTracker => fakeTargetTracker;
#else
        public ITargetTracker TargetTracker => realTargetTracker;
#endif

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
            viewStateMachine = new ViewStateMachine();

            viewStateMachine.Initialize(new IState<ViewState>[] {
                    new ARState(
                        webCam, 
                        TargetTracker, 
                        signDataRegistry, 
                        windowController, 
                        targetModel, 
                        viewStateMachine
                        ),
                    new LibraryState(signDataRegistry, windowController, targetModel, viewStateMachine),
                    new InfoState(windowController, targetModel, viewStateMachine, signDataRegistry)
                }, ViewState.AR);
        }
    }
}