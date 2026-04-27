using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FaceChecker : MonoBehaviour
{
    [Header("Face Sprites (index 0 = high connection, index 4 = low)")]
    [Tooltip("Assign the 5 face sprites for this specific NPC here.")]
    public Sprite[] faceSprites = new Sprite[5];

    private SpriteRenderer spriteRenderer;
    private int lastConnectionValue = -1; // Used to track changes and prevent updating every frame

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Ensure the EHCManager exists
        if (EHCManager.Instance != null)
        {
            int currentConnection = EHCManager.Instance.connection;

            // Only update the sprite if the connection value has actually changed
            if (currentConnection != lastConnectionValue)
            {
                lastConnectionValue = currentConnection;
                UpdateFaceSprite();
            }
        }
    }

    void UpdateFaceSprite()
    {
        if (faceSprites == null || faceSprites.Length == 0) return;

        // Uses the same logic as EHCManager (4 - (value / 21)) to find the correct index based on a 0-100 scale.
        // Value 100 results in index 0. Value 0 results in index 4.
        int index = Mathf.Clamp(4 - (lastConnectionValue / 21), 0, faceSprites.Length - 1);
        
        spriteRenderer.sprite = faceSprites[index];
    }
}
