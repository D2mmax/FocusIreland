using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform defaultSpawn;
    [SerializeField] private Transform doorSpawn;
    [SerializeField] private Transform schoolSpawn;
    [SerializeField] private string expectedDoorID = "schooldoorinside";

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        if (DayFlags.schoolCompleted && schoolSpawn != null)
        {
            player.transform.position = schoolSpawn.position;
        }
        else if (SceneTransitionManager.Instance != null
            && SceneTransitionManager.Instance.lastDoor == expectedDoorID)
        {
            player.transform.position = doorSpawn.position;
            SceneTransitionManager.Instance.lastDoor = "";
        }
        else
        {
            player.transform.position = defaultSpawn.position;
        }
    }
}
