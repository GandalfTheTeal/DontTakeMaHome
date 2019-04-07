using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBomb : MonoBehaviour
{
    #region stats 
    [SerializeField] protected float _ExplosiveRadius;
    [SerializeField] protected GameObject _ExplosionEffect;
    #endregion

    #region PrivateFunctionality
    protected bool _HasExploded = false;
    protected Collider2D[] _hits;
    #endregion

    #region UnityFunctions
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _ExplosiveRadius);
    }
    #endregion

    #region CustomFunctions
    public virtual void Explode()
    {
        if (_HasExploded) return;

        Instantiate(_ExplosionEffect, transform.position, transform.rotation);
        _hits = Physics2D.OverlapCircleAll(transform.position, _ExplosiveRadius);

    }
    #endregion

}