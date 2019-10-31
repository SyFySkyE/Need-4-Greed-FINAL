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

    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinText();
    }
    private void UpdateCoinText()
    {
        coinText.text = coinsCollected.ToString();
    }

    public void AddCoin()
    {
        coinsCollected++;
        UpdateCoinText();
    }

    public void AddCoinBonus()
    {
        coinsCollected += coinBonus;
        UpdateCoinText();
    }

    public int GetCoinsCollected()
    {
        return coinsCollected;
    }

    public void PlayerHurt()
    {
        coinsCollected -= coinsLostOnHurt;
        UpdateCoinText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AddCoinBonus();
        }
    }
}
