using DefaultNamespace;
using Interfaces;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace View
{
    public class ARState : IState<ViewState>
    {

        private readonly WebCam webCam;
        private readonly ITargetTracker targetTracker;

        private ARWindow arWindow;

        public ARState(WebCam webCam, ITargetTracker targetTracker) {
            this.webCam = webCam;
            this.targetTracker = targetTracker;
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
            arWindow = RootController.Instance.WindowController
                                     .ShowWindow(typeof(ARWindow)) as ARWindow;

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
            RootController.Instance.WindowController.HideWindow(typeof(ARWindow));
            targetTracker.TargetDetected -= TargetDetectedHandler;
            targetTracker.TargetLost -= TargetLostHandler;
        }

        private void TargetDetectedHandler(int index)
        {
            arWindow?.ShowSignButton();
            arWindow?.SetSignNumber(index);
        }

        private void TargetLostHandler(int index)
        {
            arWindow?.HideSignButton();
            arWindow?.SetSignNumber(index);
        }

        private void TargetComputedHandler(int index, Matrix4x4 matrix) { }

    }
}