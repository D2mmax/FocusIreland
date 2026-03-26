using UnityEngine;
using TMPro;

// ---------------------------------------------------------------------------
//  InteractPrompt  — a world-space "Press E" label that always faces the camera
//  Place on a child GameObject of any NPC or door that needs an interact prompt
// ---------------------------------------------------------------------------
public class InteractPrompt : MonoBehaviour
{
    [Header("Settings")]
    public string promptText = "Press E";
    public float heightOffset = 2.5f;

    private Canvas canvas;
    private TextMeshProUGUI tmp;
    private Transform cam;

    void Awake()
    {
        // Create a world-space canvas on this GameObject
        canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.sortingOrder = 10;

        // Set canvas size
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(200, 50);
        rt.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        // Create TMP child
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(transform, false);

        tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = promptText;
        tmp.fontSize = 42;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = new Color(1f, 0.92f, 0.016f);

        RectTransform textRt = textObj.GetComponent<RectTransform>();
        textRt.sizeDelta = new Vector2(200, 50);
        textRt.anchoredPosition = Vector2.zero;

        // Position above the parent
        transform.localPosition = new Vector3(0, heightOffset, 0);

        gameObject.SetActive(false);
    }

    void Start()
    {
        cam = Camera.main?.transform;
    }

    void LateUpdate()
    {
        if (cam != null)
            transform.rotation = Quaternion.LookRotation(transform.position - cam.position);
    }
}
