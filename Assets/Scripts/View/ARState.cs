using DefaultNamespace;
using Interfaces;
using System;
using UnityEngine.Assertions;
using Data;
using Model;

namespace View
{
    public class ARState : IState<ViewState>
    {

        private WebCam webCam;
        private ITargetTracker targetTracker;
        private readonly IRegistry<SignData> signDataRegistry;
        private IWindowController windowController;
        private readonly TargetModel targetModel;
        private readonly IStateMachine<ViewState> viewStateMachine;
        private ARWindow arWindow;

        public ARState(WebCam webCam, 
                       ITargetTracker targetTracker, 
                       IRegistry<SignData> signDataRegistry,
                       IWindowController windowController,
                       TargetModel targetModel,
                       IStateMachine<ViewState> viewStateMachine) 
        {
            this.webCam = webCam;
            this.targetTracker = targetTracker;
            this.signDataRegistry = signDataRegistry;
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
        }

        public ViewState Id => ViewState.AR;

        public void Enter()
        {
            arWindow = windowController.ShowWindow(typeof(ARWindow)) as ARWindow;

            Assert.IsNotNull(arWindow);

            arWindow.LibraryButtonClicked += LibraryButtonClickHandler;
            arWindow.SignButtonClicked += SignButtonClickHandler;

            targetTracker.TargetDetected += TargetDetectedHandler;
            targetTracker.TargetLost += TargetLostHandler;
        }

        private void SignButtonClickHandler()
        {
            viewStateMachine.ChangeState(ViewState.Info);
        }

        private void LibraryButtonClickHandler()
        {
            viewStateMachine.ChangeState(ViewState.Library);
        }

        public void Exit()
        {
            windowController.HideWindow(typeof(ARWindow));
            targetTracker.TargetDetected -= TargetDetectedHandler;
            targetTracker.TargetLost -= TargetLostHandler;
        }

        private void TargetDetectedHandler(int targetId)
        {
            var signData = signDataRegistry.Get(targetId);
            if (signData)
            {
                arWindow?.ShowSignButton();
                arWindow?.SetSignNumber(signData.Number);
                arWindow?.SetSignName(signData.Name);
            }

            targetModel.Id = targetId;
        }

        private void TargetLostHandler(int targetId)
        {
            arWindow?.HideSignButton();
            targetModel.Id = -1;
        }
    }
}