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

        private readonly TargetModel targetModel = new();

        public IWindowController WindowController => windowController;
#if UNITY_EDITOR
        public ITargetTracker TargetTracker => fakeTargetTracker;
#else
        public ITargetTracker TargetTracker => realTargetTracker;
#endif
  
        private ViewStateMachine viewStateMachine;
        public IStateMachine<ViewState> StateMachine => viewStateMachine;

        private SignInventory signInventory;

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
            var signFactory = new SignModel.Factory();
            signInventory = new SignInventory(signFactory);

            foreach (var signData in signDataRegistry.GetAll())
            {
                var model = signInventory.CreateModel(signData);
                model?.Load();
            }

            viewStateMachine = new ViewStateMachine();
            viewStateMachine.Initialize(new IState<ViewState>[] {
                    new ARState(webCam, TargetTracker, signInventory, windowController, targetModel, viewStateMachine),
                    new LibraryState(signInventory,windowController, targetModel, viewStateMachine),
                    new InfoState(signInventory,windowController, targetModel, viewStateMachine) 
            }, ViewState.AR);
        }
    }
}