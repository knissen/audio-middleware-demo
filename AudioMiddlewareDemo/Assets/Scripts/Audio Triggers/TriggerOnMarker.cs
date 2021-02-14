using System;
using System.Runtime.InteropServices;
using FMOD;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOnMarker : MonoBehaviour
{
    // Data has to be in its own class to work with pinning
    [StructLayout(LayoutKind.Sequential)]
    class MarkerInfo
    {
        public StringWrapper lastMarker = new StringWrapper();
    }

    public UnityEvent actionOnMarker;
    public FMODUnity.StudioEventEmitter eventEmitter;
    public string markerName = "Boom";

    private EVENT_CALLBACK _markerCallback;
    private MarkerInfo _markerInfo;
    private GCHandle _markerHandle;

    // Start is called before the first frame update
    void Start()
    {
        // Keep a reference to the callback so it is not garbage collected
        _markerCallback = new FMOD.Studio.EVENT_CALLBACK(OnMarkerHit);

        // Local variable to use to hold data from callback
        _markerInfo = new MarkerInfo();

        // Create a handle to the info variable and pin it so it does not get garbage collected
        _markerHandle = GCHandle.Alloc(_markerInfo, GCHandleType.Pinned);

        // Pass a pointer to the info variable to the event instance so it can fill it with data in the callback
        eventEmitter.EventInstance.setUserData(GCHandle.ToIntPtr(_markerHandle));
        eventEmitter.EventInstance.setCallback(OnMarkerHit, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(_markerInfo.lastMarker) && markerName.Equals(_markerInfo.lastMarker))
        {
            UnityEngine.Debug.Log("Marker hit");
            actionOnMarker.Invoke();

            // Clear the entry so we don't repeat this till next time its set
            _markerInfo.lastMarker = new StringWrapper();
        }
    }

    private void OnDestroy()
    {
        // Clear the user data pointer and free the marker handle for garbage collections
        eventEmitter.EventInstance.setUserData(IntPtr.Zero);
        _markerHandle.Free();
    }

    // Attribute indicates this callback will be called from unmanaged code and method must be static
    [AOT.MonoPInvokeCallback(typeof(EVENT_CALLBACK))]
    protected static FMOD.RESULT OnMarkerHit(EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameters)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        var timelineParameters = (TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameters, typeof(TIMELINE_MARKER_PROPERTIES));

        string markerName = timelineParameters.name;

        if (markerName.Equals(markerName, StringComparison.CurrentCultureIgnoreCase))
        {
            IntPtr markerInfoPtr;
            FMOD.RESULT result = instance.getUserData(out markerInfoPtr);

            if (result == RESULT.OK && markerInfoPtr != IntPtr.Zero)
            {
                GCHandle markerHandle = GCHandle.FromIntPtr(markerInfoPtr);
                MarkerInfo markerInfo = (MarkerInfo)markerHandle.Target;

                markerInfo.lastMarker = timelineParameters.name;
            }
        }

        return RESULT.OK;
    }
}
