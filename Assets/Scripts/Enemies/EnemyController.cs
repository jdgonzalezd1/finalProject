using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float damage = 25;

    [SerializeField] private EnemyNavigation attackStatus;
    [SerializeField] private EnemyNavigation targetStatus;
    [SerializeField] private PlayerHealth player;

    private void Start()
    {
        attackStatus = GetComponent<EnemyNavigation>();
        player = FindAnyObjectByType<PlayerHealth>();
    }
    
    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        if (attackStatus.IsAttacking && collision.collider.gameObject.CompareTag("Player"))
        {
            player.Attacked(damage);
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collided");
        if (attackStatus.IsAttacking && other.gameObject.CompareTag("Player"))
        {
            player.Attacked(damage);
        }
    }
}
