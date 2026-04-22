using UnityEngine;
using UnityEngine.InputSystem;

public class DragManager : MonoBehaviour
{
    private GameObject selectedObject;
    private Vector3 offset;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Crayon"))
                {
                    selectedObject = hit.collider.gameObject;
                    offset = selectedObject.transform.position - hit.point;
                }
            }
        }

        if (Mouse.current.leftButton.isPressed && selectedObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Plane plane = new Plane(Vector3.forward, Vector3.zero);
            float distance;

            if (plane.Raycast(ray, out distance))
            {
                Vector3 point = ray.GetPoint(distance);
                selectedObject.transform.position = point + offset;
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            selectedObject = null;
        }
    }
}