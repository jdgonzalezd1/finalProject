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
    [SerializeField] private PlayerHealth player;
    [SerializeField] private NavMeshAgent enemy;

    [SerializeField] private Collider enemyCollider;

    [SerializeField] private Animator animator;

    private void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        enemyCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        player = FindAnyObjectByType<PlayerHealth>();
        playerPosition = GameObject.FindWithTag("Player").transform;
        locations = GetLocations();
        AttackPosition();
    }

    private void Update()
    {      
        if (PauseMenu.instance.gamePause == false)
        {
            Moving();
            locations = GetLocations();
            enemy.isStopped = false;
            InitiateAttack(playerPosition.position);
        }
        else
        {
            enemy.isStopped = true;
            animator.SetBool("isMoving", false);
        }


    }

    private void Moving()
    {
        if (!player.isAlive)
        {
            enemy.isStopped = true;
            animator.SetBool("isMoving", false);
        }
        else
        {
            enemy.destination = lockedPosition.position;
            animator.SetBool("isMoving", true);
        }
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

    private void AttackPosition()
    {
        lockedPosition = locations[Random.Range(0, locations.Length)];
    }

    private void InitiateAttack(Vector3 playerPosition)
    {
        if (Vector3.Distance(transform.position, lockedPosition.position) < 0.5f)
        {
            enemy.isStopped = true;
            //transform.LookAt(playerPosition);
            animator.SetBool("isMoving", false);
            StartCoroutine(Attack(playerPosition));
        }
    }

    private IEnumerator Attack(Vector3 playerPosition)
    {
        float step = enemy.speed * Time.deltaTime;
        isAttacking = true;
        animator.SetBool("isAttacking", isAttacking);
        enemyCollider.isTrigger = true;
        yield return new WaitForSeconds(1);
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);
        yield return new WaitForSeconds(1);
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        enemyCollider.isTrigger = false;
        enemy.isStopped = false;
    }

    public bool IsAttacking
    {
        get { return isAttacking; }
    }

}
