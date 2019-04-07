using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{

    [SerializeField]
    private GameObject _player;
    [SerializeField] private float _NewCameraWeight = 1;
    [SerializeField] private float _NewCameraRadius = 40;
    public static bool _hasBeenInstantiated = false;

    // Use this for initialization
    void Start()
    {
        InputManager.playerCount = 0;
        InstantiatePlayers();
        _hasBeenInstantiated = true;
    }

    private void FixedUpdate()
    {
        if(!_hasBeenInstantiated)
        {
            InstantiatePlayers();
            _hasBeenInstantiated = true;
        }
    }

    public void InstantiatePlayers()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        for (int i = 0; i < GameManager.numPlayers; i++)
        {
            InputManager.playerCount = i + 1;
            //Debug.Log(InputManager.playerCount);
            var obj = Instantiate(_player, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
            obj.GetComponent<InputManager>().playerID = i+1;
            
            GameManager.players[i] = obj;
            Debug.Log("yes");

            var targetGroup = GameObject.FindObjectOfType<Cinemachine.CinemachineTargetGroup>();

            var targets = targetGroup.m_Targets;
            List<Cinemachine.CinemachineTargetGroup.Target> list = new List<Cinemachine.CinemachineTargetGroup.Target>(targets);

            var newTarget = new Cinemachine.CinemachineTargetGroup.Target();
            newTarget.target = obj.transform;
            newTarget.weight = _NewCameraWeight;
            newTarget.radius = 40;
            list.Add(newTarget);
            targetGroup.m_Targets = list.ToArray();
        }
    }
}
