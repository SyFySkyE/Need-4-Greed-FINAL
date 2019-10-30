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

    private AudioSource cpAudio;
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        cpAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        coinReqText.text = coinReq.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerCoinCollector>().GetCoinsCollected() >= coinReq)
            {
                cpAudio.PlayOneShot(passSfx, passSfxVolume);
                passVfx.Play();
            }
            else
            {
                playerAnim.SetTrigger("Death_t");
                other.GetComponent<PlayerStateManager>().CurrentState = PlayerState.Dead;
            }
        }
    }
}
