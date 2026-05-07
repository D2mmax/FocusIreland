using UnityEngine;

public class ColorBucket : MonoBehaviour
{
    public string bucketColor;

    public AudioClip correctSound;
    public AudioClip wrongSound;

    public GameObject confettiPrefab;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        Crayon crayon = other.GetComponent<Crayon>();

        if (crayon != null)
        {
            if (crayon.colorType == bucketColor)
            {
                if (audioSource != null && correctSound != null)
                    audioSource.PlayOneShot(correctSound);

                if (confettiPrefab != null)
                {
                    GameObject confetti = Instantiate(confettiPrefab, transform.position, Quaternion.identity);
                    Destroy(confetti, 2f);
                }

                Destroy(other.gameObject, 0.1f);

                // Notify manager that a crayon was sorted correctly
                if (CrayonSortManager.Instance != null)
                    CrayonSortManager.Instance.OnCrayonSorted();
            }
            else
            {
                if (audioSource != null && wrongSound != null)
                    audioSource.PlayOneShot(wrongSound);

                crayon.ReturnToStart();
            }
        }
    }
}
