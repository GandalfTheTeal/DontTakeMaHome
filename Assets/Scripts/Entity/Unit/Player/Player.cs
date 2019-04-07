using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(CharacterAnimator))]
public class Player : Unit
{
    private Vector2 _initialPosition;

    private InputManager _IM;

    private InteractionHandler _InteractionH;
    private ItemHandler _ItemH;

    public UnityEvent DieEvent;
    public UnityEvent ReviveEvent;

    // Use this for initialization
    public override void Awake()
    {
        base.Awake();

        _IM = GetComponent<InputManager>();
        _InteractionH = GetComponentInChildren<InteractionHandler>();
        _ItemH = GetComponentInChildren<ItemHandler>();



    }

    private void Start()
    {
        _HM.OnDie += DecreasePlayerCount;
        _HM.OnDie += OtherDieStuff;
        _HM.OnRevive += IncreasePlayerCount;
        _HM.OnRevive += OtherReviveStuff;

    }

    private void Update()
    {
        if (_HM.GetIsAlive())
        {
            _direction = _IM.xInput;
        }
    }

    private void FixedUpdate()
    {
        if (_HM.GetIsAlive())
        {
            _onGround = CheckGrounded(_groundOffsetX) || CheckGrounded(-_groundOffsetX);
            if (!GameManager.isPaused)
            {
                UseInput();
            }
            CheckFlip(_IM.xInput);
        }
    }

    private void DecreasePlayerCount()
    {
        GameManager.numPlayers--;
    }

    private void IncreasePlayerCount()
    {
        GameManager.numPlayers++;
    }

    private void OtherDieStuff()        // TODO: Need to refactor eventually
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        var targetGroup = GameObject.FindObjectOfType<Cinemachine.CinemachineTargetGroup>();

        var targets = targetGroup.m_Targets;
        
        _IM.enabled = false;
        _IM.xInput = 0;
        _RB.velocity = Vector3.zero;

        DieEvent.Invoke();
        
        if (_IM.playerID == 1) return;
         List<Cinemachine.CinemachineTargetGroup.Target> list = new List<Cinemachine.CinemachineTargetGroup.Target>(targets); 
        foreach (var item in targets)
        {
            if (item.target.transform == this.transform)
            {
                list.Remove(item);
                break;
            }
        }
        targetGroup.m_Targets = list.ToArray();
    }

    private void OtherReviveStuff()        // TODO: Need to refactor eventually
    {
        ReviveEvent.Invoke();

        var targetGroup = GameObject.FindObjectOfType<Cinemachine.CinemachineTargetGroup>();

        var targets = targetGroup.m_Targets;
        List<Cinemachine.CinemachineTargetGroup.Target> list = new List<Cinemachine.CinemachineTargetGroup.Target>(targets);

        var newTarget = new Cinemachine.CinemachineTargetGroup.Target();
        newTarget.target = this.gameObject.transform;
        newTarget.weight = 40;
        newTarget.radius = 40;
        list.Remove(newTarget);
        targetGroup.m_Targets = list.ToArray();
    }

    private void UseInput()
    {
        _movM.Move(_IM.xInput, _moveSpeed, _RB);

        if (_IM.tryInteract)
        {
            _InteractionH.InteractAttempt(_ItemH);
        }

        if (_IM.tryUse)
        {
            _ItemH.AttemptUseItem();
        }

        if (_IM.drop)
        {
            _ItemH.AttemptDrop();
        }

        if (_IM.tryJump && _onGround)
        {
            //Debug.Log("jump");
            _movM.Jump(_jumpSpeed, _RB);
        }

        _IM.tryJump = false;
        _IM.tryInteract = false;
        _IM.tryUse = false;
        _IM.drop = false;
    }
}
