using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    private Animator animator;

    public float health;
    public float mana;
    public bool isAlive = true;
    

    private bool canBeHurt = true;

    public float hurtDelay = 2f;

    public bool isRecovering = false;

    public float manaRecoveryRate = 5.0f;

    private InputTest inputTestInstance;
    private ThirdPersonController thirdPersonController;
    private HUD hud;

    void Awake()

    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        hud = FindAnyObjectByType<HUD>();

        health = 100;
        mana = 700;
        

        inputTestInstance = GetComponent<InputTest>();

        if (inputTestInstance != null)
        {
            inputTestInstance.SpellUsedEvent += SpellUsed;
        }


        
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {        
        if (hit.gameObject.CompareTag("DeathZone") && canBeHurt)
        {                               
            StartCoroutine(canBeHurtDelay(health));
        }
    }


    public IEnumerator canBeHurtDelay(float damage)
    {
        if (health > 0)
        {            
            canBeHurt = false;
            health -= damage;

            if (health <= 0)
            {
                animator.SetBool("Death", true);
                isAlive = false;
                ThirdPersonController thirdPersonController = GetComponent<ThirdPersonController>();
                thirdPersonController.movementEnabled = false;
                hud.GameOver();
            }

            hud.UpdateHealthBar(health);
            
            yield return new WaitForSeconds(hurtDelay);

            canBeHurt = true;

        }
    }


    private void DecrementMana(int amount)
    {
        mana -= amount;

        if (mana <= 0)
        {
            mana = 0;
            animator.SetBool("NoCast", true);
        }
        

        if (!isRecovering)
        {            
            StartCoroutine(StartManaRecovery());
        }

        hud.UpdateManaBar(mana);
    }

    private IEnumerator StartManaRecovery()
    {
        isRecovering = true;
        
        while (mana < 700)
        {
            yield return new WaitForSeconds(2.0f);            
            mana += manaRecoveryRate;            

            if (mana > 700)
            {
                mana = 700;
            }
            hud.UpdateManaBar(mana);
        }
        isRecovering = false;
    }


    private void SpellUsed()
    {
        if (mana > 0)
        {            
            DecrementMana(25);            
        }
    }
   


    public void ResetNoCast()
    {
        animator.SetBool("NoCast", false);
    }

    public void Attacked(float damage)
    {    
           StartCoroutine(canBeHurtDelay(damage));  
    }



    private void OnDestroy()
    {
        if (inputTestInstance != null)
        {
            inputTestInstance.SpellUsedEvent -= SpellUsed;
        }
    }

}






