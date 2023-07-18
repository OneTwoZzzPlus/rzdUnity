using DefaultNamespace;
using Interfaces;
using System;
using UnityEngine.Assertions;
using Model;

namespace View
{
    public class ARState : IState<ViewState>
    {
        private readonly WebCam webCam;
        private readonly ITargetTracker targetTracker;
        private readonly SignInventory signInventory;
        private readonly IWindowController windowController;
        private readonly TargetModel targetModel;
        private readonly IStateMachine<ViewState> viewStateMachine;
        private ARWindow arWindow;

        public ARState(WebCam webCam, 
                       ITargetTracker targetTracker,
                       SignInventory signInventory, 
                       WindowController windowController, 
                       TargetModel targetModel, 
                       ViewStateMachine viewStateMachine)
        {
            this.webCam = webCam;
            this.targetTracker = targetTracker;
            this.signInventory = signInventory;
            this.windowController = windowController;
            this.targetModel = targetModel;
            this.viewStateMachine = viewStateMachine;
            if (webCam)
            {
                webCam.OnInitialized += OnWebcamInitialized;
                webCam.Initiate();
            }
        }

        private void OnWebcamInitialized()
        {
            WebGLBridge.EngineStarted();
            arWindow.SetActiveCamSwitchButton(true);
        }

        public ViewState Id => ViewState.AR;

        public void Enter()
        {
            arWindow = windowController.ShowWindow(typeof(ARWindow)) as ARWindow;

            Assert.IsNotNull(arWindow);

            arWindow.LibraryButtonClicked += LibraryButtonClickHandler;
            arWindow.SignButtonClicked += SignButtonClickHandler;
            arWindow.CamSwitchButtonClicked += CamSwitchButtonClickHandler;

            targetTracker.TargetDetected += TargetDetectedHandler;
            targetTracker.TargetLost += TargetLostHandler;
        }

        public void Exit()
        { 
            targetTracker.TargetDetected -= TargetDetectedHandler;
            targetTracker.TargetLost -= TargetLostHandler;
            windowController.HideWindow(typeof(ARWindow));
        }

        private void SignButtonClickHandler()
        {
            viewStateMachine.ChangeState(ViewState.Info);
        }

        private void LibraryButtonClickHandler()
        {
            viewStateMachine.ChangeState(ViewState.Library);
        }

        private void CamSwitchButtonClickHandler()
        {
            arWindow.SetActiveCamSwitchButton(false);
            webCam.switchCamera();
        }

        private void TargetDetectedHandler(int targetId)
        {
            targetModel.Id = targetId;
            var signModel = signInventory.GetModel(targetId);
            if (signModel is {})
            {
                arWindow.ShowSignButton();
                arWindow.SetSignNumber(signModel.Number);
                arWindow.SetSignName(signModel.Name);

                if (signModel.IsFound) 
                    return;
                signModel.IsFound = true;
                signModel.FoundTime = DateTime.Now;
                signModel.Save();

            }
        }

        private void TargetLostHandler(int targetId)
        {
            arWindow.HideSignButton();
        }
    }
}