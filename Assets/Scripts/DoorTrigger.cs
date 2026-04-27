using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// ---------------------------------------------------------------------------
//  DoorTrigger  — shows a Press E prompt and loads a scene on interaction
//  Place on any exit collider. Set Is Trigger on the collider.
// ---------------------------------------------------------------------------
public class DoorTrigger : MonoBehaviour
{
    [Header("Scene to Load")]
    [Tooltip("Exact name of the scene to load")]
    public string targetScene;

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

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            interactCooldown = COOLDOWN_DURATION;
            if (SceneFader.Instance != null)
                SceneFader.Instance.FadeTo(targetScene);
            else
                SceneManager.LoadScene(targetScene);
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
