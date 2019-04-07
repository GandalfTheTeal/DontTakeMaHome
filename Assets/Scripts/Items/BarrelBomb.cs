using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarrelBomb : BaseBomb, IHittable
{
    #region stats 
    [SerializeField] private int _DamageToApply;
    [SerializeField] private float _ExtraRadius;
    [SerializeField] private string[] _tags;
    [SerializeField] private GameObject _ExtraExplosionEffect;

    #endregion

    #region UnityFunctions
    private new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _ExtraRadius);

    }

    #endregion


    #region CustomFunctions

    public void PowerExplode()
    {
        if (_HasExploded) return;
        Instantiate(_ExtraExplosionEffect, transform.position, transform.rotation);
        _hits = Physics2D.OverlapCircleAll(transform.position, _ExtraRadius);
        _HasExploded = true;
        if (_hits == null) return;

        ApplyDamage(_hits);

    }


    public void RawExplode()
    {
        if (_HasExploded) return;
        base.Explode();
        if (_hits == null) return;
        ApplyDamage(_hits);


    }

    public void ApplyDamage(Collider2D[] targets)
    {
        foreach (Collider2D item in targets)
        {
            IHittable hittable = item.GetComponent<IHittable>();
            if ((hittable != null && _tags.Contains(item.gameObject.tag)))
            {
                hittable.TakeDamage(_DamageToApply);
                Destroy(gameObject); // Destroys itself on contact with anything
            }

        }
    }

    public void TakeDamage(float damage)
    {
        PowerExplode();
    }


    #endregion

}