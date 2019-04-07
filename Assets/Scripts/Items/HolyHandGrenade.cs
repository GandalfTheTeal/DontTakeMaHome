using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HolyHandGrenade : BaseBomb
{
    #region stats 
    [SerializeField] private int _HealingToApply;
    [SerializeField] private float _ExtraRadius;
    [SerializeField] private LayerMask _layer;

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

    public void RawExplode()
    {
        if (_HasExploded) return;
        base.Explode();
        if (_hits == null) return;
        ApplyDamage(_hits);
    }

    public void ApplyDamage(Collider2D[] targets)
    {
        Debug.Log("1");
        foreach (Collider2D item in targets)
        {
            Debug.Log("2: " + item.gameObject.tag );
            if (item != null && item.tag == "Player")
            {
                Debug.Log("3");
                Debug.Log("Collision " + item.name + "ShouldHeal");

                item.gameObject.GetComponent<HealthManager>().GainHealth(_HealingToApply);
                
                Destroy(gameObject); // Destroys itself on contact with anything
            }
        }
    }


    #endregion

}