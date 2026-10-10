using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IHealth))]
public class HealthDisplay : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private IHealth health;

    void Start()
    {
        health = GetComponent<IHealth>();

        if (!ValidateComponents()) return;
        healthSlider.maxValue = health.GetHealth();
    }

    void Update()
    {
        if (!ValidateComponents()) return;

        healthSlider.value = health.GetHealth();
    }

    #region Tools
    private bool ValidateComponents()
    {
        if (
            healthSlider == null ||
            health == null
        ) return false;

        return true;
    }
    #endregion
}
