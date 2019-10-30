using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthController : MonoBehaviour
{
    [Header("Player Health Components")]
    [SerializeField] private int healthPoints = 3;
    [SerializeField] private TextMeshProUGUI hpText;

    private PlayerManager playerManager;
    private bool vulnerable = true;

    [SerializeField] private float secondsBeforeLoad = 3f; // TODO take out... Right?

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        UpdateHPText();
    }

    private void UpdateHPText()
    {
        hpText.text = healthPoints.ToString();
    }

    private void PlayerHurt()
    {
        if (vulnerable)
        {
            healthPoints--;
            UpdateHPText();
            vulnerable = false;
            StartCoroutine(Recover());
        }
        CheckForPlayerDeath();
    }    

    private IEnumerator Recover()
    {
        yield return new WaitForSeconds(playerManager.RecoveryTime);
        vulnerable = true;
    }

    private void CheckForPlayerDeath()
    {
        if (healthPoints <= 0)
        {
            CommencePlayerDying();
        }
    }

    public void CommencePlayerDying()
    {
        if (playerManager.CurrentState != PlayerState.Dead)
        {
            StartCoroutine(GameOverRoutine());
            playerManager.CurrentState = PlayerState.Dead;
        }
    }

    private IEnumerator GameOverRoutine()
    {
        vulnerable = false;
        yield return new WaitForSeconds(secondsBeforeLoad);
        // TODO should probably point to checkpoint logic
        Debug.Log("Respawn at checkpoint");
    }
}
