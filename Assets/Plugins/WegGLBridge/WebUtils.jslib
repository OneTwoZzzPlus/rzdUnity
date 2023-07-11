 var LibraryGLClear = {
	OnStartEngine: function(){
		window.dispatchReactUnityEvent(
			"OnStartEngine"
		);
	},
	OnStartAr: function(){
		window.dispatchReactUnityEvent(
			"OnStartAr"
		);
	},
	OnStopAr: function(){
		window.dispatchReactUnityEvent(
			"OnStopAr"
		);
	},
	OnStopAudio: function(){
		window.dispatchReactUnityEvent(
			"OnStopAudio"
		);
	},
	OnStartAudio: function(){
		window.dispatchReactUnityEvent(
			"OnStartAudio"
		);
	},

	OnCameraAllowed: function(){
		window.dispatchReactUnityEvent(
			"OnCameraAllowed"
		);
	},
	
	OnMute : function(){
	    window.dispatchReactUnityEvent(
	        "OnMute"
        );
    },
    
    OnUnmute : function(){
        window.dispatchReactUnityEvent(
    	    "OnUnmute"
        );
    },
    
    OnSetAudioPosition : function(value){
        window.dispatchReactUnityEvent(
    	    "OnSetAudioPosition", value
        );
    }
};
mergeInto(LibraryManager.library, LibraryGLClear); 