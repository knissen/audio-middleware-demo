﻿using FMODUnity;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOnBeatEmitter : MonoBehaviour
{
    // Variables that are modified in the callback need to be part of a seperate class.
    // This class needs to be 'blittable' otherwise it can't be pinned in memory.
    [StructLayout(LayoutKind.Sequential)]
    class TimelineInfo
    {
        public int currentMusicBar = 0;
        public int currentBeat = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
        public float currentTempo = 0;
    }

    public event System.EventHandler Beat;
    public event System.EventHandler Bar;

    public float CurrentTempo { get { return _timelineInfo != null ? _timelineInfo.currentTempo : 0f; } }

    //[SerializeField] [EventRef] private string _musicEventRef = "";
    [SerializeField] private FMODUnity.StudioEventEmitter _musicEventEmitter;

    [Header("Events")]
    public UnityEvent actionOnOne;
    public UnityEvent actionOnBar;

    private TimelineInfo _timelineInfo;
    private GCHandle _timelineHandle;

    private FMOD.Studio.EVENT_CALLBACK _beatCallback;
    private FMOD.Studio.EventInstance _musicInstance;

    private int _previousBar;
    private bool _listeningForEvents;

    void Start()
    {
        _timelineInfo = new TimelineInfo();

        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        _beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);

        // Pin the class that will store the data modified during the callback
        _timelineHandle = GCHandle.Alloc(_timelineInfo, GCHandleType.Pinned);
    }

    [ContextMenu("Listen")]
    public void ListenForEvents()
    {
        _musicInstance = _musicEventEmitter.EventInstance;// FMODUnity.RuntimeManager.CreateInstance(_musicEventRef);

        // Pass the object through the userdata of the instance
        _musicInstance.setUserData(GCHandle.ToIntPtr(_timelineHandle));

        _musicInstance.setCallback(_beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

        _listeningForEvents = true;
    }

    private void Update()
    {
        if(!_listeningForEvents && _musicEventEmitter.IsPlaying())
        {
            ListenForEvents();
        }

        if (_timelineInfo.currentBeat != -1)
        {
            actionOnOne.Invoke();
            Beat?.Invoke(this, EventArgs.Empty);

            _timelineInfo.currentBeat = -1;
        }

        if (_timelineInfo.currentMusicBar != _previousBar)
        {
            _previousBar = _timelineInfo.currentMusicBar;

            actionOnBar.Invoke();
            Bar?.Invoke(this, EventArgs.Empty);
        }
    }

    void OnDestroy()
    {
        _musicInstance.setUserData(IntPtr.Zero);
        _musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _musicInstance.release();
        _timelineHandle.Free();
    }

    //void OnGUI()
    //{
    //    GUILayout.Box(String.Format("Current Beat = {0}, Current Bar = {1}, Last Marker = {2}", _timelineInfo.currentBeat, _timelineInfo.currentMusicBar, (string)_timelineInfo.lastMarker));
    //}

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        // Retrieve the user data
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            // Get the object to store beat and marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentMusicBar = parameter.bar;
                        timelineInfo.currentBeat = parameter.beat;
                        timelineInfo.currentTempo = parameter.tempo;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                    }
                    break;
            }
        }

        return FMOD.RESULT.OK;
    }
}
