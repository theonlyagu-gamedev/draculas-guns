using UnityEngine;

[RequireComponent(typeof(DetectPlayer))]
public class BloodSpikes : MonoBehaviour, IPowerAttack
{
    [field: Header("Attack Settings")]
    [field: SerializeField] public bool CanAttack { get; set; } = false;
    [field: SerializeField] public float MaxAttackTime { get; set; } = 10f;
    [field: SerializeField] public float AttactTime { get; set; } = 0f;
    [field: SerializeField] public float RechargeTime { get; set; } = 10f;
    [field: SerializeField] public float MaxRechargeTime { get; set; } = 10f;

    [Header("Spike Settings")]
    [SerializeField] private float spikeRate = 5f;
    [SerializeField] private LayerMask groundLayer = ~0;
    [SerializeField] private float groundCheckDistance = 50f;
    private float nextSpikeTime;

    [Header("Components")]
    [SerializeField] private DetectPlayer dp;
    [SerializeField] private GameObject spike;

    void Start()
    {
        dp = GetComponent<DetectPlayer>();
    }

    void Update()
    {
        if (dp == null || spike == null) return;

        if (!CanAttack)
        {
            Recharge();
            return;
        }

        AttactTime += Time.deltaTime;
        if (AttactTime >= MaxAttackTime)
        {
            CanAttack = false;
            AttactTime = 0f;
            RechargeTime = 0f;
            return;
        }

        if (Time.time >= nextSpikeTime)
        {
            nextSpikeTime = Time.time + 1f / spikeRate;
            SpawnSpike();
        }
    }

    private void SpawnSpike()
    {
        Vector3 playerPosition = dp.PlayerPosition();
        Vector3 spawnPosition = playerPosition;

        if (Physics.Raycast(playerPosition, Vector3.down, out RaycastHit hit, groundCheckDistance, groundLayer, QueryTriggerInteraction.Ignore))
        {
            spawnPosition = hit.point;
        }

        Instantiate(spike, spawnPosition, Quaternion.identity);
    }

    private void Recharge()
    {
        if (RechargeTime >= MaxRechargeTime) return;

        RechargeTime = Mathf.Min(RechargeTime + Time.deltaTime, MaxRechargeTime);
    }

    public bool IsRecharged()
    {
        return RechargeTime >= MaxRechargeTime;
    }

    public void OnAttack()
    {
        if (CanAttack || !IsRecharged()) return;

        CanAttack = true;
        AttactTime = 0f;
        nextSpikeTime = Time.time;
    }
}
