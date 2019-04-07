using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicManager : MonoBehaviour
{

    public static MenuMusicManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
			Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }


    public void Enable()
    {
        this.gameObject.SetActive(true);
    }

}
