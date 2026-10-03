using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 10f;
    public float distanceLimit = 5f;
    public float travelRate = 0.5f;
    private float distance = 0;

    [Header("Gameobjects & Components")]
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        distance += Time.deltaTime * travelRate;

        if (distance >= distanceLimit) Destroy(gameObject);

        // Move in the direction its facing
        rb.linearVelocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;

        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<IHealth>(out var health))
            {
                health.TakeDamage();
                Destroy(gameObject);
            }
        }
    }
}