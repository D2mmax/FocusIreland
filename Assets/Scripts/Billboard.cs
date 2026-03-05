using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        transform.rotation = mainCam.transform.rotation;
    }
}