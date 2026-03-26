using UnityEngine;

// ---------------------------------------------------------------------------
//  PersistentHUD  — attach to HUDCanvas to keep it alive across scene loads
// ---------------------------------------------------------------------------
public class PersistentHUD : MonoBehaviour
{
    void Awake()
    {
        // Destroy duplicate HUDs if one already exists
        PersistentHUD[] huds = FindObjectsByType<PersistentHUD>(FindObjectsSortMode.None);
        if (huds.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
