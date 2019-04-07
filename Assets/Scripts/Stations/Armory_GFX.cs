using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armory_GFX : MonoBehaviour
{
    #region Effects
    [SerializeField] private GameObject[] _UseOutlines;
    #endregion

    #region Caches
    private Armory _Armory;
    private Animator _ANM;
    [SerializeField] private SpriteRenderer _SP;
    #endregion


    private void Awake()
    {
        _Armory = GetComponentInParent<Armory>();
        _ANM = GetComponent<Animator>();

        _Armory.OnItemDispense += DispenseItem;
        _Armory.OnEnterRange += EnterRangeEffect;
        _Armory.OnLeaveRange += LeaveRangeEffect;

		_SP.sprite = _Armory.itemToDispense.gameSprite;
    }

    public void DispenseItem()
    {
		_ANM.SetTrigger("Use");
    }

    public void LeaveRangeEffect()
    {
        foreach (GameObject item in _UseOutlines)
        {
            item.SetActive(false);
        }
    }
    public void EnterRangeEffect()
    {
        foreach (GameObject item in _UseOutlines)
        {
            item.SetActive(true);
        }
    }



}
