using UnityEngine;
using UnityEngine.Networking;

public class Cannon_Shoot : NetworkBehaviour

{
    #region public variables
    public Transform weaponBarrel;
    public bool lockedAndReady = false;
    #endregion

    #region Stats
    [SerializeField] private float _Force;
    private Vector2 _AngleOfShot;
    #endregion


    #region events
    public delegate void ShootEvent();
    public ShootEvent OnShoot;
    public ShootEvent OnFailedShoot;

    #endregion

    #region Serializable variables

    [SerializeField] private GameObject _Projectile;

    #endregion


    #region UnityFunctions

    private void Awake()
    {
        _AngleOfShot = new Vector2(weaponBarrel.rotation.x, weaponBarrel.rotation.y);
        OnShoot += Shoot;
    }
    #endregion


    #region CustomFunctions

    public void Shoot()
    {
        if (lockedAndReady)
        {
            var obj = Instantiate(_Projectile, weaponBarrel.position, weaponBarrel.rotation);
            lockedAndReady = false;

            if (obj.GetComponentInParent<HeavyProjectile>())
            {
                obj.GetComponentInParent<HeavyProjectile>().Shoot( obj.transform.right * _Force);
            }
        }

    }


    #endregion


}
