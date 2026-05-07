using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Shelter Scene Spawns")]
    [SerializeField] private Transform bedroomSpawn;
    [SerializeField] private Transform patDeskSpawn;
    [SerializeField] private Transform aoifeSpawn;

    [Header("Outdoor Scene Spawns")]
    [SerializeField] private Transform shelterDoorSpawn;
    [SerializeField] private Transform schoolDoorSpawn;
    [SerializeField] private Transform marcusSpawn;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        // Shelter scene spawning
        if (bedroomSpawn != null || patDeskSpawn != null || aoifeSpawn != null)
        {
            if (DayFlags.shelterState == 0)
            {
                if (bedroomSpawn != null)
                    player.transform.position = bedroomSpawn.position;
            }
            else if (DayFlags.shelterState == 1)
            {
                if (DayFlags.crayonSortCompleted && aoifeSpawn != null)
                    player.transform.position = aoifeSpawn.position;
                else if (patDeskSpawn != null)
                    player.transform.position = patDeskSpawn.position;
            }
            else if (DayFlags.shelterState == 2)
            {
                if (bedroomSpawn != null)
                    player.transform.position = bedroomSpawn.position;
            }
            return;
        }

        // Outdoor scene spawning
        if (DayFlags.basketballCompleted && marcusSpawn != null)
            player.transform.position = marcusSpawn.position;
        else if (DayFlags.schoolCompleted && schoolDoorSpawn != null)
            player.transform.position = schoolDoorSpawn.position;
        else if (shelterDoorSpawn != null)
            player.transform.position = shelterDoorSpawn.position;
    }
}
