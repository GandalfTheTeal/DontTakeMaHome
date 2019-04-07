using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public void LoadScene(int scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);//, LoadSceneMode.Single);
        //GameManager.players = null;
        GameManager.gameOverOff = true;
    }

    public void EnableMainMenuMusic()
    {
        MenuMusicManager.Instance.Enable();
    }

     public void DisableMainMenuMusic()
    {
        MenuMusicManager.Instance.Disable();
    }
}
