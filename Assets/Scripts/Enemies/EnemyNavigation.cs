using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] private Transform[] locations;
    [SerializeField] private Transform lockedPosition;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private NavMeshAgent enemy;


    private void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        attackPosition();
    }

    private void Update()
    {
        enemy.destination = lockedPosition.position;
    }

    private void attackPosition()
    {
        lockedPosition = locations[Random.Range(0, locations.Length)];        
    }

    private void initiateAttack()
    {
        if (Vector3.Distance(transform.position, lockedPosition.position) < 0.5f)
        {
            enemy.isStopped = true;

        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1);


    }

}
