using UnityEngine;

public class ShelterEntranceTrigger : MonoBehaviour
{
    [Header("Prompt")]
    public GameObject interactPrompt;

    private bool playerInRange = false;
    private float interactCooldown = 0f;
    private const float COOLDOWN_DURATION = 0.3f;

    void Start()
    {
        if (interactPrompt) interactPrompt.SetActive(false);
    }

    void Update()
    {
        if (interactCooldown > 0f)
        {
            interactCooldown -= Time.deltaTime;
            return;
        }

        if (!playerInRange) return;
        if (DialogueScreenManager.Instance != null && DialogueScreenManager.Instance.IsInDialogue) return;

        if (UnityEngine.InputSystem.Keyboard.current != null && UnityEngine.InputSystem.Keyboard.current.eKey.wasPressedThisFrame)
        {
            interactCooldown = COOLDOWN_DURATION;

            DayFlags.shelterState = 1;

            if (SceneFader.Instance != null)
                SceneFader.Instance.FadeTo("ShelterScene");
            else
                UnityEngine.SceneManagement.SceneManager.LoadScene("ShelterScene");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = true;
        interactCooldown = COOLDOWN_DURATION;
        if (interactPrompt) interactPrompt.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
        if (interactPrompt) interactPrompt.SetActive(false);
    }
}
