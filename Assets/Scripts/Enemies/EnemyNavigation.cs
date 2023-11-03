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

    [SerializeField] private Animator animator;

    private void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerPosition = GameObject.FindWithTag("Player").transform;
        locations = GetLocations();
        AttackPosition();
    }

    private void Update()
    {
        locations = GetLocations();
        Moving();
        InitiateAttack(playerPosition.position);
    }

    private Transform[] GetLocations()
    {
        List<Transform> trackPositions = new();
        foreach (Transform child in playerPosition.GetChild(2))
        {
            trackPositions.Add(child);
        }
        return trackPositions.ToArray();
    }

    private void Moving()
    {
        enemy.destination = lockedPosition.position;
        animator.SetBool("isMoving", true);        
    }

    private void AttackPosition()
    {
        lockedPosition = locations[Random.Range(0, locations.Length)];
    }

    private void InitiateAttack(Vector3 playerPosition)
    {
        if (Vector3.Distance(transform.position, lockedPosition.position) < 0.5f)
        {
            enemy.isStopped = true;
            transform.LookAt(playerPosition);
            animator.SetBool("isMoving", false);
            StartCoroutine(Attack(playerPosition));
        }
    }

    private IEnumerator Attack(Vector3 playerPosition)
    {
        //Debug.Log("I'm executing");

        isAttacking = true;
        animator.SetBool("isAttacking", isAttacking);
        yield return new WaitForSeconds(1.5f);
        float step = enemy.speed * Time.deltaTime;                
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);
        yield return new WaitForSeconds(2);        
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        enemy.isStopped = false;       
    }

}
