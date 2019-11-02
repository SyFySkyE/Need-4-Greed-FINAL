using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Components")]
    [SerializeField] private int healthPoints = 3;
    [SerializeField] private float recoveryTime = 1.5f;
    [SerializeField] private TextMeshProUGUI hPText;

    [Header("Particle System Prefabs")]
    [SerializeField] private ParticleSystem hurtVfx;
    [SerializeField] private ParticleSystem deathVfx;

    [Header("Sfx Clips")]
    [SerializeField] AudioClip hurtSfx;
    [SerializeField] AudioClip deathSfx;

    [Header("Sfx Volume")]
    [SerializeField] private float hurtSfxVolume = 1f;
    [SerializeField] private float deathSfxVolume = 3f;

    private Animator playerAnim;
    private AudioSource playerAudio;
    private const int initHP = 3; // Health gets set back to full upon restart, checkpoint pass
    public int InitHP { get { return this.InitHP; } }    
    private bool vulnerable = true;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        UpdateHPText();
    }

    private void UpdateHPText()
    {
        hPText.text = healthPoints.ToString();
    }

    private void PlayerHurt()
    {
        if (vulnerable)
        {
            vulnerable = false;
            playerAnim.SetTrigger("Hurt");
            playerAudio.PlayOneShot(hurtSfx, hurtSfxVolume);
            hurtVfx.Play();
            healthPoints--;
            UpdateHPText();
            CheckForPlayerDeath();
            StartCoroutine(Recover());
        }                
    }

    private void Restart()
    {
        playerAnim.SetTrigger("Death_t");
        playerAudio.PlayOneShot(deathSfx, deathSfxVolume);
        deathVfx.Play();
    }

    private IEnumerator Recover()
    {
        yield return new WaitForSeconds(recoveryTime);
        vulnerable = true;
    }

    private void CheckForPlayerDeath()
    {
        if (healthPoints <= 0)
        {
            BroadcastMessage("Restart");
        }
    }

    public int GetHealth()
    {
        return this.healthPoints;
    }

    private void Respawn()
    {
        ResetPlayerHealth();
    }

    public void ResetPlayerHealth()
    {
        this.healthPoints = initHP;
        UpdateHPText();
    }
}
