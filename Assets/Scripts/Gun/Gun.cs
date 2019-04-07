using UnityEngine;
using UnityEngine.Networking;


[CreateAssetMenu(fileName = "Items/Gun")]
public class Gun : Item
{
    #region Stats
    [SerializeField]
    private float _coolDown = 1f; // Cooldown until the player can shoot again

    [SerializeField]
    private GameObject _projectile; // Projectile prefab

    [SerializeField]
    private GameObject _blastEffect; // Projectile prefab

    [SerializeField]
    private Transform _origin; // Where the projectile originates

    [SerializeField]
    private Transform _targetDir; // The target direction for the projectile

    private float _timer = 0f; // Keeps time

    private bool _canShoot = true; // Wether the player can shoot or not



    #endregion

    #region overrideFunctions
    public override void Pickup(ItemHandler user)
    {
        _origin = user.origin;
        _targetDir = user.targetDir;
    }

    public override void Drop(ItemHandler user)
    {
        base.Drop(user);
    }

    public override void Activate(ItemHandler user)
    {
        base.Activate(user);
        Shoot(user);
    }

    public override void EndAmmuniton(ItemHandler user)
    {
        base.EndAmmuniton(user);
    }
    #endregion

    private void Shoot(ItemHandler user)
    {
        if (_canShoot)
        {
            _origin = user.origin ;
            _targetDir = user.targetDir;
            Vector2 dir = (_targetDir.position - _origin.position).normalized;
            GameObject obj = Instantiate(_projectile, _origin.position, _origin.rotation);
            obj.GetComponent<Projectile>().Shoot(dir);
            Instantiate(_blastEffect, _origin);
            _timer = _coolDown;
        }
    }



}
