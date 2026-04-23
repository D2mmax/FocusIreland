using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Freeze rotation so the rigidbody never tips over
        // Freeze Y position so the player stays grounded and doesn't float
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }

    void FixedUpdate()
    {
        if (DialogueScreenManager.Instance != null && DialogueScreenManager.Instance.IsInDialogue)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        Vector3 movement = Vector3.zero;

        if (keyboard.wKey.isPressed) movement += new Vector3(-1f, 0f, 0f);
        if (keyboard.sKey.isPressed) movement += new Vector3(1f, 0f, 0f);
        if (keyboard.dKey.isPressed) movement += new Vector3(0f, 0f, 1f);
        if (keyboard.aKey.isPressed) movement += new Vector3(0f, 0f, -1f);

        if (movement.magnitude > 0f)
        {
            movement = movement.normalized;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Kill any residual velocity so there is no drift
            rb.linearVelocity = Vector3.zero;
        }
    }
}