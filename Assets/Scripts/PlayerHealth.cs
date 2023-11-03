using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    private Animator animator;

    public int health;
    public int mana;
    

    private bool canBeHurt = true;

    public float hurtDelay = 2f;

    private InputTest inputTestInstance;
    private HUD hud;

    void Awake()

    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        hud = FindAnyObjectByType<HUD>();

        health = 100;
        mana = 1000;
        //stamina = 100;
        hud.UpdateHealth(health);
        hud.UpdateMana(mana);

        inputTestInstance = GetComponent<InputTest>();

        if (inputTestInstance != null)
        {
            inputTestInstance.SpellUsedEvent += SpellUsed;
        }


        
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        int damage;
        if (hit.gameObject.CompareTag("Target") && canBeHurt)
        {
            damage = 25;
            StartCoroutine(canBeHurtDelay(damage));
        }
        else if (hit.gameObject.CompareTag("DeathZone") && canBeHurt)
        {            
            damage = health;
            StartCoroutine(canBeHurtDelay(damage));
        }



    }


    public IEnumerator canBeHurtDelay(int damage)
    {
        if (health > 0)
        {
            

            canBeHurt = false;

            health -= damage;

            

            if (health <= 0)

            {

                
                animator.SetBool("Death", true);
                ThirdPersonController thirdPersonController = GetComponent<ThirdPersonController>();
                thirdPersonController.movementEnabled = false;
                hud.GameOver();

            }
            hud.UpdateHealth(health);
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

        hud.UpdateMana(mana);
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



    private void OnDestroy()
    {
        if (inputTestInstance != null)
        {
            inputTestInstance.SpellUsedEvent -= SpellUsed;
        }
    }



}






