using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum StationState { Available, Loaded, Cooldown, Disabled }

public class BaseStation : MonoBehaviour
{
    // useless as of now, might come in handy whenever we do player-specific behavior
    [HideInInspector] public Entity activeUser;
    //public StationState stationState = StationState.Available;



    #region events
    public delegate void InteractEvent();
    public event InteractEvent OnEnterRange;
    public event InteractEvent OnInteract;
    public event InteractEvent OnLeaveRange;
    public event InteractEvent OnUseItem;
    #endregion events

    #region stats
    [SerializeField] private float _StationCooldown = 2f;

    #endregion
    protected bool canAct;
    private float _CurrentCooldown;


    private void FixedUpdate()
    {
        if (!canAct)
        {
            _CurrentCooldown -= Time.deltaTime;
        }
        if (_CurrentCooldown <= 0)
        {
            canAct = true;
            _CurrentCooldown = _StationCooldown;
        }

    }

    public virtual bool CanReceiveItem()
    {
        return false;
    }

    public virtual bool CanAct()
    {
        return canAct;
    }

    public virtual void LeaveRange(InteractionHandler handler)
    {
        if (OnLeaveRange != null) OnLeaveRange.Invoke();
    }

    public virtual void Interact(InteractionHandler handler)
    {
        if (OnInteract != null) OnInteract.Invoke();
        canAct = false;
        if (OnLeaveRange != null) OnLeaveRange.Invoke();
    }

    public virtual void UseItemOnStation(InteractionHandler handler, Item itemToUse)
    {
        if (OnUseItem != null) OnUseItem.Invoke();
        canAct = false;
        if (OnLeaveRange != null) OnLeaveRange.Invoke();
    }

    public virtual void OnRange(InteractionHandler handler)
    {
        //Debug.Log("Entered Range");
        if (OnEnterRange != null && canAct) OnEnterRange.Invoke();
    }
    void BroadcastDestroy()
    {
        //Debug.Log("destroyed" + name);
    }
}
