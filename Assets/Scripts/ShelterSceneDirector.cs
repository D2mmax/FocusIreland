using UnityEngine;

public class ShelterSceneDirector : MonoBehaviour
{
    [Header("Morning Before School (State 0)")]
    public GameObject[] morningObjects;

    [Header("Evening After School (State 1)")]
    public GameObject[] eveningObjects;

    [Header("Next Morning (State 2)")]
    public GameObject[] nextMorningObjects;

    void Start()
    {
        // Disable everything first
        SetActive(morningObjects, false);
        SetActive(eveningObjects, false);
        SetActive(nextMorningObjects, false);

        // Enable the correct set based on current state
        switch (DayFlags.shelterState)
        {
            case 0:
                SetActive(morningObjects, true);
                break;
            case 1:
                SetActive(eveningObjects, true);
                break;
            case 2:
                SetActive(nextMorningObjects, true);
                break;
        }
    }

    void SetActive(GameObject[] objects, bool active)
    {
        if (objects == null) return;
        foreach (GameObject obj in objects)
        {
            if (obj != null)
                obj.SetActive(active);
        }
    }
}
