using UnityEngine;

public class OutdoorSceneDirector : MonoBehaviour
{
    [Header("Morning Objects (active before school)")]
    public GameObject[] morningObjects;

    [Header("After School Objects (active after school, before basketball)")]
    public GameObject[] afterSchoolObjects;

    [Header("Post Basketball Objects (active after basketball)")]
    public GameObject[] postBasketballObjects;

    void Start()
    {
        // Disable everything first
        SetActive(morningObjects, false);
        SetActive(afterSchoolObjects, false);
        SetActive(postBasketballObjects, false);

        if (DayFlags.basketballCompleted)
        {
            SetActive(postBasketballObjects, true);
        }
        else if (DayFlags.schoolCompleted)
        {
            SetActive(afterSchoolObjects, true);
        }
        else
        {
            SetActive(morningObjects, true);
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
