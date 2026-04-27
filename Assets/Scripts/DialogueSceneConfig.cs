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

    [Tooltip("The NPC's portrait sprite shown on the right side")]
    public Sprite npcSprite;

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
}
