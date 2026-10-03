using UnityEngine;

[RequireComponent(typeof(IPowerAttack))]
public class DetectPlayer : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float detectionRadius = 5.0f;
    [SerializeField] private LayerMask targetLayer;

    [Header("Components")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private IPowerAttack powerAttack;

    void Start()
    {
        playerTransform = FindAnyObjectByType<LookDirection>().transform;
        powerAttack = GetComponent<IPowerAttack>();
    }

    void Update()
    {
        if (
            playerTransform == null
        ) return;

        DetectObjects();
    }

    void DetectObjects()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                if (powerAttack.IsRecharged()) powerAttack.OnAttack();
            }
        }
    }

    public Vector3 PlayerPosition()
    {
        if (playerTransform == null) return Vector3.zero;

        return playerTransform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
