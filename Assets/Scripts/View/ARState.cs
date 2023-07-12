using DefaultNamespace;
using Interfaces;
using System;
using UnityEngine;
using UnityEngine.Assertions;
using Data;

namespace View
{
    public class ARState : IState<ViewState>
    {

        private WebCam webCam;
        private ITargetTracker targetTracker;
        private readonly IRegistry<SignData> signDataRegistry;
        private IWindowController windowController;
        private ARWindow arWindow;

        public ARState(WebCam webCam, 
                       ITargetTracker targetTracker, 
                       IRegistry<SignData> signDataRegistry,
                       IWindowController windowController) 
        {
            this.webCam = webCam;
            this.targetTracker = targetTracker;
            this.signDataRegistry = signDataRegistry;
            this.windowController = windowController;
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
            throw new NotImplementedException();
        }

        private void LibraryButtonClickHandler()
        {
            throw new NotImplementedException();
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
        }

        private void TargetLostHandler(int targetId)
        {
            arWindow?.HideSignButton();
        }
    }
}