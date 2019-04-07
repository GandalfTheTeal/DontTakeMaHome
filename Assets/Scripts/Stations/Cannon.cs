using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : BaseStation, IInteractible
{

    #region Serializable fields
    [SerializeField] private Item _ItemToAccept;
    #endregion fields

    #region Events
    public event InteractEvent OnLoadCannon;
    public event InteractEvent OnBroadcastDestroy;
    #endregion

    #region Caches
    private Cannon_Shoot _CannonShoot;
    private Transform _CannonPivot;
    #endregion


    private void Awake()
    {
        _CannonShoot = GetComponentInChildren<Cannon_Shoot>();
    }



    public override void Interact(InteractionHandler handler)
    {
        base.Interact(handler);
        if (_CannonShoot.lockedAndReady)
        {
            _CannonShoot.OnShoot();
        }
        else
        {
            _CannonShoot.OnFailedShoot();
        }
    }

    public override bool CanReceiveItem()
    {
        return !_CannonShoot.lockedAndReady;
    }


    public override void UseItemOnStation(InteractionHandler handler, Item useItem)
    {
        base.UseItemOnStation(handler, useItem);

        if (_CannonShoot.lockedAndReady == false)
        {

            if (_ItemToAccept.itemName == useItem.itemName)
            {
                _CannonShoot.lockedAndReady = true;
                canAct = true;
               // stationState = StationState.Loaded;
                if (OnLoadCannon != null) OnLoadCannon.Invoke();
            }
        }
    }


}