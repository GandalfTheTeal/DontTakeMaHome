using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _ANMUpper;
    [SerializeField] private Animator _ANMLower;
    public SpriteRenderer scarf;


    private Rigidbody2D _RB;

    private ItemHandler _ItemHandler;
    private InteractionHandler _InteractionHandler;
    private HealthManager _HM;

    [Header("AudioFiles")]
    private AudioSource _AS;
    [SerializeField] private AudioClip _EatBerry;
    [SerializeField] private AudioClip _Die;
    [SerializeField] private AudioClip _TakeHit;
    [SerializeField] private AudioClip _Jump;


    private void Awake()
    {
        _ItemHandler = GetComponentInChildren<ItemHandler>();
        _InteractionHandler = GetComponentInChildren<InteractionHandler>();
        _RB = GetComponent<Rigidbody2D>();
        _HM = GetComponentInChildren<HealthManager>();
        _AS = _ANMUpper.GetComponent<AudioSource>();

        _ItemHandler.OnUseItem += UseItem;
        _ItemHandler.OnPickup += UpdateItem;
        _InteractionHandler.OnUseItem += UseItem;
        _HM.OnDie += VisualDie;
        _HM.OnRevive += VisualRevive;
        _HM.OnTakeDamage += VisualDamage;
    }

    public void Update()
    {
        _ANMLower.SetFloat("Speed", Mathf.Abs(_RB.velocity.x));
        _ANMLower.SetFloat("Vertical", Mathf.Abs(_RB.velocity.y));
    }

    public void UseItem(Item item)
    {
        _ANMUpper.SetTrigger("UsedItem");
        if (item != null && item.itemName == "RaspBerry")
        {
            _AS.PlayOneShot(_EatBerry);
        }
    }

    public void UpdateItem(Item newItem)
    {
        _ANMUpper.SetBool("HoldGun", newItem.itemType == ItemType.Gun ? true : false);
        _ANMUpper.SetBool("HoldBerry", newItem.itemType == ItemType.Berry ? true : false);
    }

    public void VisualDamage()
    {
        _AS.PlayOneShot(_TakeHit);
    }


    public void VisualDie()
    {
        _ANMUpper.SetTrigger("Die");
        _ANMLower.SetTrigger("Die");
        _AS.PlayOneShot(_Die);
        GameManager.DecreasePlayerCount();
    }

    public void VisualRevive()
    {
        _ANMUpper.SetTrigger("Revive");
        _ANMLower.SetTrigger("Revive");
    }



}
