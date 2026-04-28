using UnityEngine;
using Cinemachine;

public class CameraDebugger : MonoBehaviour
{
    private CinemachineBrain brain;
    private int frameCount = 0;

    void Start()
    {
        brain = FindObjectOfType<CinemachineBrain>();
    }

    void Update()
    {
        if (brain == null) return;
        frameCount++;

        var activeVcam = brain.ActiveVirtualCamera;
        string vcamName = activeVcam != null ? activeVcam.Name : "NONE";

        // Only log first 10 frames and any changes
        if (frameCount <= 10)
            Debug.Log($"[Camera] Frame {frameCount} | vcam: {vcamName} | FOV: {brain.OutputCamera.fieldOfView} | IndoorCam enabled: {GetIndoorCamEnabled()}");
    }

    bool GetIndoorCamEnabled()
    {
        var all = FindObjectsOfType<CinemachineVirtualCamera>();
        foreach (var v in all)
            if (v.name == "IndoorCam") return v.enabled;
        return false;
    }
}
