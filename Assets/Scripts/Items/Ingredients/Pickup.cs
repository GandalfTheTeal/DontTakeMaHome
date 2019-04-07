using UnityEngine;

public class Pickup : MonoBehaviour, IInteractible
{
    public Item associatedItem;

    private SpriteRenderer _SR;

    [SerializeField] private GameObject _UsePointer;
    public event BaseStation.InteractEvent OnBroadcastDestroy;

    private void Start()
    {
        _SR = GetComponentInChildren<SpriteRenderer>();

        if (associatedItem.gameSprite != null)
        {
            _SR.sprite = associatedItem.gameSprite;
        }
    }

    public void OnRange(InteractionHandler handler)
    {
        if (_UsePointer != null) _UsePointer.SetActive(true);
    }

    public void LeaveRange(InteractionHandler handler)
    {
        if (_UsePointer != null) _UsePointer.SetActive(false);
    }

    public void UseItemOnStation(InteractionHandler handler, Item item)
    {

    }

    public bool CanAct()
    {
        return true;
    }

    public bool CanReceiveItem()
    {
        return false;
    }

    public void Interact(InteractionHandler handler)
    {
        if (handler != null)
        {
            handler.GetComponentInParent<ItemHandler>().OnPickup(associatedItem);
            DestroyObject();
        }

    }


    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
