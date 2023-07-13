using DefaultNamespace;
using Interfaces;
using System;
using UnityEngine.Assertions;
using Data;
using Model;
using static UnityEngine.GraphicsBuffer;

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
                       SignDataRegistry signDataRegistry, 
                       WindowController windowController, 
                       TargetModel targetModel, 
                       ViewStateMachine viewStateMachine)
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

        private void TargetDetectedHandler(int targetId)
        {
            targetModel.Id = targetId;
            var signData = signDataRegistry.Get(targetId);
            if (signData)
            {
                arWindow?.ShowSignButton();
                arWindow?.SetSignNumber(signData.Number);
                arWindow?.SetSignName(signData.Name);
            }
        }

        private void TargetLostHandler(int targetId)
        {
            arWindow?.HideSignButton();
            //targetModel.Id = -1;
        }
    }
}