using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearGFX : MonoBehaviour
{
    [SerializeField]
    private GameObject _DamageEffect;
    [SerializeField]
    private GameObject _DeathEffect;
    [SerializeField]
    private Transform effectCenter;


    private Bear _BR;
    private Animator _ANM;
    private Rigidbody2D _RB;

    private HealthManager _HM;


	[SerializeField] private AudioSource _AS;
	[SerializeField] private AudioClip AttackSound;
	[SerializeField] private AudioClip HitSound;
	[SerializeField] private AudioClip DieSound;

    private void Awake()
    {
        _ANM = GetComponentInChildren<Animator>();
        _RB = GetComponent<Rigidbody2D>();
        _HM = GetComponentInChildren<HealthManager>();
        _BR = GetComponent<Bear>();
        _AS = GetComponent<AudioSource>();

        _HM.OnTakeDamage += VisualDamage;
        _HM.OnDie += VisualDie;
        _BR.OnAttack += VisualAttack;

        float randomPitch = Random.Range(0.8f,1.2f);
        _AS.pitch = randomPitch;
    }


    void VisualAttack()
    {
        _ANM.SetTrigger("Attack");
		_AS.PlayOneShot(AttackSound);
    }

    void VisualDamage()
    {
        _ANM.SetTrigger("Damage");
        Instantiate(_DamageEffect, effectCenter.position, effectCenter.rotation);
		_AS.PlayOneShot(HitSound);
    }

    void VisualDie()
    {
        _ANM.SetTrigger("Die");
		_AS.PlayOneShot(DieSound);
        Instantiate(_DeathEffect, effectCenter.position, effectCenter.rotation);
        GetComponent<Collider2D>().enabled = false;
    }
}
