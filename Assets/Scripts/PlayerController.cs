using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    void Update()
    {
         if (DialogueManager.Instance != null && DialogueManager.Instance.isInDialogue) return;
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        Vector3 movement = Vector3.zero;

        if (keyboard.wKey.isPressed) movement += new Vector3(-1f, 0f, 0f); // North
        if (keyboard.sKey.isPressed) movement += new Vector3(1f, 0f, 0f);  // South
        if (keyboard.dKey.isPressed) movement += new Vector3(0f, 0f, 1f);  // East
        if (keyboard.aKey.isPressed) movement += new Vector3(0f, 0f, -1f); // West

        movement = movement.normalized;
        transform.position += movement * moveSpeed * Time.deltaTime;
    }
}