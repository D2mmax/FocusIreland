using UnityEngine;
using UnityEngine.UI;

// ---------------------------------------------------------------------------
//  EHCManager  — persistent singleton that owns Energy, Hope, Connection stats
//  Uses sprite swapping instead of sliders — assign 5 sprites per stat (full to empty)
// ---------------------------------------------------------------------------
public class EHCManager : MonoBehaviour
{
    public static EHCManager Instance { get; private set; }

    [Header("Starting Values (0-100)")]
    [Range(0, 100)] public int energy     = 100;
    [Range(0, 100)] public int hope       = 100;
    [Range(0, 100)] public int connection = 100;

    [Header("HUD Image References")]
    public Image energyImage;
    public Image hopeImage;
    public Image connectionImage;

    [Header("Energy Sprites (index 0 = full, index 4 = empty)")]
    public Sprite[] energySprites     = new Sprite[5];

    [Header("Hope Sprites (index 0 = full, index 4 = empty)")]
    public Sprite[] hopeSprites       = new Sprite[5];

    [Header("Connection Sprites (index 0 = full, index 4 = empty)")]
    public Sprite[] connectionSprites = new Sprite[5];

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        UpdateSprites();
    }

    // Call this from DialogueScreenManager when a choice is made
    public void ApplyEffect(EHCEffect effect)
    {
        energy     = Mathf.Clamp(energy     + effect.energyDelta,     0, 100);
        hope       = Mathf.Clamp(hope       + effect.hopeDelta,       0, 100);
        connection = Mathf.Clamp(connection + effect.connectionDelta, 0, 100);
        UpdateSprites();
    }

    void UpdateSprites()
    {
        if (energyImage)     energyImage.sprite     = GetSprite(energy,     energySprites);
        if (hopeImage)       hopeImage.sprite       = GetSprite(hope,       hopeSprites);
        if (connectionImage) connectionImage.sprite = GetSprite(connection, connectionSprites);
    }

    // Maps a 0-100 value to one of 5 sprite slots
    Sprite GetSprite(int value, Sprite[] sprites)
    {
        if (sprites == null || sprites.Length == 0) return null;

        // 100-81 = index 0 (full)
        // 80-61  = index 1
        // 60-41  = index 2
        // 40-21  = index 3
        // 20-0   = index 4 (empty)
        int index = Mathf.Clamp(4 - (value / 21), 0, sprites.Length - 1);
        return sprites[index];
    }
}
