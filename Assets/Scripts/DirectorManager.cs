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

    //[Header("=== Assets Settings ===")]
    //public ActorManager attacker;
    //public ActorManager victim;

	void Start ()
    {
        pd = GetComponent<PlayableDirector>();
        //pd.playableAsset = Instantiate(frontStab);

	}

    public void PlayFrontStab(ActorManager attack, ActorManager victim)
    {
        pd.playableAsset = Instantiate(frontStab);

        foreach (var track in pd.playableAsset.outputs)
        {
            if (track.streamName == "Attacker Script")
            {
                pd.SetGenericBinding(track.sourceObject, attack);
            }
            if (track.streamName == "Victim Script")
            {
                pd.SetGenericBinding(track.sourceObject, victim);
            }
            if (track.streamName == "Attacker Animation")
            {
                pd.SetGenericBinding(track.sourceObject, attack.ac.anim);
            }
            if (track.streamName == "Victim Animation")
            {
                pd.SetGenericBinding(track.sourceObject, victim.ac.anim);
            }
        }
        pd.Play();
    }

    void Update ()
    {
		if(Input.GetKeyDown(KeyCode.H) && gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            pd.Play();
        }
	}
}
