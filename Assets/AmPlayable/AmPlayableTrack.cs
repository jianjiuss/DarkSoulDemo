using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0f, 0.4866645f, 1f)]
[TrackClipType(typeof(AmPlayableClip))]
[TrackBindingType(typeof(ActorManager))]
public class AmPlayableTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<AmPlayableMixerBehaviour>.Create (graph, inputCount);
    }
}
