using Data;
using DefaultNamespace;
using Interfaces;
using Model;
using Unity.VisualScripting;
using UnityEngine.Assertions;

namespace View
{
    public class InfoState : IState<ViewState>
    {
        private readonly IRegistry<SignData> signDataRegistry;
        private readonly IWindowController windowController;
        private readonly TargetModel targetModel;
        private readonly IStateMachine<ViewState> viewStateMachine;

        private InfoWindow signInfoWindow;

        public ViewState Id => ViewState.Info;

        public InfoState(
            IWindowController windowController, 
            TargetModel targetModel, 
            ViewStateMachine viewStateMachine,
            SignDataRegistry signDataRegistry) 
        { 
            this.windowController = windowController;
            this.targetModel = targetModel;
            this.viewStateMachine = viewStateMachine;
            this.signDataRegistry = signDataRegistry;
        }

        public void Enter()
        {
            signInfoWindow = windowController.ShowWindow(typeof(InfoWindow)) as InfoWindow;
            Assert.IsNotNull(signInfoWindow);

            signInfoWindow.BackButtonClicked += BackButtonClickHandler;

            var signData = signDataRegistry.Get(targetModel.Id);
            if (signData)
            {
                signInfoWindow.SetSignName(signData.Name);
                signInfoWindow.SetSignNumber(signData.Number);
                signInfoWindow.SetSignDescription(signData.Description);
                signInfoWindow.SetImage(signData.Sprite);
            }
        }
        public void Exit()
        {
            signInfoWindow.BackButtonClicked -= BackButtonClickHandler;
            windowController.HideWindow(typeof(InfoWindow));
        }

        private void BackButtonClickHandler()
        {
            viewStateMachine.RevertState();
        }
    }
}