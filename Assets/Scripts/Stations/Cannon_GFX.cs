using UnityEngine;

public class Cannon_GFX : MonoBehaviour
{
    #region SerializableEffects
    [SerializeField] private GameObject _ShootingEffect;
    [SerializeField] private GameObject _FailedEffect;
    [SerializeField] private GameObject _DestroyEffect;
    [SerializeField] private GameObject _LoadingEffect;
    [SerializeField] private GameObject _CollapseEffect;


    [SerializeField] private GameObject[] _ShootReadyOutline;
    [SerializeField] private GameObject[] _LoadReadyOutline;
    #endregion

    #region Caches
    private Cannon_Shoot _CannonShoot;
    private Cannon _Cannon;
    private Animator _ANM;
    #endregion

    #region UnityFunctions
    void Awake()
    {
        _CannonShoot = GetComponentInChildren<Cannon_Shoot>();
        _Cannon = GetComponentInChildren<Cannon>();
        _ANM = GetComponentInChildren<Animator>();
        _CannonShoot.OnShoot += ShotEffect;
        _CannonShoot.OnFailedShoot += FailedEffect;

        _Cannon.OnEnterRange += EnterRangeEffect;
        _Cannon.OnLeaveRange += LeaveRangeEffect;

        _Cannon.OnUseItem += UseItemEffect;


    }

    #endregion

    #region CustomFunctions

    public void ShotEffect()
    {
        Instantiate(_ShootingEffect, _CannonShoot.weaponBarrel);
        _ANM.SetTrigger("Trigger");
    }

    public void FailedEffect()
    {
        // Shooting Failed
    }

    public void DestroyEffect()
    {
        Instantiate(_DestroyEffect, _CannonShoot.weaponBarrel);
    }

    public void UseItemEffect()
    {
        // LOADING!
    }

    public void EnterRangeEffect()
    {
        if (_CannonShoot.lockedAndReady)
        {
            foreach (GameObject item in _ShootReadyOutline)
            {
                item.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject item in _LoadReadyOutline)
            {
                item.SetActive(true);

            }
        }
    }

    public void LeaveRangeEffect()
    {
        foreach (GameObject item in _ShootReadyOutline)
        {
            item.SetActive(false);

        }

        foreach (GameObject item in _LoadReadyOutline)
        {
            item.SetActive(false);

        }
    }

    #endregion


}