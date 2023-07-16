using Data;
using DefaultNamespace;
using Interfaces;
using Model;
using UnityEngine.Assertions;

namespace View
{
    public class InfoState : IState<ViewState>
    {
        private readonly SignInventory signInventory;
        private readonly IWindowController windowController;
        private readonly TargetModel targetModel;
        private readonly IStateMachine<ViewState> viewStateMachine;

        private SignInfoWindow signInfoWindow;

        public ViewState Id => ViewState.Info;

        public InfoState(SignInventory signInventory, 
                         WindowController windowController, 
                         TargetModel targetModel, 
                         ViewStateMachine viewStateMachine)
        {
            this.signInventory = signInventory;
            this.windowController = windowController;
            this.targetModel = targetModel;
            this.viewStateMachine = viewStateMachine;
        }

        public void Enter()
        {
            signInfoWindow = windowController.ShowWindow(typeof(SignInfoWindow)) as SignInfoWindow;
            Assert.IsNotNull(signInfoWindow);

            signInfoWindow.BackButtonClicked += BackButtonClickHandler;
            windowController.ShowWindow(typeof(SignInfoWindow));

            var signData = signInventory.GetModel(targetModel.Id);
            if (signData is {})
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
            windowController.HideWindow(typeof(SignInfoWindow));
        }

        private void BackButtonClickHandler()
        {
            viewStateMachine.RevertState();
        }
    }
}