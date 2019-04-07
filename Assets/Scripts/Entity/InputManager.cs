using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public int playerID = 0;
    public static int playerCount = 0;
    public static bool enableInput = true;

    public float xInput = 0;
    public bool tryInteract = false;
    public bool tryUse = false;
    public bool tryJump = false;
    public bool drop = false;
    public bool start = false;
    

    // Use this for initialization
    void Start()
    {
        Debug.Log(playerID);
        foreach (SpriteRenderer SP in GetComponentsInChildren<SpriteRenderer>())
        {
            SP.sortingOrder = SP.sortingOrder + (playerID * 10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<HealthManager>().GetIsAlive()) return;
        if (enableInput == false) return;

        if(GameManager.canUseInput)
        {
            xInput = Input.GetAxis("Horizontal" + "_" + playerID); // Gets horizontal movement
            tryJump |= Input.GetKeyDown("joystick " + playerID + " button 0"); // A - Jump
            tryInteract |= Input.GetKeyDown("joystick " + playerID + " button 2"); // X - Interact
            tryUse |= Input.GetKeyDown("joystick " + playerID + " button 3"); // Y - Use
            drop |= Input.GetKeyDown("joystick " + playerID + " button 1"); // B - Drop
            start = Input.GetKeyDown("joystick " + playerID + " button 7");
        }

        if (start)
        {
            GameManager.TogglePause();
        }
    }
}
