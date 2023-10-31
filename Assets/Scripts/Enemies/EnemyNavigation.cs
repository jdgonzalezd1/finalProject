using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [Tooltip("Field that indicates if enemy is currently attacking")]
    [SerializeField] private bool isAttacking;

    [SerializeField] private Transform[] locations;
    [SerializeField] private Transform lockedPosition;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private NavMeshAgent enemy;
    
    private void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        locations = GetLocations();
        AttackPosition();     
    }

    private void Update()
    {
        locations = GetLocations();
        enemy.destination = lockedPosition.position;
        InitiateAttack(playerPosition.position);        
    }

    private Transform[] GetLocations()
    {
        List<Transform> trackPositions = new();
        foreach(Transform child in playerPosition.GetChild(2))
        {                        
            trackPositions.Add(child);
        }
        return trackPositions.ToArray();        
    }

    private void AttackPosition()
    {
        lockedPosition = locations[Random.Range(0, locations.Length)];        
    }

    private void InitiateAttack(Vector3 playerPosition)
    {        
        if (Vector3.Distance(transform.position, lockedPosition.position) < 1f)
        {            
            enemy.isStopped = true;
            StartCoroutine(Attack(playerPosition));
        }
    }

    private IEnumerator Attack(Vector3 playerPosition)
    {
        yield return new WaitForSeconds(1);
        float step = enemy.speed * Time.deltaTime;
        isAttacking = true;
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);
        yield return new WaitForSeconds(1);
        enemy.isStopped = false;
        isAttacking = false;
    }

}
