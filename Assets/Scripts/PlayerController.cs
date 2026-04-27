using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;

    private Rigidbody rb;

    private static readonly int IsMoving  = Animator.StringToHash("IsMoving");
    private static readonly int Direction  = Animator.StringToHash("Direction");

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }

    void FixedUpdate()
    {
        if (DialogueScreenManager.Instance != null && DialogueScreenManager.Instance.IsInDialogue)
        {
            rb.linearVelocity = Vector3.zero;
            animator.SetBool(IsMoving, false);
            return;
        }

        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        Vector3 movement = Vector3.zero;

        if (keyboard.wKey.isPressed)
        {
            movement += new Vector3(-1f, 0f, 0f);
            animator.SetInteger(Direction, 0);
        }
        else if (keyboard.sKey.isPressed)
        {
            movement += new Vector3(1f, 0f, 0f);
            animator.SetInteger(Direction, 1);
        }
        else if (keyboard.dKey.isPressed)
        {
            movement += new Vector3(0f, 0f, 1f);
            animator.SetInteger(Direction, 2);
        }
        else if (keyboard.aKey.isPressed)
        {
            movement += new Vector3(0f, 0f, -1f);
            animator.SetInteger(Direction, 3);
        }

        if (movement.magnitude > 0f)
        {
            movement = movement.normalized;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            animator.SetBool(IsMoving, true);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
            animator.SetBool(IsMoving, false);
        }
    }
}