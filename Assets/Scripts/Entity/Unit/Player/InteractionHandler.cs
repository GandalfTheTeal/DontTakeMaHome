using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionHandler : MonoBehaviour
{
    #region public Lists
    //Things that are in Range. Usually, we should be fine only using the first on the list. We might prefer to do a for each to confirm closest.
    public List<GameObject> InteractionsInRange = new List<GameObject>();


    public IInteractible closestInteraction;
    #endregion 

    #region Caches
    public Entity entity;
    #endregion


    #region Events
    //Called whenever we interact without an item.
    public delegate void InteractEvent(IInteractible interactible);
    public InteractEvent OnInteract;

    // Called whenever we interact with an interactible with an item
    public delegate void UseItemEvent(Item item);
    public UseItemEvent OnUseItem;

    #endregion 

    #region UnityFunctions
    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    private void Update()
    {
        if (closestInteraction != null)
        {
            closestInteraction.OnRange(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IInteractible>() != null)
        {
            InteractionsInRange.Add(other.gameObject);
            //other.GetComponent<IInteractible>().OnBroadcastDestroy += RemoveObject(gameObject);
            closestInteraction = UpdateInteractibles();
        }

        if (closestInteraction != null) closestInteraction.OnRange(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (InteractionsInRange.Contains(other.gameObject))
        {
            InteractionsInRange.Remove(other.gameObject);
            other.GetComponent<IInteractible>().LeaveRange(this);
            closestInteraction = UpdateInteractibles();
        }

        if (closestInteraction != null) closestInteraction.OnRange(this);
    }
    #endregion


    #region Custom Functions

    public IInteractible UpdateInteractibles()
    {
        if (InteractionsInRange.Count <= 0) return null;
        var interaction = InteractionsInRange[0];
        if (interaction == null) return null;

        foreach (GameObject item in InteractionsInRange)
        {
            if (Vector3.Distance(interaction.transform.position, transform.position) > Vector3.Distance(item.transform.position, transform.position)) //TODO VECTOR3 DISTANCE IS BAD PRACTICE
            {
                interaction = item;
            }
        }
        return interaction.GetComponent<IInteractible>();

    }

    public void InteractAttempt(ItemHandler handler)
    {
        closestInteraction = UpdateInteractibles();
        if (closestInteraction == null)
        {
            InteractionsInRange = new List<GameObject>();
            return;
        }

        else
        {
            if (closestInteraction.CanAct() == false) return;
            //closestInteraction.LeaveRange(this);
            if (handler.currentItem != null && closestInteraction.CanReceiveItem())
            {
                InteractWithItem(closestInteraction, handler);
                OnUseItem.Invoke(handler.currentItem);
            }

            else
            {
                InteractWithoutItem(closestInteraction);
            }
        }
    }

    void InteractWithoutItem(IInteractible interactible)
    {
        interactible.Interact(this);
    }


    void InteractWithItem(IInteractible interactible, ItemHandler handler)
    {
        interactible.UseItemOnStation(this, handler.currentItem);
        handler.currentItem.OnUseWithStation(handler);
        handler.AttemptUseItem();
    }
    #endregion



}
