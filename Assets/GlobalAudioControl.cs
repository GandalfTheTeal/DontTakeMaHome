using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudioControl : MonoBehaviour
{

    [SerializeField] private GameManager _GM;


    [SerializeField] private AudioSource _MainMusicSource;
    [SerializeField] private AudioSource _AmbientSource;

    [SerializeField] private AudioClip _HighIntense;
    [SerializeField] private AudioClip _LowIntense;
    [SerializeField] private AudioClip _DefeatIntense;


    [SerializeField] private AudioClip _DefeatLoop;
    [SerializeField] private AudioClip _WinLoop;


    private bool lowIntense = true;


    private void Start()
    {
        _GM = GameObject.FindObjectOfType<GameManager>();
        _GM.onGameOver += PlayEnd;
    }

    private void Update()
    {
        if (GameManager.numTrees < 3 && lowIntense)
        {
            PlayHighIntense();
            lowIntense = false;
        }
    }


    public void PlayHighIntense()
    {
        _MainMusicSource.Stop();
        _MainMusicSource.clip = _HighIntense;
        _MainMusicSource.Play();
    }
    public void PlayEnd()
    {
        if (!GameManager.BearDead)
        {
            _MainMusicSource.PlayOneShot(_DefeatIntense);
            _MainMusicSource.clip = _DefeatLoop;
            _MainMusicSource.volume = 1;

        }
        if (GameManager.BearDead)
        {
            _MainMusicSource.clip = _WinLoop;
            _MainMusicSource.volume = 1;
        }
        _MainMusicSource.Play();
        _AmbientSource.Stop();
        GetComponentInChildren<AudioListener>().gameObject.transform.Translate(1000, 0, 1000);
        _GM.onGameOver -= PlayEnd;

    }
}
