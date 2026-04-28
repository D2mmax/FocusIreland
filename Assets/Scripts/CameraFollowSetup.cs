using UnityEngine;
using Cinemachine;

public class CameraFollowSetup : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        if (vcam == null) return;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            vcam.Follow = player.transform;
        else
            Debug.LogWarning("CameraFollowSetup: No GameObject with tag 'Player' found.");
    }
}
