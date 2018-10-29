using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.07586217f, 1f, 0f)]
[TrackClipType(typeof(MySuperPlayableClip))]
[TrackBindingType(typeof(Animator))]
public class MySuperPlayableTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<MySuperPlayableMixerBehaviour>.Create (graph, inputCount);
    }
}
