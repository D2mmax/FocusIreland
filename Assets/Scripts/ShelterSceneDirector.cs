using UnityEngine;

public class ShelterSceneDirector : MonoBehaviour
{
    [Header("Morning Before School (State 0)")]
    public GameObject[] morningObjects;

    [Header("Evening After School (State 1)")]
    public GameObject[] eveningObjects;

    [Header("Post Crayon Sort (State 2)")]
    public GameObject[] postCrayonObjects;

    [Header("Next Morning (State 3)")]
    public GameObject[] nextMorningObjects;

    void Start()
    {
        SetActive(morningObjects, false);
        SetActive(eveningObjects, false);
        SetActive(postCrayonObjects, false);
        SetActive(nextMorningObjects, false);

        switch (DayFlags.shelterState)
        {
            case 0:
                SetActive(morningObjects, true);
                break;
            case 1:
                SetActive(eveningObjects, true);
                break;
            case 2:
                SetActive(postCrayonObjects, true);
                break;
            case 3:
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
