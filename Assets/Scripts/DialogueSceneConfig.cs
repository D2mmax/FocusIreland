using UnityEngine;

// ---------------------------------------------------------------------------
//  DialogueSceneConfig  — visual setup for one NPC's dialogue screen
//  Create via: Right-click in Project > Create > FocusIreland > Dialogue Scene Config
// ---------------------------------------------------------------------------
[CreateAssetMenu(fileName = "NewDialogueSceneConfig", menuName = "FocusIreland/Dialogue Scene Config")]
public class DialogueSceneConfig : ScriptableObject
{
    [Header("Characters")]
    [Tooltip("Name displayed above the NPC's dialogue text")]
    public string npcName;

    [Tooltip("The NPC's default portrait sprite (used if the array is empty)")]
    public Sprite npcSprite;

    [Header("Dynamic NPC Portraits (index 0 = high connection, index 4 = low)")]
    [Tooltip("Leave empty to just use the default npcSprite above.")]
    public Sprite[] dynamicNPCSprites = new Sprite[5];

    [Tooltip("The player character's portrait sprite shown on the left side")]
    public Sprite playerSprite;

    [Header("Scene Background")]
    [Tooltip("Background image shown during this dialogue (e.g. classroom, street)")]
    public Sprite backgroundSprite;

    [Header("Repeat Visit")]
    [Tooltip("Short line shown on repeat visits instead of the full conversation")]
    [TextArea(2, 3)]
    public string repeatLine = "Hey again!";

    [Header("Display Options")]
    [Tooltip("Hides the NPC sprite — use for internal monologue or choice moments with no character")]
    public bool hideNPCSprite = false;

    [Tooltip("Hides the player sprite")]
    public bool hidePlayerSprite = false;

    [Tooltip("Hides the background image")]
    public bool hideBackground = false;

    /// <summary>
    /// Returns the dynamically calculated NPC sprite based on the current connection value.
    /// Uses the default npcSprite if the dynamic array hasn't been set up.
    /// </summary>
    public Sprite GetCurrentNPCSprite()
    {
        if (dynamicNPCSprites == null || dynamicNPCSprites.Length == 0)
        {
            return npcSprite;
        }

        int connectionValue = 50; // Default fallback amount

        if (EHCManager.Instance != null)
        {
            connectionValue = EHCManager.Instance.connection;
        }

        // Same mathematical logic as FaceChecker & EHCManager
        int index = Mathf.Clamp(4 - (connectionValue / 21), 0, dynamicNPCSprites.Length - 1);
        
        // Safety check to ensure the slot isn't unexpectedly empty
        return dynamicNPCSprites[index] != null ? dynamicNPCSprites[index] : npcSprite;
    }
}
