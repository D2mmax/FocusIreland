using UnityEngine;

public class ColorBucket : MonoBehaviour
{
    public string bucketColor;

    public AudioClip correctSound;
    public AudioClip wrongSound;

    public GameObject confettiPrefab; // NOTE: GameObject now

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
                audioSource.PlayOneShot(correctSound);

                // 🔥 SPAWN CONFETTI
                if (confettiPrefab != null)
                {
                    GameObject confetti = Instantiate(
                        confettiPrefab,
                        transform.position,
                        Quaternion.identity
                    );

                    Destroy(confetti, 2f); // clean up after playing
                }

                Destroy(other.gameObject, 0.1f);
            }
            else
            {
                audioSource.PlayOneShot(wrongSound);
                crayon.ReturnToStart();
            }
        }
    }
}