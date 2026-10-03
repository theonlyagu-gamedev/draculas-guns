using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandGun : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float fireRate = 5f; // shots per second
    private float nextFireTime;

    [Header("Components")]
    [SerializeField] private LookDirection aimDirection;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform muzzel;

    void Start()
    {
        aimDirection = FindAnyObjectByType<LookDirection>();
    }

    void Update()
    {
        if (!ValidateComponents()) return;

        if (Mouse.current.leftButton.isPressed && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 dir = aimDirection.Direction;
        if (dir.sqrMagnitude < 0.001f) dir = aimDirection.transform.forward;

        Instantiate(projectile, muzzel.position, Quaternion.LookRotation(dir));
    }

    #region Tools
    private bool ValidateComponents()
    {
        if (
            aimDirection == null ||
            projectile == null ||
            muzzel == null
        ) return false;

        return true;
    }
    #endregion
}