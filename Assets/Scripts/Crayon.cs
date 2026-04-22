using UnityEngine;

public class Crayon : MonoBehaviour
{
    public string colorType;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    public void ReturnToStart()
    {
        transform.position = startPosition;
    }
}