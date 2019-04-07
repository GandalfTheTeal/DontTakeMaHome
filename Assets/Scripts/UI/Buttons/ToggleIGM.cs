using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleIGM : MonoBehaviour
{
    public void UnPause()
    {
        GameManager.TogglePause();
        GameManager.isPaused = false;
    }
}
