using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private string openingLine = "Hello! How are you?";
    [SerializeField] private string[] choices = new string[]
    {
        "I'm doing well thanks",
        "I'm struggling a bit",
        "I don't want to talk"
    };
    [SerializeField] private string[] responses = new string[]
    {
        "That's great to hear! Keep it up!",
        "That's okay, things will get better. You're not alone.",
        "That's okay, I'm here if you need me."
    };

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (DialogueManager.Instance.isInDialogue)
                DialogueManager.Instance.CloseDialogue();
            else
                DialogueManager.Instance.StartDialogue(openingLine, choices, responses);
        }

        if (DialogueManager.Instance.isInDialogue)
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
                DialogueManager.Instance.SelectChoice(0);
            if (Keyboard.current.digit2Key.wasPressedThisFrame)
                DialogueManager.Instance.SelectChoice(1);
            if (Keyboard.current.digit3Key.wasPressedThisFrame)
                DialogueManager.Instance.SelectChoice(2);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
