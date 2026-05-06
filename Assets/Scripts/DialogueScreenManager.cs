using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueScreenManager : MonoBehaviour
{
    public static DialogueScreenManager Instance { get; private set; }

    [Header("Screen Root")]
    public CanvasGroup dialogueScreenGroup;

    [Header("Background & Characters")]
    public Image backgroundImage;
    public Image npcImage;
    public Image playerImage;

    [Header("Text")]
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogueText;

    [Header("Player Side Text")]
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerDialogueText;

    [Header("Choice Buttons (exactly 3)")]
    public Button[] choiceButtons;
    private TextMeshProUGUI[] choiceLabels;

    [Header("Close Button")]
    [Tooltip("A single button shown when a node has no choices — press to continue/close")]
    public Button closeButton;

    public bool IsInDialogue { get; private set; }
    private DialogueTree currentTree;
    private DialogueNode currentNode;

    // Optional callback fired when dialogue ends — used by NPCInteraction for scene loads
    private System.Action onDialogueEndCallback;

    // Prevents E key from closing immediately after opening
    private float inputCooldown = 0f;
    private const float COOLDOWN_DURATION = 0.2f;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        choiceLabels = new TextMeshProUGUI[choiceButtons.Length];
        for (int i = 0; i < choiceButtons.Length; i++)
            choiceLabels[i] = choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();

        if (closeButton != null)
            closeButton.onClick.AddListener(EndDialogue);

        HideScreen();
    }

    public void StartRepeatDialogue(DialogueSceneConfig config)
    {
        backgroundImage.sprite = config.backgroundSprite;
        npcImage.sprite        = config.GetCurrentNPCSprite(); // Updated to get dynamic sprite
        playerImage.sprite     = config.playerSprite;
        npcNameText.text       = config.npcName;
        dialogueText.text      = config.repeatLine;

        npcImage.gameObject.SetActive(!config.hideNPCSprite);
        playerImage.gameObject.SetActive(!config.hidePlayerSprite);
        backgroundImage.gameObject.SetActive(!config.hideBackground);

        foreach (var btn in choiceButtons) btn.gameObject.SetActive(false);
        if (closeButton != null) closeButton.gameObject.SetActive(true);

        IsInDialogue = true;
        inputCooldown = COOLDOWN_DURATION;
        ShowScreen();
    }

    public void StartDialogue(DialogueSceneConfig config, DialogueTree tree, System.Action onEnd = null)
    {
        onDialogueEndCallback = onEnd;
        currentTree = tree;

        backgroundImage.sprite = config.backgroundSprite;
        npcImage.sprite        = config.GetCurrentNPCSprite(); // Updated to get dynamic sprite
        playerImage.sprite     = config.playerSprite;
        npcNameText.text       = config.npcName;

        npcImage.gameObject.SetActive(!config.hideNPCSprite);
        playerImage.gameObject.SetActive(!config.hidePlayerSprite);
        backgroundImage.gameObject.SetActive(!config.hideBackground);

        IsInDialogue = true;
        inputCooldown = COOLDOWN_DURATION;
        ShowScreen();
        ShowNode(tree.GetNode(tree.startNodeID));
    }

    void ShowNode(DialogueNode node)
    {
        if (node == null) { EndDialogue(); return; }
        currentNode = node;

        bool isPlayerSpeaking = node.speaker == DialogueNode.Speaker.Player;

        // NPC side
        if (npcNameText != null)        npcNameText.gameObject.SetActive(!isPlayerSpeaking);
        if (dialogueText != null)       dialogueText.gameObject.SetActive(!isPlayerSpeaking);

        // Player side
        if (playerNameText != null)     playerNameText.gameObject.SetActive(isPlayerSpeaking);
        if (playerDialogueText != null) playerDialogueText.gameObject.SetActive(isPlayerSpeaking);

        if (isPlayerSpeaking)
        {
            if (playerNameText != null)     playerNameText.text = "Ciarán";
            if (playerDialogueText != null) playerDialogueText.text = node.dialogueText;
        }
        else
        {
            dialogueText.text = node.dialogueText;
        }

        foreach (var btn in choiceButtons) btn.gameObject.SetActive(false);
        if (closeButton != null) closeButton.gameObject.SetActive(false);

        bool hasChoices = node.choices != null && node.choices.Count > 0;

        if (!hasChoices)
        {
            if (closeButton != null)
            {
                closeButton.gameObject.SetActive(true);
                closeButton.onClick.RemoveAllListeners();

                bool hasContinue = !string.IsNullOrEmpty(node.continueNodeID);
                var label = closeButton.GetComponentInChildren<TMPro.TextMeshProUGUI>();

                if (hasContinue)
                {
                    if (label != null) label.text = "Continue";
                    closeButton.onClick.AddListener(() => ShowNode(currentTree.GetNode(currentNode.continueNodeID)));
                }
                else
                {
                    if (label != null) label.text = "End Conversation";
                    closeButton.onClick.AddListener(EndDialogue);
                }
            }
            return;
        }

        int count = Mathf.Min(node.choices.Count, choiceButtons.Length);
        for (int i = 0; i < count; i++)
        {
            int idx = i;
            choiceLabels[i].text = node.choices[i].choiceText;
            choiceButtons[i].gameObject.SetActive(true);
            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(idx));
        }
    }

    void OnChoiceSelected(int index)
    {
        DialogueChoice choice = currentNode.choices[index];

        if (EHCManager.Instance != null)
            EHCManager.Instance.ApplyEffect(choice.ehcEffect);

        if (string.IsNullOrEmpty(choice.nextNodeID))
            EndDialogue();
        else
            ShowNode(currentTree.GetNode(choice.nextNodeID));
    }

    void Update()
    {
        if (!IsInDialogue) return;

        if (inputCooldown > 0f)
        {
            inputCooldown -= Time.deltaTime;
        }
    }

    void EndDialogue()
    {
        IsInDialogue = false;
        npcImage.gameObject.SetActive(true);
        playerImage.gameObject.SetActive(true);
        backgroundImage.gameObject.SetActive(true);
        HideScreen();
        var callback = onDialogueEndCallback;
        onDialogueEndCallback = null;
        callback?.Invoke();
    }

    void ShowScreen()
    {
        dialogueScreenGroup.alpha          = 1f;
        dialogueScreenGroup.interactable   = true;
        dialogueScreenGroup.blocksRaycasts = true;
    }

    void HideScreen()
    {
        dialogueScreenGroup.alpha          = 0f;
        dialogueScreenGroup.interactable   = false;
        dialogueScreenGroup.blocksRaycasts = false;
        foreach (var btn in choiceButtons) btn.gameObject.SetActive(false);
        if (closeButton != null) closeButton.gameObject.SetActive(false);
    }
}
