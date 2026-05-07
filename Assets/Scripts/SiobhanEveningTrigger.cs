using UnityEngine;
using UnityEngine.InputSystem;

public class SiobhanEveningTrigger : MonoBehaviour
{
    [Header("Dialogue Configs")]
    public DialogueSceneConfig siobhanConfig;

    [Header("Dialogue Trees")]
    public DialogueTree honestTree;
    public DialogueTree humourTree;
    public DialogueTree shutdownTree;

    [Header("Prompt")]
    public GameObject interactPrompt;

    [Header("Timeskip Settings")]
    public float timeSkipHoldDuration = 3f;

    private bool playerInRange = false;
    private float interactCooldown = 0f;
    private const float COOLDOWN_DURATION = 0.3f;
    private bool hasSpoken = false;

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
        if (hasSpoken) return;
        if (DialogueScreenManager.Instance != null && DialogueScreenManager.Instance.IsInDialogue) return;

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            interactCooldown = COOLDOWN_DURATION;
            StartConversation();
        }
    }

    void StartConversation()
    {
        if (DialogueScreenManager.Instance == null) return;

        string mode = DayFlags.GetDominantMode();

        Debug.Log($"[SiobhanEvening] Humour: {DayFlags.humourChoices} | Honest: {DayFlags.honestChoices} | Shutdown: {DayFlags.shutdownChoices} | Dominant: {mode}");

        DialogueTree tree;
        if (mode == "humour")
            tree = humourTree;
        else if (mode == "shutdown")
            tree = shutdownTree;
        else
            tree = honestTree;

        hasSpoken = true;
        if (interactPrompt) interactPrompt.SetActive(false);

        DialogueScreenManager.Instance.StartDialogue(siobhanConfig, tree, OnConversationEnd);
    }

    void OnConversationEnd()
    {
        DayFlags.shelterState = 3;

        if (SceneFader.Instance != null)
        {
            SceneFader.Instance.FadeToBlackWithText(
                "He lay awake for a while. Then, somehow, he slept.",
                timeSkipHoldDuration,
                () =>
                {
                    if (SceneFader.Instance.timeskipText != null)
                        SceneFader.Instance.timeskipText.gameObject.SetActive(false);
                    SceneFader.Instance.ForceReady();
                    SceneFader.Instance.FadeTo("ShelterScene");
                }
            );
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("ShelterScene");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = true;
        interactCooldown = COOLDOWN_DURATION;
        if (interactPrompt && !hasSpoken) interactPrompt.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
        if (interactPrompt) interactPrompt.SetActive(false);
    }
}
