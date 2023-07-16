using DefaultNamespace;
using Interfaces;
using System;
using UnityEngine;
using UnityEngine.Assertions;
using Data;
using Model;

namespace View
{
    public class ARState : IState<ViewState>
    {

        private WebCam webCam;
        private ITargetTracker targetTracker;
        private readonly SignInventory signInventory;
        private IWindowController windowController;
        private ARWindow arWindow;
        private readonly TargetModel targetModel;
        private readonly IStateMachine<ViewState> stateMachine;

        public ARState(WebCam webCam,
                       ITargetTracker targetTracker,
                       SignInventory signDataRegistry,
                       IWindowController windowController,
                       TargetModel targetModel,
                       IStateMachine<ViewState> stateMachine)
        {
            this.webCam = webCam;
            this.targetTracker = targetTracker;
            this.signInventory = signDataRegistry;
            this.windowController = windowController;
            this.targetModel = targetModel;
            this.stateMachine = stateMachine;
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
            stateMachine.ChangeState(ViewState.Info);
        }

        private void LibraryButtonClickHandler()
        {
            stateMachine.ChangeState(ViewState.Library);  
        }

        public void Exit()
        {
            windowController.HideWindow(typeof(ARWindow));
            targetTracker.TargetDetected -= TargetDetectedHandler;
            targetTracker.TargetLost -= TargetLostHandler;
        }

        private void TargetDetectedHandler(int targetId)
        {
            targetModel.Id = targetId;
            var signModel = signInventory.GetModel(targetId);
            if (signModel is { })
            {
                arWindow?.ShowSignButton();
                arWindow?.SetSignNumber(signModel.Number);
                arWindow?.SetSignName(signModel.Name);

                if (signModel.IsFound)
                    return;
                signModel.IsFound = true;
                signModel.FoundTime = DateTime.Now;
            }
        }

        private void TargetLostHandler(int targetId)
        {
            arWindow?.HideSignButton();
        }
    }
}