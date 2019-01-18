using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterBones : MonoBehaviour
{
    public SkinnedMeshRenderer srcMeshRenderer;
    public SkinnedMeshRenderer tgtMeshRenderer;

	// Use this for initialization
	void Start ()
    {
        tgtMeshRenderer.bones = srcMeshRenderer.bones;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
