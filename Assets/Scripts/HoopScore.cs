using UnityEngine;

public class HoopScore : MonoBehaviour
{
    private bool scored = false;

    public int scoreValue = 2;

    [Header("Effects")]
    public GameObject confettiPrefab;
    public Transform confettiSpawnPoint;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip scoreSound;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ball")) return;

        if (scored) return;

        if (collision.transform.position.y > transform.position.y)
        {
            scored = true;

            Debug.Log("SCORE!");

            ScoreManager.instance.AddScore(scoreValue);

            // 🔊 SCORE SOUND
            if (audioSource != null && scoreSound != null)
            {
                audioSource.PlayOneShot(scoreSound);
            }

            // 🎉 CONFETTI
            if (confettiPrefab != null)
            {
                Vector3 spawnPos = confettiSpawnPoint != null
                    ? confettiSpawnPoint.position
                    : transform.position;

                GameObject fx = Instantiate(confettiPrefab, spawnPos, Quaternion.identity);
                Destroy(fx, 2f);
            }

            Destroy(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            scored = false;
        }
    }
}