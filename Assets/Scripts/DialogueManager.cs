using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI npcText;
    [SerializeField] private TextMeshProUGUI choice1Text;
    [SerializeField] private TextMeshProUGUI choice2Text;
    [SerializeField] private TextMeshProUGUI choice3Text;

    public bool isInDialogue = false;
    private string[] currentResponses;

    private void Awake()
    {
        Instance = this;
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(string opening, string[] choices, string[] responses)
    {
        dialoguePanel.SetActive(true);
        isInDialogue = true;
        currentResponses = responses;
        npcText.text = opening;

        choice1Text.text = "1. " + choices[0];
        choice2Text.text = "2. " + choices[1];
        choice3Text.text = "3. " + choices[2];

        choice1Text.gameObject.SetActive(true);
        choice2Text.gameObject.SetActive(true);
        choice3Text.gameObject.SetActive(true);
    }

    public void SelectChoice(int index)
    {
        npcText.text = currentResponses[index];
        choice1Text.gameObject.SetActive(false);
        choice2Text.gameObject.SetActive(false);
        choice3Text.gameObject.SetActive(false);
    }

    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        isInDialogue = false;
    }
}
