using Data;
using DefaultNamespace;
using Interfaces;
using Model;
using UnityEngine.Assertions;

namespace View
{
    public class InfoState : IState<ViewState>
    {
        private readonly IRegistry<SignData> signDataRegistry;
        private readonly IWindowController windowController;
        private readonly TargetModel targetModel;
        private readonly IStateMachine<ViewState> viewStateMachine;

        private SignInfoWindow signInfoWindow;

        public ViewState Id => ViewState.Info;

        public InfoState(SignDataRegistry signDataRegistry, 
                         WindowController windowController, 
                         TargetModel targetModel, 
                         ViewStateMachine viewStateMachine)
        {
            this.signDataRegistry = signDataRegistry;
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
            windowController.HideWindow(typeof(SignInfoWindow));
        }

        private void BackButtonClickHandler()
        {
            viewStateMachine.RevertState();
        }
    }
}