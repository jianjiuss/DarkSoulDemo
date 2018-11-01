using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class AmPlayableBehaviour : PlayableBehaviour
{

    //PlayableDirector pd;
    public ActorManager am;

    public override void OnGraphStart (Playable playable)
    {
        //pd = (PlayableDirector)playable.GetGraph().GetResolver();
    }

    public override void OnGraphStop(Playable playable)
    {
        //if(pd != null)
        //{
        //    pd.playableAsset = null;
        //}
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        am.LockActorController(false);
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        am.LockActorController(true);
    }
}
