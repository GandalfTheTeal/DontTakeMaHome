using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lifetime : MonoBehaviour
{

	public UnityEvent OnDead;

	public float lifespan = 3;
    void Start()
    {
        Invoke("CallDead",lifespan);

    }


    void CallDead()
    {
		Destroy(gameObject,lifespan);
		if(OnDead!= null)OnDead.Invoke();
    }
}
