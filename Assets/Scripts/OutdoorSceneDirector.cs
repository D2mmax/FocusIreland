using UnityEngine;

public class OutdoorSceneDirector : MonoBehaviour
{
    [Header("Morning Objects (active before school)")]
    public GameObject morningBusStopTrigger;

    [Header("Afternoon Objects (active after school)")]
    public GameObject marcus;

    void Start()
    {
        if (DayFlags.schoolCompleted)
        {
            // After school — disable morning bus stop, enable Marcus
            if (morningBusStopTrigger != null) morningBusStopTrigger.SetActive(false);
            if (marcus != null) marcus.SetActive(true);
        }
        else
        {
            // Morning — enable bus stop, disable Marcus
            if (morningBusStopTrigger != null) morningBusStopTrigger.SetActive(true);
            if (marcus != null) marcus.SetActive(false);
        }
    }
}
