using DefaultNamespace;
using Interfaces;
using System;
using System.Linq;
using UnityEngine.Assertions;
using Model;
using UnityEngine;

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
        private readonly ArView arView;
        private ARWindow arWindow;

        public ARState(WebCam webCam, 
                       ITargetTracker targetTracker,
                       SignInventory signInventory, 
                       WindowController windowController, 
                       TargetModel targetModel, 
                       ViewStateMachine viewStateMachine,
                       ArView arView)
        {
            this.webCam = webCam;
            this.targetTracker = targetTracker;
            this.signInventory = signInventory;
            this.windowController = windowController;
            this.targetModel = targetModel;
            this.viewStateMachine = viewStateMachine;
            this.arView = arView;
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

        private bool firstRun = true;
        
        public void Enter()
        {
            arWindow = windowController.ShowWindow(typeof(ARWindow)) as ARWindow;

            if (firstRun) {
                UpdateModelsLock();
                firstRun = false;
            }
            
            Assert.IsNotNull(arWindow);

            arWindow.LibraryButtonClicked += LibraryButtonClickHandler;
            arWindow.SignButtonClicked += SignButtonClickHandler;
            arWindow.CamSwitchButtonClicked += CamSwitchButtonClickHandler;

            targetTracker.TargetDetected += TargetDetectedHandler;
            targetTracker.TargetComputed += TargetComputedHandler;
            targetTracker.TargetLost += TargetLostHandler;
        }

        private void TargetComputedHandler(int targetId, Matrix4x4 matrix)
        {
            arView.Move(matrix);
        }

        public void Exit()
        { 
            targetTracker.TargetDetected -= TargetDetectedHandler;
            targetTracker.TargetLost -= TargetLostHandler;
            targetTracker.TargetComputed -= TargetComputedHandler;
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

                if (!signModel.IsFound) {
                    signModel.IsFound = true;
                    signModel.FoundTime = DateTime.Now;
                    signModel.Save();
                    UpdateModelsLock();

                }
                if(!signModel.IsLocked)
                    arView.Show(signModel.ArObject);
                    
            }
        }

        private void UpdateModelsLock()
        {
            var foundIds = signInventory.GetAll().Where(m => m.IsFound).Select(m => m.Id).ToList();
            
            foreach (var model in signInventory.GetAll()) {
                model.UpdateLockedStatus(foundIds);
            }
        }

        private void TargetLostHandler(int targetId)
        {
            arWindow.HideSignButton();
            arView.Hide();
        }
    }
}