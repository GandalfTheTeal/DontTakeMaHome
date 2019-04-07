using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreasePlayerCount : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private void Start()
    {
        GameManager.numPlayers = 1;
        text.text = "" + GameManager.numPlayers;
        GameManager.players = new GameObject[4];
    }

    public void IncreaseThePlayerCount()
    {
        if (GameManager.numPlayers < 4)
        {
            GameManager.numPlayers++;
            text.text = "" + GameManager.numPlayers;
        }

        else if (GameManager.numPlayers >= 4)
        {
            GameManager.numPlayers = 1;
            text.text = "" + GameManager.numPlayers;
        }
    }
}
