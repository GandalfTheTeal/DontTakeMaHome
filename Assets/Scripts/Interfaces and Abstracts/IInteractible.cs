using UnityEngine;

public interface IInteractible
{
    bool CanReceiveItem();

    bool CanAct();

    //Called when it enters a player's Range - CONSIDER PUTTING THE ENTITY AS A PARAMETER
    void OnRange(InteractionHandler handler);


    //called when it exits a player's Range - CONSIDER PUTTING THE ENTITY AS A PARAMETER
    void LeaveRange(InteractionHandler handler);


    // Called when an entity calls the interact function on it. 
    void Interact(InteractionHandler handler);


    //Called when entity holding an item calls the interact function on it.
    void UseItemOnStation(InteractionHandler handler, Item itemToUse);



}

