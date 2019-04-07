using UnityEngine;

[CreateAssetMenu(fileName = "Items/Berry")]
public class Berry : Item
{
    #region Stats
    public int healthRegen;
    #endregion

    #region overrideFunctions
    public override void Pickup(ItemHandler user)
    {

    }

    public override void Drop(ItemHandler user) {; }

    public override void Activate(ItemHandler user)
    {
        base.Activate(user);
       user.GetComponentInParent<HealthManager>().GainHealth(healthRegen);
        user.LoseItem(this);
      
    }

    public override void EndAmmuniton(ItemHandler user)
    {
        base.EndAmmuniton(user);
        Debug.Log("A berry is without ammo");
    }
    #endregion
}
