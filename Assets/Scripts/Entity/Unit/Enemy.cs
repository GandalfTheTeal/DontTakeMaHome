using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Base enemy class that all other enemies inherit from, no Game Object directly uses this
//

public abstract class Enemy : Unit
{
    [SerializeField]
    protected float _damage = 1f;   // Should be fairly obvious

    [SerializeField]
    protected float _timeOut = 1f;  // Attack time cooldown

    [SerializeField]
    protected float _frontDistance = 1f; // Distance the raycast to check in front should go

    [SerializeField]
    protected float _offsetX = 1.01f; // Distance from origin/pivot to the left

    [SerializeField]
    protected float _offsetY = 0f;  // Distance from origin/pivot to the top

    protected Vector2 _left = new Vector2(-1, 0);   // Left in vector format

    protected bool _shouldTarget = true;    // Whether the enemy should target or if it's a waste of resources || Don't think it's currently working properly

    protected bool _shouldMove = true;      // Whether the enemy shoul dtry moving or if it's just going to push the enemy in front

    protected float _timer = 0f;        // Time left for the enemy attack

    protected GameObject _target = null;        // Target for the enemy attack

    protected Vector2 origin;       // Where the raycasts serive their starting point from


    public delegate void AttackEvent();
    public AttackEvent OnAttack;

    #region Unity Functions

    public override void Awake()
    {
        base.Awake();
        OnAttack += Attack;
        //TODO
        GetComponentInChildren<HealthManager>().OnDie += Die;
    }

    protected void FixedUpdate()
    {
        if (!GameManager.gameOver)    // If the game isn't over
        {
            if (_timer <= 0) // If target is null and attack timer is done
            {
                _target = CheckForTarget();     // Try to set target
                if (_target != null)
                {
                    OnAttack.Invoke();
                    _timer = _timeOut;
                }
            }

            else if (_timer > 0)        // If the attack timer isn't done
            {
                _timer -= Time.deltaTime;       // Decrease attack timer by the difference between the last and the current frame
            }
        }
    }
    #endregion
    #region My Functions

    protected GameObject CheckForTarget()
    {
        RaycastHit2D hit = CheckFront();

        if (hit)
        {
            string tag = hit.collider.gameObject.tag;
            if ((tag == "Tree" || tag == "Player") && _timer <= 0)
            {
                return hit.collider.gameObject;
            }

            else
            {
                return null;
            }
        }

        else
        {
            return null;
        }
    }

    protected Vector2 GetOrigin()       // Get and return the origin of the current Game Object
    {
        origin = transform.position;
        return origin;
    }

    protected void Attack()
    {
        if (onAttack != null)
        {
            onAttack();
        }
        _target.GetComponent<HealthManager>().TakeDamage(_damage);      // Do damage to the health manager of the target
        //Debug.Log("I has attacken");
    }

    protected RaycastHit2D CheckFront()
    {
        Vector2 origin = GetOrigin();
        origin.y += _offsetY;
        origin.x -= _offsetX;

        Debug.DrawRay(origin, _left, Color.black, 0.05f);
        RaycastHit2D hit = Physics2D.Raycast(origin, _left, _frontDistance);        // Raycast directly left by specified distance

        return hit;     // Return all the data from the raycast hit
    }


    public void Die()
    {
        Destroy(gameObject, 5f);
    }

    #endregion
}