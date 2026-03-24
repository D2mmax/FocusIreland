using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteraction : MonoBehaviour
{
    [Header("Dialogue Data")]
    public DialogueSceneConfig sceneConfig;
    public DialogueTree dialogueTree;

    [Header("Prompt (optional)")]
    public GameObject interactPrompt;

    private bool playerInRange = false;
    private bool hasSpoken = false;
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

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            interactCooldown = COOLDOWN_DURATION;
            if (hasSpoken)
                DialogueScreenManager.Instance.StartRepeatDialogue(sceneConfig);
            else
            {
                hasSpoken = true;
                DialogueScreenManager.Instance.StartDialogue(sceneConfig, dialogueTree);
            }
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
