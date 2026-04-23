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
        if (mainCam == null) return;

        // Only copy Y rotation so the sprite stays upright and centred
        transform.rotation = Quaternion.Euler(0f, mainCam.transform.eulerAngles.y, 0f);
    }
}