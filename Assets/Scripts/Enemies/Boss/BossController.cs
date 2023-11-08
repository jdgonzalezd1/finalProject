using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    [SerializeField] public BossNavigation bossNavi;
    [SerializeField] private float attackCooldown = 0;
    
    [SerializeField] private bool isCasting;
    [SerializeField] private bool isAttacking;
    


    private void Start()
    {
        bossNavi = GetComponent<BossNavigation>();
    }
    private void Update()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }
    
    
    public bool IsCasting
    {
        get { return isCasting; }
        set { isCasting = value; }
    }

    public bool IsAttacking
    {
        get { return isAttacking; } 
        set { isAttacking = value; }
    }
    
    public float AttackCooldown
    {
        get { return attackCooldown; }
        set { attackCooldown = value; }
    }
    
        
}
