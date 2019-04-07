using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armory : BaseStation, IInteractible
{
    public Item itemToDispense;

    #region Serializable fields
    [SerializeField] private Vector2 _ForceToAdd;
    [SerializeField] private int ammo;
    [SerializeField] private bool usesAmmo;
    #endregion fields

    #region Events
    public event InteractEvent OnItemDispense;
    #endregion


    public new void Interact(InteractionHandler handler)
    {
        base.Interact(handler);
        if (handler != null)
        {
            handler.GetComponentInParent<ItemHandler>().OnPickup(itemToDispense);
            if (OnItemDispense != null) OnItemDispense.Invoke();
            ammo = usesAmmo ? ammo-- : ammo;
            //	TO DO AMMO SYSTEM
        }

    }



}
