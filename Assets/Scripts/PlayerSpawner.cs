using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform defaultSpawn;
    [SerializeField] private Transform doorSpawn;
    [SerializeField] private string expectedDoorID = "schooldoorinside";

    void Start()
    {
        if (SceneTransitionManager.Instance != null)
        Debug.Log("Last door: " + SceneTransitionManager.Instance.lastDoor);
    else
        Debug.Log("SceneTransitionManager is NULL");
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        if (SceneTransitionManager.Instance != null 
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
