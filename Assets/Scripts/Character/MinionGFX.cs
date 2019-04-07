using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionGFX : MonoBehaviour
{
    [SerializeField]
    private GameObject _DamageEffect;
    [SerializeField]
    private GameObject _DeathEffect;
    [SerializeField]
    private Transform effectCenter;
    [SerializeField] private AudioSource _AttackSource;


    private Minion _Mn;
    private Animator _ANM;
    private Rigidbody2D _RB;

    private HealthManager _HM;


    private void Awake()
    {
        _ANM = GetComponentInChildren<Animator>();
        _RB = GetComponent<Rigidbody2D>();
        _HM = GetComponentInChildren<HealthManager>();
        _Mn = GetComponent<Minion>();

        _HM.OnTakeDamage += VisualDamage;
        _HM.OnDie += VisualDie;
        _Mn.OnAttack += VisualAttack;

        AudioSource audioSource = GetComponent<AudioSource>();
        float randomPitch = Random.Range(0.8f,1.2f);
        audioSource.pitch = randomPitch;
    }


    void VisualAttack()
    {
        _ANM.SetTrigger("Attack");
        _AttackSource.PlayOneShot(_AttackSource.clip);
    }

    void VisualDamage()
    {
        _ANM.SetTrigger("Damage");
        Instantiate(_DamageEffect, effectCenter.position, effectCenter.rotation);
    }

    void VisualDie()
    {
        _ANM.SetTrigger("Die");
        Instantiate(_DeathEffect, effectCenter.position, effectCenter.rotation);
        GetComponent<Collider2D>().enabled = false;
    }
}
