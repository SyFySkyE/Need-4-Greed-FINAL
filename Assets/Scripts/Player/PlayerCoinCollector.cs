using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCoinCollector : MonoBehaviour
{
    [Header("Coin Collecting Behavior")]
    [SerializeField] private int coinsCollected = 0;
    [SerializeField] private int coinBonus = 10;
    [SerializeField] private int coinsLostOnHurt = 5;

    [Header("Coin Text")]
    [SerializeField] private TextMeshProUGUI coinText;

    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        UpdateCoinText();
    }
    private void UpdateCoinText()
    {
        coinText.text = coinsCollected.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfJumpsOnEnemy();
    }

    private void CheckIfJumpsOnEnemy()
    {
        if (playerManager.CurrentState == PlayerState.LandingOnEnemy && playerManager.PreviousState != playerManager.CurrentState)
        {
            AddCoinBonus();
        }
    }

    public void AddCoin()
    {
        coinsCollected++;
        UpdateCoinText();
    }

    private void AddCoinBonus()
    {
        coinsCollected += coinBonus;
        UpdateCoinText();
    }

    public int GetCoinsCollected()
    {
        return coinsCollected;
    }

    private void PlayerHurt()
    {
        coinsCollected -= coinsLostOnHurt;
        UpdateCoinText();
    }
}
