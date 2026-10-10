using UnityEngine;

public class Spike : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float warningTime = 0.5f;
    [SerializeField] private float riseSpeed = 10f;
    [SerializeField] private float lifetime = 1.5f;

    [Header("Components")]
    [SerializeField] private GameObject spike;
    [SerializeField] private GameObject areaWarning;

    private GameObject spikeInstance;
    private Vector3 spawnPosition;
    private float timer;

    void Start()
    {
        if (!ValidateComponents())
        {
            Destroy(gameObject);
            return;
        }

        spawnPosition = FloorDetectionRay.GetFloorPosition(transform.position, groundLayer);

        // Spawned as children so destroying this object cleans everything up
        Instantiate(areaWarning, spawnPosition, Quaternion.identity, transform);
        spikeInstance = Instantiate(spike, spawnPosition, Quaternion.identity, transform);

        // Hide the spike under the ground
        float spikeHeight = spikeInstance.GetComponentInChildren<Renderer>().bounds.size.y;
        spikeInstance.transform.position = spawnPosition + Vector3.down * spikeHeight;
    }

    void Update()
    {
        if (spikeInstance == null) return;

        timer += Time.deltaTime;
        if (timer < warningTime) return;

        spikeInstance.transform.position = Vector3.MoveTowards(
            spikeInstance.transform.position,
            spawnPosition,
            riseSpeed * Time.deltaTime
        );

        if (timer >= warningTime + lifetime) Destroy(gameObject);
    }

    #region Tools
    private bool ValidateComponents()
    {
        if (
            spike == null ||
            areaWarning == null
        ) return false;

        return true;
    }
    #endregion
}
