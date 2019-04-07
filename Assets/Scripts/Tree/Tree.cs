using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tree_GFX))]
public class Tree : Entity
{

    public delegate void OnTreeDown();
    OnTreeDown treeDown;

    private ICollapsible[] _ThingsToCollapse;
    public delegate void CollapseEvent();
    public CollapseEvent OnTreeCollapse;

    new private void Awake()
    {
        base.Awake();
        _HM.OnDie += Collapse;
        
        _ThingsToCollapse = GetComponentsInChildren<ICollapsible>();
        OnTreeCollapse += GameManager.DecreaseTreeCount;
    }


    public void Collapse()
    {
        GetComponent<Collider2D>().enabled = false;
        OnTreeCollapse.Invoke();
        foreach (var item in _ThingsToCollapse)
        {
            item.Collapse();
        }
    }


    #region DEBUGGERFUNCTIONS
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _HM.TakeDamage(1f);
        }
    }
    #endregion

}
