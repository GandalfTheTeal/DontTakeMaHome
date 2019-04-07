using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public float inspectorNumTrees;

    [SerializeField]
    private static GameObject _gameOverCanvas = null;

    [SerializeField]
    private GameObject WinLetters;
    [SerializeField]
    private GameObject LoseLetters;

    [SerializeField]
    private static GameObject _pauseMenu = null;

    public static int numTrees = 0;
    internal GameObject[] trees = null;

    public static GameObject[] players = new GameObject[4];

    public static float numEnemies = 0;

    public static bool gameOver = false;

    public static bool BearDead;

    public static bool gameOverOff = false;

    public static bool canUseInput = true;

    float timer = 0f;

    public static float numPlayers = 1;

    public static bool isPaused = false;

    private bool _hasRun = true;

    public delegate void LosingEvent();
    public LosingEvent onGameOver;
    public LosingEvent OnLostTree;


    // Use this for initialization
    void Awake()
    {
        TurnOffGameOver();
        var temp = GameObject.FindGameObjectWithTag("GameCanvas");
        Debug.Log(temp != null);

        var _first = temp.GetComponentInChildren<PauseMenuComponent>();
        var _second = _first.gameObject;
        _pauseMenu = _second;

        _gameOverCanvas = GameObject.FindGameObjectWithTag("GameOverCanvas");
        _gameOverCanvas.SetActive(false);

        _pauseMenu.SetActive(false);

        trees = GameObject.FindGameObjectsWithTag("Tree");

        gameOver = false;
        numEnemies = 0;
        canUseInput = true;
        gameOverOff = false;
        isPaused = false;


        numTrees = trees.Length;
        onGameOver += GameOver;
        InputManager.enableInput = true;
        AudioListener.pause = false;
    }

    private void Update()
    {
        inspectorNumTrees = numTrees;
    }

    private void FixedUpdate()
    {
        if (gameOver)
        {
            onGameOver();
            if (BearDead)
            {
                WinLetters.SetActive(true);
                LoseLetters.SetActive(false);
            }

            else
            {
                LoseLetters.SetActive(true);
            }
        }

        if (gameOverOff)
        {
            TurnOffGameOver();
        }

        if (isPaused)
        {
            canUseInput = false;
            Time.timeScale = 0;
            _hasRun = false;
        }
        else
        {
            if (_hasRun == false)
            {
                StartCoroutine(PauseThing());
            }
        }

        //Debug.Log(numTrees);
    }

    public static void AddPlayer()
    {
        numPlayers++;
    }

    private void TurnOffGameOver()
    {
        if (_gameOverCanvas != null) _gameOverCanvas.SetActive(false);
        gameOverOff = false;
        gameOver = false;
        Time.timeScale = 1f;

        WinLetters.SetActive(false);
        LoseLetters.SetActive(false);
        BearDead = false;


    }

    public static void GameOver()
    {
        if (_gameOverCanvas != null) _gameOverCanvas.SetActive(true);
        InputManager.enableInput = false;
        Time.timeScale = .2f;


    }

    public static void DecreaseTreeCount()
    {
        numTrees--;
        if (numTrees <= 0)
        {
            gameOver = true;
        }
    }

    public static void DecreasePlayerCount()
    {
        numPlayers--;
        Debug.Log(numPlayers);
        Debug.Break();
        if (numPlayers <= 0)
        {
            gameOver = true;
        }
    }

    public static void TogglePause()
    {
        if (_pauseMenu)
        {
            if (_pauseMenu.activeInHierarchy == true)
            {
                _pauseMenu.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                isPaused = true;
                _pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    private IEnumerator PauseThing()
    {
        yield return new WaitForSeconds(2f);
        canUseInput = true;
        _hasRun = true;
    }
}
