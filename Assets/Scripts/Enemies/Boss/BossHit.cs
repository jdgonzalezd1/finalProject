using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHit : MonoBehaviour
{
    private PlayerHealth health;
    private BossController controller;

    private void Awake()
    {
        health = FindAnyObjectByType<PlayerHealth>();
        controller = FindAnyObjectByType<BossController>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            health.Attacked(controller.DamageDealt);
        }
    }
}
