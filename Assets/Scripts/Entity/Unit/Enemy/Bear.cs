using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Enemy
{
    // Use this for initialization
    void Start()
    {
        // _groundOffsetX = 2.02f;
        // _offsetX = 2.02f;
        // _offsetY = 1;
        // _frontDistance = 1.2f;
    }

    new private void FixedUpdate()
    {
        base.FixedUpdate();
        if (!CheckFront())
        {
            _movM.Move(-1, _moveSpeed, _RB);
        }

        if (_HM.GetHealth() <= 0)
        {
            GameManager.BearDead = true;
            GameManager.gameOver = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GetComponent<HealthManager>().TakeDamage(3);
        }

    }
}
