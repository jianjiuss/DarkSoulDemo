using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class AmPlayableClip : PlayableAsset, ITimelineClipAsset
{
    public AmPlayableBehaviour template = new AmPlayableBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<AmPlayableBehaviour>.Create (graph, template);
        AmPlayableBehaviour clone = playable.GetBehaviour ();
        return playable;
    }
}
