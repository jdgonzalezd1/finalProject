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
    public int stamina;

    private bool canBeHurt = true;

    public float hurtDelay = 2f;

    private InputTest inputTestInstance;







    void Start()

    {
        animator = GetComponent<Animator>();

        health = 100;


        mana = 100;
        inputTestInstance = GetComponent<InputTest>();
        if (inputTestInstance != null)
        {
            inputTestInstance.SpellUsedEvent += SpellUsed;
        }


        stamina = 100;

        Debug.Log("Current health is " + health + " Current mana is: " + mana + " Current stamina is: " + stamina);

        


        
    }

    void Update()
    {

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.CompareTag("Target"))

        {
            if (canBeHurt)

            {
                StartCoroutine(canBeHurtDelay());
            }

            
        }

       
    }


    public IEnumerator canBeHurtDelay()
    {
        if (health > 0)
        {

            canBeHurt = false;

            health -= 25;

            Debug.Log("Damage Taken: 25 ");

            Debug.Log("Current health is " + health);

            yield return new WaitForSeconds(hurtDelay);

            canBeHurt = true;
        }

        else

        {

            Debug.Log("Current health is " + health);
            Debug.Log("You are DEAD.");
            animator.SetBool("Death", true);
            ThirdPersonController thirdPersonController = GetComponent<ThirdPersonController>();
            thirdPersonController.movementEnabled = false;


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
    }


    private void SpellUsed()
         {
            
                if (mana > 0)
                {

                    Debug.Log("Spell Used");
                    DecrementMana(25);
                    Debug.Log(mana);
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


 



