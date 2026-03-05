using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string doorID = "mainDoor";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Door triggered, Instance is: " + SceneTransitionManager.Instance);
            SceneTransitionManager.Instance.lastDoor = doorID;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}