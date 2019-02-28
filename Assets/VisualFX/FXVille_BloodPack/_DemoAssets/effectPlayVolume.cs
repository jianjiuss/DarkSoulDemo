using UnityEngine;
using System.Collections;

public class effectPlayVolume : MonoBehaviour 
{
	public ParticleSystem[] effectsToPlay;
	public bool armed = false;

	void OnTriggerEnter(Collider other)
	{
		armed = true;
	}	

	void OnTriggerExit(Collider other)
	{
		armed = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if(armed && Input.GetKeyDown(KeyCode.Space))
		{
			foreach(ParticleSystem p in effectsToPlay)
			{
				if(p.isPlaying == true)
				{
					p.Clear();
					p.Play();
				}
				else
				{
					p.Play();
				}
			}
		}
	}
}
