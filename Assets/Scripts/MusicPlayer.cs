using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [Header("Level Music")]
    [SerializeField] AudioClip levelOne;
    [SerializeField] AudioClip levelTwo;
    [SerializeField] AudioClip bossMusic;

    private AudioSource musicPlayer;

    private void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
    }    

    public void PlayMusic(int index)
    {
        switch (index)
        {
            case 1:
                musicPlayer.clip = levelOne;
                break;
            case 2:
                musicPlayer.clip = levelTwo;
                break;
            case 3:
                musicPlayer.clip = bossMusic;
                break;
            default:
                musicPlayer.clip = levelOne;
                break;
        }

        if (!musicPlayer.isPlaying)
        {
            musicPlayer.Play();
        }
    }
}
