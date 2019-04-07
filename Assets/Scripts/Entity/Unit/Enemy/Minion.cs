using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Enemy
{
    [SerializeField]
    private float _bufferDistance = 1f;

    [SerializeField]
    private float _topRayDistance = 2f;

    [SerializeField]
    private float _checkAboveEnemyY = 2.05f;

    [SerializeField]
    private float _checkAboveEnemyX = 2f;

    [SerializeField]
    private float _checkAboveOffsetY = 2.5f;

    internal delegate void OnJump();
    OnJump onJump;

    public bool above = false;
    public bool forceMove = false;

    public static int MinionCount = 0;

    public bool front = false;

    // Use this for initialization
    void Start()
    {
        // Values that work on a 2x4 collider with the origin in the middle
        //_groundOffsetX = 0.5f;
        //_groundOffsetY = 0.51f;
        //_raycastDistance = 0.1f;
        //_frontDistance = 1.5f;
        //_offsetX = 1.01f;
        //_offsetY = 2f;
        //_topRayDistance = 2.2f;
        //_checkAboveEnemyY = 3.8f;
        //_checkAboveEnemyX = 2.5f;
        //_checkAboveOffsetY = 3.7f;
        //_bufferDistance = _frontDistance;
        

    }

    // Update is called once per frame
    new private void FixedUpdate()
    {
        //Debug.Log(CheckGrounded(_groundOffsetX));
        base.FixedUpdate();
        if (forceMove)
        {
            _movM.Move(-1f, _moveSpeed, _RB);
            return;
        }

        if (!GameManager.gameOver)
        {
            if (!CheckAboveSelf(0.9f) && GameManager.numTrees > 0)
            {
                if (_shouldMove)
                {
                    _movM.Move(-1f, _moveSpeed, _RB);
                    //Debug.Log("Moving");
                    if (CheckFront())
                    {
                        CheckJump();
                    }
                }
            }
        }

        if (transform.position.y < -10)
        {
            GameManager.numEnemies--;
            Destroy(gameObject);
        }
    }

    // Checks in front of it to see if there's an enemy, if there is, checks above them to see if there's an enemy in the way of a jump
    private void CheckJump()
    {
        RaycastHit2D hit = CheckFront();

        if (hit)
        {
            var tag = hit.collider.gameObject.tag;
            if (tag == "Enemy")
            {
                Vector2 enemyCheck = new Vector2(0.4f, 3f);     // Direction of the ray (up right)

                origin.y += _checkAboveEnemyY;
                origin.x -= _checkAboveEnemyX + 0.1f;   // How far left the raycast to detect the top enemy starts out

                Debug.DrawRay(origin, enemyCheck * 2f, Color.blue, 0.01f);
                RaycastHit2D otherHit = Physics2D.Raycast(origin, enemyCheck, 2f);     // Casts a ray one enemy over and one enemy up to detect an enemy

                _onGround = CheckGrounded(-_groundOffsetX) || CheckGrounded(_groundOffsetX);

                if (otherHit == false && _onGround)     // If there is an enemy in front but no enemy on top of them, jump
                {
                    _movM.Jump(_jumpSpeed, _RB);
                }

                else if (otherHit)
                {
                    front = true;
                    _shouldMove = false;
                    if (otherHit.collider.tag == "Enemy")
                    {
                        _shouldTarget = false;
                    }
                }
            }

            else if (tag == "Tree" && hit.distance <= _bufferDistance)     // If the first ray hits a tree
            {
                _shouldMove = false;
                _shouldTarget = true;
            }

            else   // If the first ray hits anything that isn't an enemy or tree
            {
                if ((hit.distance <= _bufferDistance))
                {
                    front = true;
                    _shouldMove = false;
                    _shouldTarget = false;
                }
            }
        }
    }

    private bool CheckAboveSelf(float offset)
    {
        Vector2 origin = GetOrigin();
        origin.y += _checkAboveOffsetY;
        origin.x += offset;

        Debug.DrawRay(origin, _left * _topRayDistance, Color.magenta, 0.05f);
        RaycastHit2D hit = Physics2D.Raycast(origin, _left, _topRayDistance);

        if (hit)
        {
            above = true;
            _shouldMove = false;
            return true;
        }

        else
        {
            above = false;
            _shouldMove = true;
            return false;
        }
    }
}
