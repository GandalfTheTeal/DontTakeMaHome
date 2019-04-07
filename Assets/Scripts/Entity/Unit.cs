using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//  Base Unit class that all things that move and have health inherit from
//

[RequireComponent(typeof(MovementManager))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Unit : Entity
{
    [SerializeField]
    protected float _jumpSpeed = 20f;

    [SerializeField]
    protected float _moveSpeed = 10f;

    [SerializeField]
    protected float _groundOffsetX = 0.5f;

    [SerializeField]
    protected float _raycastDistance = 0.1f;

    [SerializeField]
    protected float _groundOffsetY = 1.01f;

    protected bool _onGround = false;
    protected bool _goingRight = false;

    protected float _direction = 0;

    public delegate void OnAttack();
    public OnAttack onAttack;

    protected MovementManager _movM;

    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        _RB = GetComponent<Rigidbody2D>();
        _movM = GetComponent<MovementManager>();
    }

    protected bool CheckGrounded(float offSet)
    {
        Vector2 origin = transform.position;
        origin.x += offSet;
        origin.y -= _groundOffsetY;

        Debug.DrawRay(origin, Vector2.down * _raycastDistance, Color.red, 0.01f);

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, _raycastDistance);

        if (hit)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    protected void CheckFlip(float direction)
    {
        if (direction == 0)
        {
            return;
        }

        bool newGoingRight = direction > 0;

        if (newGoingRight != _goingRight)
        {
            _goingRight = newGoingRight;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
}
