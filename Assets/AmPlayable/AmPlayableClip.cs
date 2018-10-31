using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class AmPlayableClip : PlayableAsset, ITimelineClipAsset
{
    public AmPlayableBehaviour template = new AmPlayableBehaviour ();
    public ExposedReference<ActorManager> am;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<AmPlayableBehaviour>.Create (graph, template);
        AmPlayableBehaviour clone = playable.GetBehaviour ();
        //am.exposedName = GetInstanceID().ToString();
        clone.am = am.Resolve(graph.GetResolver());
        return playable;
    }
}
