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
        private readonly SignInventory signInventory;
        private readonly IWindowController windowController;
        private readonly TargetModel targetModel;
        private readonly IStateMachine<ViewState> viewStateMachine;

        private InfoWindow signInfoWindow;

        public ViewState Id => ViewState.Info;

        public InfoState(
            IWindowController windowController, 
            TargetModel targetModel, 
            ViewStateMachine viewStateMachine,
            SignInventory signDataRegistry) 
        { 
            this.windowController = windowController;
            this.targetModel = targetModel;
            this.viewStateMachine = viewStateMachine;
            this.signInventory = signDataRegistry;
        }

        public void Enter()
        {
            signInfoWindow = windowController.ShowWindow(typeof(InfoWindow)) as InfoWindow;
            Assert.IsNotNull(signInfoWindow);

            signInfoWindow.BackButtonClicked += BackButtonClickHandler;

            var signModel = signInventory.GetModel(targetModel.Id);
            if (signModel is { })
            {
                signInfoWindow.SetSignName(signModel.Name);
                signInfoWindow.SetSignNumber(signModel.Number);
                signInfoWindow.SetSignDescription(signModel.Description);
                signInfoWindow.SetImage(signModel.Sprite);
                signInfoWindow.SetFound(signModel.IsFound, signModel.FoundTime);
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