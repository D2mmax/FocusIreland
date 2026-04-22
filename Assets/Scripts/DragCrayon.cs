using UnityEngine;

public class DragCrayon : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + offset;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }
}