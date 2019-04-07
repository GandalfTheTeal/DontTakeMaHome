using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPoseControl : MonoBehaviour
{
	[SerializeField] private Animator[] animsToSend;


    // Use this for initialization
    void Start()
    {
		foreach (var item in animsToSend)
		{
			item.SetTrigger("HeroPose");
		}
    }
}
