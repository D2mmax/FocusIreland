using UnityEngine;
using UnityEngine.InputSystem;

public class BasketballMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float screenPadding = 0.5f;

    private float minX;
    private float maxX;
    private float moveInput;

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        CalculateScreenBounds();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 position = transform.position;

        position.x += moveInput * moveSpeed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, minX, maxX);

        transform.position = position;
    }

    void CalculateScreenBounds()
    {
        float camHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;

        minX = -camHalfWidth + screenPadding;
        maxX = camHalfWidth - screenPadding;
    }

    // Called automatically by Player Input component
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<float>();
    }
}