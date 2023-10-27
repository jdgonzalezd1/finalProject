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
        stamina = 100;


        inputTestInstance = GetComponent<InputTest>();

        if (inputTestInstance != null)
        {
            inputTestInstance.SpellUsedEvent += SpellUsed;
        }


        
        //Displays a message on the console about the initial value of health, mana and stamina:
        Debug.Log("<b><size=14>Current health is: <color=green>" + health +
            "</color> Current mana is: <color=blue>" + mana + "</color> Current stamina is: <color=orange>" +
            stamina + "</color></size></b>");


        //Can be deleted---->
       /* Debug.Log("<b>This is bold text</b>");
        Debug.Log("<i>This is italicized text</i>");
        Debug.Log("<size=30>This is large text</size>");
        Debug.Log("<color=red>This is red text</color>");*/





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
            Debug.Log("<b><size=14>Damage Taken: <color=red>25</color></size></b>");
            

            canBeHurt = false;

            health -= 25;

            Debug.Log("<b><size=14>Current health is <color=green>" + health + "</color></size></b>");


            if (health <= 0)

            {

                //Debug.Log("<b><size=14>Current health is <color=green>" + health + "</color></size></b>");
                Debug.Log("<b><size=14><color=maroon>You are DEAD.</color></size></b>");
                animator.SetBool("Death", true);
                ThirdPersonController thirdPersonController = GetComponent<ThirdPersonController>();
                thirdPersonController.movementEnabled = false;


            }


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


 



