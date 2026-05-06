using UnityEngine;
using Cinemachine;

public class CameraFollowSetup : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        if (vcam == null) return;

        // Ensure this camera always wins on scene load regardless of minigame return
        vcam.Priority = 20;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            vcam.Follow = player.transform;
        else
            Debug.LogWarning("CameraFollowSetup: No GameObject with tag 'Player' found.");
    }
}
