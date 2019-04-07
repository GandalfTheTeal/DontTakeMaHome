using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : BaseStation, IInteractible
{


    #region Serializable fields
    [SerializeField] private Item _ItemToAccept;
    [SerializeField] private BerrySpawner[] spawners;
    public event InteractEvent OnBroadcastDestroy;
    #endregion fields

    #region Caches
    #endregion

    private void Awake()
    {

    }


    public override void Interact(InteractionHandler handler)
    {
        if (!canAct) return;
        base.Interact(handler);

        foreach (BerrySpawner item in spawners)
        {
            if (item.readyToSpawn) item.InitiateSpawn();
        }
    }



}