using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterBones : MonoBehaviour
{
    public SkinnedMeshRenderer srcMeshRenderer;
    public List<SkinnedMeshRenderer> tgtMeshRenderers;

	// Use this for initialization
	void Start ()
    {
        foreach(var tgt in tgtMeshRenderers)
        {
            tgt.bones = srcMeshRenderer.bones;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
