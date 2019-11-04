using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Checkpoint : MonoBehaviour
{
    [Header("Coin Requirement")]
    [SerializeField] private int coinReq;
    [SerializeField] TextMeshPro coinReqText;

    [Header("Player Dependency")]
    [SerializeField] private GameObject player;

    [Header("Respawn Attributes")]
    [SerializeField] private float zPosToRespawn = 0f;
    [SerializeField] private float secondsBeforeRespawn = 2f;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip passSfx;
    [SerializeField] private float passSfxVolume = 1f;
    [SerializeField] private AudioClip failSfx;
    [SerializeField] private float failSfxVolume = 3f;

    [Header("Particle System")]
    [SerializeField] private ParticleSystem passVfx;

    [Header("Music Player Dependency")]
    [SerializeField] private MusicPlayer musicController;
    [SerializeField] private int nextTrackNumberToPlay;

    private AudioSource cpAudio;

    // Start is called before the first frame update
    void Start()
    {
        cpAudio = GetComponent<AudioSource>();
        coinReqText.text = coinReq.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.layer = 11; // Don't collide
            if (other.GetComponent<PlayerCoinCollector>().GetCoinsCollected() >= coinReq)
            {
                PlayCompletition();
                other.GetComponent<PlayerHealth>().ResetPlayerHealth();
                other.GetComponent<PlayerStateManager>().SetTransformLocation(this.transform.position);
            }
            else
            {
                other.GetComponent<PlayerStateManager>().BroadcastMessage("Restart");            
            }
        }
    }

    private void PlayCompletition()
    {
        cpAudio.PlayOneShot(passSfx, passSfxVolume);
        passVfx.Play();
        PlayNextSong();
    }

    private void PlayNextSong()
    {
        musicController.PlayMusic(nextTrackNumberToPlay);
    }
}
