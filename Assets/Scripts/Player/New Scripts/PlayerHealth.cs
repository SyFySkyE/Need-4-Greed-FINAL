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

    [Header("Sfx Clips")]
    [SerializeField] AudioClip hurtSfx;

    [Header("Sfx Volume")]
    [SerializeField] private float hurtSfxVolume;

    private Animator playerAnim;
    private AudioSource playerAudio;

    private bool vulnerable = true;

    // Start is called before the first frame update
    void Start()
    {
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
            playerAnim.SetTrigger("Hurt");
            playerAudio.PlayOneShot(hurtSfx, hurtSfxVolume);
            hurtVfx.Play();
            healthPoints--;
            UpdateHPText();
            vulnerable = false;
            StartCoroutine(Recover());
        }
        CheckForPlayerDeath();
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
            BroadcastMessage("GameOver");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
