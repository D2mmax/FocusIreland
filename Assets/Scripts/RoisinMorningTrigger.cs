using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoisinMorningTrigger : MonoBehaviour
{
    [Header("Dialogue")]
    public DialogueSceneConfig roisinConfig;
    public DialogueTree roisinTree;

    [Header("Prompt")]
    public GameObject interactPrompt;

    [Header("Timing")]
    public float holdDuration = 3f;

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

        hasSpoken = true;
        if (interactPrompt) interactPrompt.SetActive(false);

        DialogueScreenManager.Instance.StartDialogue(roisinConfig, roisinTree, OnConversationEnd);
    }

    void OnConversationEnd()
    {
        StartCoroutine(RunEndSequence());
    }

    IEnumerator RunEndSequence()
    {
        // Fade to black once and stay there
        if (SceneFader.Instance != null)
        {
            SceneFader.Instance.ForceReady();
            // Manually fade to black by triggering a fade with no scene load
            // We use a long timeskip text fade to get to black, then take over
        }

        // Fade screen to black manually
        float fadeDuration = 1f;
        float t = 0f;

        // Use SceneFader's image if available, otherwise just wait
        var faderImage = SceneFader.Instance != null
            ? SceneFader.Instance.GetComponentInChildren<UnityEngine.UI.Image>()
            : null;

        if (faderImage != null)
        {
            faderImage.raycastTarget = true;
            while (t < 1f)
            {
                t += Time.deltaTime / fadeDuration;
                faderImage.color = new Color(0f, 0f, 0f, Mathf.Clamp01(t));
                yield return null;
            }
            faderImage.color = new Color(0f, 0f, 0f, 1f);
        }
        else
        {
            yield return new WaitForSeconds(fadeDuration);
        }

        // Show end cards one by one — screen stays black, just text changes
        string[] cards = new string[]
        {
            "THE END",
            "Every night in Ireland, over 1,500 children are in emergency accommodation. Every one of them has the right to a place they can call home.",
            "Focus Ireland. Challenging homelessness. Changing lives.",
            "focusireland.ie"
        };

        var timeskipText = SceneFader.Instance != null ? SceneFader.Instance.timeskipText : null;

        foreach (string card in cards)
        {
            if (timeskipText != null)
            {
                timeskipText.text = card;
                timeskipText.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(holdDuration);

            if (timeskipText != null)
                timeskipText.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);
        }

        // Load start scene — SceneFader will handle the fade in on the other side
        if (SceneFader.Instance != null)
        {
            SceneFader.Instance.ForceReady();
            SceneFader.Instance.FadeTo("StartScene");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
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
