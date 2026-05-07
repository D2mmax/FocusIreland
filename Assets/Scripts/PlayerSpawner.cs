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

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        // Shelter scene spawning
        if (bedroomSpawn != null || patDeskSpawn != null || aoifeSpawn != null)
        {
            if (DayFlags.shelterState == 0)
            {
                // Morning before school — bedroom
                if (bedroomSpawn != null)
                    player.transform.position = bedroomSpawn.position;
            }
            else if (DayFlags.shelterState == 1)
            {
                if (DayFlags.crayonSortCompleted && aoifeSpawn != null)
                {
                    // Returned from crayon sort — near Aoife
                    player.transform.position = aoifeSpawn.position;
                }
                else if (patDeskSpawn != null)
                {
                    // Returned from basketball — Pat's desk
                    player.transform.position = patDeskSpawn.position;
                }
            }
            else if (DayFlags.shelterState == 2)
            {
                // Next morning — bedroom
                if (bedroomSpawn != null)
                    player.transform.position = bedroomSpawn.position;
            }
            return;
        }

        // Outdoor scene spawning
        if (DayFlags.schoolCompleted && schoolDoorSpawn != null)
            player.transform.position = schoolDoorSpawn.position;
        else if (shelterDoorSpawn != null)
            player.transform.position = shelterDoorSpawn.position;
    }
}
