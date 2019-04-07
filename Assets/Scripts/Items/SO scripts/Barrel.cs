using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items/Barrel")]
public class Barrel : Item {

	#region stats
	public GameObject tossable;

	#endregion


	#region overrideFunctions
    public override void Pickup(ItemHandler user)
    {

    }

    public override void Drop(ItemHandler user) {base.Drop(user); }

    public override void Activate(ItemHandler user)
    {
        base.Activate(user);
        CreateGameObject(tossable,user);
        user.LoseItem(this);
      
    }

    public override void EndAmmuniton(ItemHandler user)
    {
        base.EndAmmuniton(user);
        Debug.Log("A berry is without ammo");
    }
    #endregion
}
