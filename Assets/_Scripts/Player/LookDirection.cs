using UnityEngine;
using UnityEngine.InputSystem;

public class LookDirection : MonoBehaviour
{
    public Vector3 Direction { get; private set; }

    void Update()
    {
        Direction = GetLookDirection();
        if (Direction.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(Direction);
    }

    private Vector3 GetLookDirection()
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

}
