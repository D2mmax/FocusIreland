using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGamePlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float screenPadding = 0.5f;

    private float minX;
    private float maxX;
    private float moveInput;

    void Start()
    {
        float camHalfWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        minX = -camHalfWidth + screenPadding;
        maxX = camHalfWidth - screenPadding;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += moveInput * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        transform.position = pos;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<float>();
    }
}