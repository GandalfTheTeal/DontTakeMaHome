using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
    

    protected Rigidbody2D _RB;
    protected BoxCollider2D _BC;
    protected Animator _anim;
    protected HealthManager _HM;

    public delegate void OnTakeDamage();
    OnTakeDamage onTakeDamage;

    // Use this for initialization
    public virtual void Awake()
    {
        _RB = GetComponent<Rigidbody2D>();
        _BC = GetComponent<BoxCollider2D>();
        _HM = GetComponent<HealthManager>();
    }

    public HealthManager GetHealthManager()
    {
        return _HM;
    }

    public void TakeDamage()
    {
        onTakeDamage();
        
    }
}
