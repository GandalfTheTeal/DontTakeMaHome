using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VFSBumper : MonoBehaviour
{

    public Texture bumperImg;

    public string loadLevel;

    Rect screenRect;
    Rect cropRect;

    public float holdTime = 5f;
    public float fadeTime = 2f;
    float fadeElapsed;

    void Start()
    {
        if (!bumperImg)
        {
            Debug.LogError("VFSBumper.Start() " + name + " bumper Img unassigned!");
            enabled = false;
        }
    }

    void Update()
    {
        if (holdTime > 0f)
            holdTime -= Time.deltaTime;

        if (holdTime <= 0f)
            fadeElapsed += Time.deltaTime;

        if (fadeElapsed >= fadeTime || Input.anyKey)
        {
            if (!string.IsNullOrEmpty(loadLevel))
                SceneManager.LoadScene(loadLevel);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnGUI()
    {
        Rect centreRect = new Rect((Screen.width * 0.5f) - (bumperImg.width * 0.5f), (Screen.height * 0.5f) - (bumperImg.height * 0.5f), bumperImg.width, bumperImg.height);

        GUI.color = new Color(1f, 1f, 1f, 1f - (fadeElapsed / fadeTime));
        GUI.DrawTexture(centreRect, bumperImg);
        GUI.color = Color.white;
    }

}
