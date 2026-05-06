using UnityEngine;

public class OutdoorSceneDirector : MonoBehaviour
{
    [Header("Morning Objects (active before school)")]
    public GameObject morningBusStopTrigger;
    public GameObject lily;

    [Header("Afternoon Objects (active after school)")]
    public GameObject marcus;

    void Start()
    {
        if (DayFlags.schoolCompleted)
        {
            // After school — disable morning objects, enable afternoon objects
            if (morningBusStopTrigger != null) morningBusStopTrigger.SetActive(false);
            if (lily != null) lily.SetActive(false);
            if (marcus != null) marcus.SetActive(true);
        }
        else
        {
            // Morning — enable morning objects, disable afternoon objects
            if (morningBusStopTrigger != null) morningBusStopTrigger.SetActive(true);
            if (lily != null) lily.SetActive(true);
            if (marcus != null) marcus.SetActive(false);
        }
    }
}
