using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float speed = 50f;

    [Header("Components")]
    [SerializeField] private CharacterController ch;

    void Start()
    {
        ch = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (
            !ValidateComponents()
        ) return;

        Vector3 look = LookDirection();
        if (look.sqrMagnitude > 0.001f) transform.rotation = Quaternion.LookRotation(look);

        Vector2 input = GetKeyboardInput();
        Vector3 direction = new Vector3(input.x, 0f, input.y);

        ch.Move(direction * speed * Time.deltaTime);
    }

    private Vector3 LookDirection()
    {
        Camera cam = Camera.main;
        if (cam == null) return transform.forward;

        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane ground = new Plane(Vector3.up, transform.position);

        if (ground.Raycast(ray, out float distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 dir = target - transform.position;
            dir.y = 0f;
            return dir.normalized;
        }

        return transform.forward;
    }

    #region Tools
    private bool ValidateComponents()
    {
        if (
            ch == null
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
