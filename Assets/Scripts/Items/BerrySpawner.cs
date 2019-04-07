using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerrySpawner : MonoBehaviour
{
    #region SpawnerStats
    [SerializeField] private float _CooldownBetWeenSpawns = 1.0f;
    [SerializeField] private Item[] _SpawningItems;
    [SerializeField] GameObject Droppable;
    #endregion

    #region
    private SpriteRenderer _SR;
    #endregion

    #region Functionality
    public bool readyToSpawn;
    private float _CurrentCoolDown;
    private Item _CurrentItem;
    private float alphaValue;
    #endregion


    #region Unity Functions

    private void Awake()
    {
        _SR = GetComponent<SpriteRenderer>();
		_CurrentCoolDown = _CooldownBetWeenSpawns;
    }

    private void Start()
    {
        UpdateItem();
    }

    private void FixedUpdate()
    {
        if (!readyToSpawn)
        {
            _CurrentCoolDown -= Time.deltaTime;
            alphaValue = 1 - (_CurrentCoolDown / _CooldownBetWeenSpawns);
            transform.localScale = Vector3.one * alphaValue;
        }

        if (_CurrentCoolDown <= 0 && readyToSpawn == false)
        {
            _CurrentCoolDown = _CooldownBetWeenSpawns;
            readyToSpawn = true;
        }

    }

    #endregion

    #region Custom Functions

    void UpdateItem()
    {
        int currentIndex = (int)Random.Range(0, _SpawningItems.Length);
        _CurrentItem = _SpawningItems[currentIndex];

        _SR.sprite = _CurrentItem.gameSprite;
    }

    public void InitiateSpawn()
    {
		readyToSpawn = false;
        GameObject obj = Instantiate(Droppable, transform.position, Quaternion.identity);
        var pickup = obj.GetComponent<Pickup>();
        if (pickup != null)
        {
            pickup.associatedItem = _CurrentItem;
        }
    }

    #endregion



}
