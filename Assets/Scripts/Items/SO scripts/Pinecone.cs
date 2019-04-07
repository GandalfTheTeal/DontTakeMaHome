using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items/Pinecone")]
public class Pinecone : Item
{

    #region Stats
    [SerializeField] private GameObject _Projectile;
    #endregion

    #region Override Functions
    public override void Pickup(ItemHandler user)
    {

    }

    public override void Drop(ItemHandler user)
    {
        base.Drop(user);
    }

    public override void Activate(ItemHandler user)
    {
        //base.Activate(user);

    }

    public override void OnUseWithStation(ItemHandler user)
    {
        user.LoseItem(this);

    }

    public override void EndAmmuniton(ItemHandler user)
    {
        base.EndAmmuniton(user);
    }

    #endregion




}
