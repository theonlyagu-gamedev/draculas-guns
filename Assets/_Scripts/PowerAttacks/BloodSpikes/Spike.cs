using UnityEngine;

public class Spike : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float maxHeight = -0.4f;
    [SerializeField] private float startHeight = -2.9f;
    [SerializeField] private float riseRate = 0.5f;
    [SerializeField] private float lifetime = 1.5f;
    private float lifeTimer = 0f;

    [Header("Components")]
    [SerializeField] private GameObject spike;
    [SerializeField] private ParticleSystem destroyEffect;
    private Collider col;
    private bool hasRisen = false;

    void Start()
    {
        col = GetComponent<Collider>();
        SetCollision(false);

        if (spike == null) return;

        Vector3 localPosition = spike.transform.localPosition;
        localPosition.y = startHeight;
        spike.transform.localPosition = localPosition;
    }

    void Update()
    {
        if (spike == null) return;

        if (hasRisen)
        {
            lifeTimer += Time.deltaTime;
            if (lifeTimer >= lifetime) DestroySpike();
            return;
        }

        Vector3 localPosition = spike.transform.localPosition;
        localPosition.y = Mathf.MoveTowards(localPosition.y, maxHeight, riseRate * Time.deltaTime);
        spike.transform.localPosition = localPosition;

        if (Mathf.Approximately(localPosition.y, maxHeight))
        {
            hasRisen = true;
            SetCollision(true);
        }
    }

    private void SetCollision(bool enabled)
    {
        if (col == null) return;

        col.enabled = enabled;
    }

    private void DestroySpike()
    {
        if (destroyEffect != null)
        {
            // ParticleSystem effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            // effect.Play();

            // // Clean up the particle once it has finished playing
            // ParticleSystem.MainModule main = effect.main;
            // Destroy(effect.gameObject, main.duration + main.startLifetime.constantMax);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasRisen && other.CompareTag("Player"))
        {
            Debug.Log("Player has taken damage");
        }
    }
}
