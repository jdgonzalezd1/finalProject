using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    [SerializeField] public float health;

    [SerializeField] private HUD hud;

    private void Awake()
    {
        hud = FindAnyObjectByType<HUD>();
        health = 100;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        hud.UpdateBossHealth(health);
    }
}
