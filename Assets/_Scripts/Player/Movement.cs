using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(LookDirection))]
public class Movement : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float speed = 50f;
    [SerializeField] private float acceleration = 10f;

    [Header("Components")]
    [SerializeField] private CharacterController ch;
    [SerializeField] private LookDirection ld;

    // Smooth direction tools
    private Vector3 smoothDirection;

    void Start()
    {
        ch = GetComponent<CharacterController>();
        ld = GetComponent<LookDirection>();
    }

    void Update()
    {
        if (
            !ValidateComponents()
        ) return;

        Vector3 look = ld.Direction;
        if (look.sqrMagnitude > 0.001f) transform.rotation = Quaternion.LookRotation(look);

        Vector2 input = GetKeyboardInput();
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

        smoothDirection = Vector3.MoveTowards(smoothDirection, direction, acceleration * Time.deltaTime);

        ch.Move(speed * Time.deltaTime * smoothDirection);
    }

    #region Tools
    private bool ValidateComponents()
    {
        if (
            ch == null ||
            ld == null
        ) return false;

        return true;
    }

    private Vector2 GetKeyboardInput()
    {
        float inputX = 0;
        float inputY = 0;

        if (Keyboard.current.wKey.isPressed) inputY = 1;
        if (Keyboard.current.sKey.isPressed) inputY = -1;

        if (Keyboard.current.aKey.isPressed) inputX = -1;
        if (Keyboard.current.dKey.isPressed) inputX = 1;

        return new Vector2(inputX, inputY);
    }
    #endregion
}
