using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Coin Sound Effects")]
    [SerializeField] private AudioClip[] coinSfx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerCoinCollector>().AddCoin();
            AudioSource.PlayClipAtPoint(coinSfx[Random.Range(0, coinSfx.Length)], transform.position, 1f);
            Destroy(gameObject);
        }
    }
}
