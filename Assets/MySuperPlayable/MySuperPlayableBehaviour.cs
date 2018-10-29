using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public Camera myCamera;
    public float myFloat;

    public override void OnGraphStart (Playable playable)
    {
        Debug.Log("Graph Start");
    }

    public override void OnGraphStop(Playable playable)
    {
        Debug.Log("Graph Stop");
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        Debug.Log("Play");
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        Debug.Log("pause");
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
    }
}
