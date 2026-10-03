using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class IHealth : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float health = 100f;
    [SerializeField] private float damagePoints = 10;
    private float maxHealth = 100f;

    public void IncreaseHealth(float amount)
    {
        health = Mathf.Clamp(health + amount, 0f, maxHealth);
    }

    public void TakeDamage()
    {
        health = Mathf.Clamp(health - damagePoints, 0f, maxHealth);
    }
}
