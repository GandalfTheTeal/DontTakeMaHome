using UnityEngine;

public enum ItemType { Berry, Gun, None, Tossable }

public abstract class Item : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite gameSprite;

    [SerializeField]
    private GameObject _Droppable; // Pickup prefab


    //Consumable items take 1 ammo.
    public int ammo = 1;

    public virtual void Pickup(ItemHandler user) {; }

    public virtual void Drop(ItemHandler user)
    {
        CreateGameObject(_Droppable,user);
    }

    public virtual void OnUseWithStation(ItemHandler user)
    {

    }

    public virtual void Activate(ItemHandler user)
    {
        //ammo--;
        //if (ammo <= 0)
        //{
        //    EndAmmuniton(user);
        //    Debug.Log("Ammo = " + ammo);
        //}
        //Debug.Log("Ammo = " + ammo);
    }

    public virtual void EndAmmuniton(ItemHandler user)
    {

        Debug.Log("An item now is without ammo");
    }

    protected void CreateGameObject(GameObject prefab, ItemHandler user)
    {
        var obj = Instantiate(prefab, user.transform.position, user.transform.rotation);

        if (obj.GetComponentInParent<HeavyProjectile>())
        {
            obj.GetComponentInParent<HeavyProjectile>().Shoot((user.GetComponentInParent<Entity>().GetComponentInParent<Rigidbody2D>().velocity) * user.dropForceMultiplier);
        }

        if (obj.GetComponentInParent<Pickup>())
        {
            obj.GetComponentInParent<Pickup>().associatedItem = this;
        }
    }








}
