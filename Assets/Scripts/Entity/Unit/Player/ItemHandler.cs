using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public Item currentItem;
    public float dropForceMultiplier = 1.4f;

    [SerializeField] private SpriteRenderer _SR;

    public Transform origin;

    public Transform targetDir;

    #region cooldownSystem
    private bool canUse;
    private float _CurrentCooldown;
    [SerializeField] private float _MaxCoolDown = 0.5f;
    #endregion


    public delegate void ItemEvent(Item item);
    public ItemEvent OnPickup;
    public ItemEvent OnDropItem;
    public ItemEvent OnUseItem;


    private void Awake()
    {
        OnPickup += PickupItem;
        OnUseItem += UseItem;
        OnDropItem += LoseItem;
    }

    private void Update()
    {
        if (!canUse)
        {
            _CurrentCooldown -= Time.deltaTime;
        }
        if (_CurrentCooldown <= 0)
        {
            _CurrentCooldown = _MaxCoolDown;
            canUse = true;
        }
    }

    public void AttemptUseItem()
    {
        if (currentItem != null)
        {
            OnUseItem.Invoke(currentItem);
        }

    }

    public void AttemptDrop()
    {
        if (currentItem != null)
        {
            currentItem.Drop(this);
            OnDropItem.Invoke(currentItem);
        }

    }

    public void UseItem(Item item)
    {
        if (canUse)
        {
            currentItem.Activate(this);
            canUse = false;

        }
    }

    public void LoseItem(Item item)
    {
        currentItem = null;
        _SR.sprite = null;
    }

    public void PickupItem(Item item)
    {
        if (currentItem != null)
        {
            AttemptDrop();
        }
        item.Pickup(this);
        currentItem = item;
        _SR.sprite = item.gameSprite;
        _SR.enabled = true;




    }

}
