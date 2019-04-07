using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_GFX : MonoBehaviour
{

    #region SerializableFields
    [SerializeField] private GameObject _SplinterEffect;

    [Tooltip("Makeshift Dictionary. The Indexes are paired for Visuals and Health Values")]
    [SerializeField] private GameObject[] _SplinterVisual;
    [SerializeField] private int[] _HealthValuesToAddSplinter;
    #endregion

    #region Caches
    private Tree _Tree;
    private Animator _ANM;
    private HealthManager _HM;
    private AudioSource _AS;
    #endregion

    [Header("Audio")]
    [SerializeField] private AudioClip _takeHit;
    [SerializeField] private AudioClip _Collapse;

    #region UnityFunctions;
    private void Awake()
    {
        _Tree = GetComponent<Tree>();
        _ANM = GetComponent<Animator>();
        _AS = GetComponent<AudioSource>();

    }

    private void Start()
    {
        _HM = _Tree.GetHealthManager();
        _HM.OnTakeDamage += TreeTakeDamageEffect;
        _Tree.OnTreeCollapse += CollapseEffect;

    }
    #endregion



    #region Customfunctions
    public void TreeTakeDamageEffect()
    {
        if (_ANM != null) _ANM.SetTrigger("Hit");
        _AS.PlayOneShot(_takeHit);
        //Checking for Splinters

        for (int i = 0; i < _HealthValuesToAddSplinter.Length; i++)
        {
            // changed tree to healthManager of tree
            if (_HealthValuesToAddSplinter[i] == _HM.GetHealth() && _SplinterVisual[i].activeInHierarchy == false)
            {
                _SplinterVisual[i].SetActive(true);
                Instantiate(_SplinterEffect, _SplinterVisual[i].transform.position, _SplinterVisual[i].transform.rotation);
            }
        }

    }

    public void CollapseEffect()
    {
        _AS.PlayOneShot(_Collapse);
    }


    #endregion


}
