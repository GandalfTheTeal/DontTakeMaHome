using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class HeavyProjectile : MonoBehaviour
{
    #region stats 
    [SerializeField] private string[] _TagsToCollide;
    [SerializeField] private GameObject _TrailEffect;
    [SerializeField] private float _ForceMultiplier = 1.0f;
    private bool _Collided = false;
    #endregion

    #region Cache
    private Rigidbody2D _RB;
    #endregion

    #region events
    public UnityEvent OnCollide;
    #endregion

    private void Awake()
    {
        _RB = GetComponent<Rigidbody2D>();
        if (_TrailEffect != null) Instantiate(_TrailEffect, transform);
    }

    public void Shoot(Vector2 force)
    {
        _RB.velocity = force * _ForceMultiplier;
    }

     private void OnCollisionEnter2D(Collision2D col)
    {
        if (_Collided) return;
        foreach (string item in _TagsToCollide)
        {

            if (col.collider.CompareTag(item))
            {
                OnCollide.Invoke();
                _Collided = true;
                return;
            }


        }
    }


}
