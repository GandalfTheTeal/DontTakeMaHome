using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root_GFX : MonoBehaviour
{

    [SerializeField] GameObject[] _OutLines;

    private Root _Root;
    private Animator _ANM;

    void Awake()
    {
        _Root = GetComponent<Root>();
        _ANM = GetComponentInParent<Animator>();

		_Root.OnInteract += Shake;

		_Root.OnEnterRange += EnterRangeEffect;
		_Root.OnLeaveRange += LeaveRangeEffect;
    }

    // Update is called once per frame
    void Shake()
    {
        _ANM.SetTrigger("Sway");
    }


	void LeaveRangeEffect()
    {
		foreach (GameObject item in _OutLines)
        {
            item.SetActive(false);

        }
    }

    void EnterRangeEffect()
    {
		foreach (GameObject item in _OutLines)
        {
            item.SetActive(true);

        }
    }
}
