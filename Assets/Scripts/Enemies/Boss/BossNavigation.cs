using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class BossNavigation : MonoBehaviour
{
    [SerializeField] public NavMeshAgent bossMovement;

    [SerializeField] private float distance;
    [SerializeField] private bool isMoving;

    [SerializeField] private Transform player;
    [SerializeField] private BossController bossCon;

    private void Start()
    {
        bossMovement = GetComponent<NavMeshAgent>();
        bossCon = GetComponent<BossController>();
        player = GameObject.FindWithTag("Player").transform;
        Movement();
    }


    private void Update()
    {
        //Movement();
    }

    private void Movement()
    {
        bossMovement.destination = player.position;
        //CheckDistance();
    }
    
    private void CheckDistance()
    {
        distance = bossMovement.remainingDistance;

        if (distance > 8f)
        {          
            //isMoving = false;
            //StopCoroutine(CastSpell(isMoving, player.position));
            //StartCoroutine(CastSpell(isMoving, player.position));
        }
        if (distance < 2.5f)
        {
            isMoving = false;
            StopCoroutine(InitiateAttack(player.position));
            StartCoroutine(InitiateAttack(player.position));
        }
    }


    private IEnumerator CastSpell(bool isMoving, Vector3 target)
    {
        if (!isMoving && bossCon.AttackCooldown <= 0)
        {
            bossCon.AttackCooldown = 2f;
            //bossCon.IsCasting = true;
            //throwSomething
            bossMovement.isStopped = true;
            yield return new WaitForSeconds(bossCon.AttackCooldown);
            //bossCon.IsCasting = false;
        }
        bossMovement.isStopped = false;
    }
    
    public IEnumerator InitiateAttack(Vector3 target)
    {
        float step = bossMovement.speed * Time.deltaTime;
        bossMovement.isStopped = true;
        //bossCon.IsAttacking = true;
        Debug.Log("Attacking");

        //animator.SetBool("isAttacking", isAttacking);
        //enemyCollider.isTrigger = true;

        yield return new WaitForSeconds(1);
        //bossMovement
        yield return new WaitForSeconds(1);
        //bossCon.IsAttacking = false;
        bossMovement.isStopped = false;
        Debug.Log("Finally");
        //animator.SetBool("isAttacking", isAttacking);
        //enemyCollider.isTrigger = false;
        //enemy.isStopped = false;
    }

    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }

    public float Distance
    {
        get { return distance; }
    }

    public Transform Player
    { 
        get { return player; } 
    }

}
