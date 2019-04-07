using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public void Move(float direction, float moveSpeed, Rigidbody2D _RB)
    {
        _RB.velocity = new Vector2(direction * moveSpeed, _RB.velocity.y);
    }


    public void Jump(float jumpSpeed, Rigidbody2D _RB)
    {
        //Debug.Log(GetComponent<InputManager>().tryJump);
        _RB.velocity = new Vector2(_RB.velocity.x, jumpSpeed);
    }
}
