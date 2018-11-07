using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class DirectorManager : IActorManager
{

    public PlayableDirector pd;

    [Header("=== Timeline assets ===")]
    public TimelineAsset frontStab;
    public TimelineAsset openBox;
    public TimelineAsset pullLever;

    //[Header("=== Assets Settings ===")]
    //public ActorManager attacker;
    //public ActorManager victim;

	void Start ()
    {
        pd = GetComponent<PlayableDirector>();
        //pd.playableAsset = Instantiate(frontStab);
	}

    public bool IsPlaying()
    {
        return pd.state == PlayState.Playing;
    }

    public void PlayFrontStab(ActorManager attack, ActorManager victim)
    {
        pd.playableAsset = Instantiate(frontStab);

        TimelineAsset timeline = (TimelineAsset)pd.playableAsset;

        foreach(var track in timeline.GetOutputTracks())
        {
            if (track.name == "Attacker Script")
            {
                pd.SetGenericBinding(track, attack);
                foreach(var clip in track.GetClips())
                {
                    AmPlayableClip amClip = (AmPlayableClip)clip.asset;
                    AmPlayableBehaviour amBehaviour = amClip.template;
                    amClip.am.exposedName = System.Guid.NewGuid().ToString();
                    pd.SetReferenceValue(amClip.am.exposedName, attack);
                }
            }
            else if (track.name == "Victim Script")
            {
                pd.SetGenericBinding(track, victim);
                foreach (var clip in track.GetClips())
                {
                    AmPlayableClip amClip = (AmPlayableClip)clip.asset;
                    AmPlayableBehaviour amBehaviour = amClip.template;
                    amClip.am.exposedName = System.Guid.NewGuid().ToString();
                    pd.SetReferenceValue(amClip.am.exposedName, victim);
                }
            }
            else if (track.name == "Attacker Animation")
            {
                pd.SetGenericBinding(track, attack.ac.anim);
            }
            else if (track.name == "Victim Animation")
            {
                pd.SetGenericBinding(track, victim.ac.anim);
            }
        }

        pd.Evaluate();
        pd.Play();
    }

    internal void PullLever(ActorManager attack, ActorManager victim)
    {
        pd.playableAsset = Instantiate(pullLever);

        TimelineAsset timeline = (TimelineAsset)pd.playableAsset;

        foreach (var track in timeline.GetOutputTracks())
        {
            if (track.name == "Player Script")
            {
                pd.SetGenericBinding(track, attack);
                foreach (var clip in track.GetClips())
                {
                    AmPlayableClip amClip = (AmPlayableClip)clip.asset;
                    AmPlayableBehaviour amBehaviour = amClip.template;
                    amClip.am.exposedName = System.Guid.NewGuid().ToString();
                    pd.SetReferenceValue(amClip.am.exposedName, attack);
                }
            }
            else if (track.name == "Lever Script")
            {
                pd.SetGenericBinding(track, victim);
                foreach (var clip in track.GetClips())
                {
                    AmPlayableClip amClip = (AmPlayableClip)clip.asset;
                    AmPlayableBehaviour amBehaviour = amClip.template;
                    amClip.am.exposedName = System.Guid.NewGuid().ToString();
                    pd.SetReferenceValue(amClip.am.exposedName, victim);
                }
            }
            else if (track.name == "Player Animation")
            {
                pd.SetGenericBinding(track, attack.ac.anim);
            }
            else if (track.name == "Lever Animation")
            {
                pd.SetGenericBinding(track, victim.ac.anim);
            }
        }

        pd.Evaluate();
        pd.Play();
    }

    public void PlayOpenBox(ActorManager attack, ActorManager victim)
    {
        pd.playableAsset = Instantiate(openBox);

        TimelineAsset timeline = (TimelineAsset)pd.playableAsset;

        foreach (var track in timeline.GetOutputTracks())
        {
            if (track.name == "Player Script")
            {
                pd.SetGenericBinding(track, attack);
                foreach (var clip in track.GetClips())
                {
                    AmPlayableClip amClip = (AmPlayableClip)clip.asset;
                    AmPlayableBehaviour amBehaviour = amClip.template;
                    amClip.am.exposedName = System.Guid.NewGuid().ToString();
                    pd.SetReferenceValue(amClip.am.exposedName, attack);
                }
            }
            else if (track.name == "Box Script")
            {
                pd.SetGenericBinding(track, victim);
                foreach (var clip in track.GetClips())
                {
                    AmPlayableClip amClip = (AmPlayableClip)clip.asset;
                    AmPlayableBehaviour amBehaviour = amClip.template;
                    amClip.am.exposedName = System.Guid.NewGuid().ToString();
                    pd.SetReferenceValue(amClip.am.exposedName, victim);
                }
            }
            else if (track.name == "Player Animation")
            {
                pd.SetGenericBinding(track, attack.ac.anim);
            }
            else if (track.name == "Box Animation")
            {
                pd.SetGenericBinding(track, victim.ac.anim);
            }
        }

        pd.Evaluate();
        pd.Play();
    }
    
}
