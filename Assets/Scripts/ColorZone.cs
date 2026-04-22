using UnityEngine;

public class ColorBucket : MonoBehaviour
{
    public string bucketColor;

    public AudioClip correctSound;
    public AudioClip wrongSound;

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
                Destroy(other.gameObject);
            }
            else
            {
                audioSource.PlayOneShot(wrongSound);
                crayon.ReturnToStart();
            }
        }
    }
}