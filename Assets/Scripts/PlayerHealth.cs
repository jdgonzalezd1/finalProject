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
    private GameManager gameManager;

    void Start()

    {
        animator = GetComponent<Animator>();
        gameManager = FindAnyObjectByType<GameManager>();

        health = 100;
        mana = 100;
        stamina = 100;
        gameManager.UpdateHealth(health);
        gameManager.UpdateMana(mana);

        inputTestInstance = GetComponent<InputTest>();

        if (inputTestInstance != null)
        {
            inputTestInstance.SpellUsedEvent += SpellUsed;
        }


        //Displays a message on the console about the initial value of health, mana and stamina:
        /*
        Debug.Log("<b><size=14>Current health is: <color=green>" + health +
            "</color> Current mana is: <color=blue>" + mana + "</color> Current stamina is: <color=orange>" +
            stamina + "</color></size></b>");
        */
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
            //Debug.Log("<b><size=14>Damage Taken: <color=red>25</color></size></b>");

            canBeHurt = false;

            health -= damage;

            //Debug.Log("<b><size=14>Current health is <color=green>" + health + "</color></size></b>");

            if (health <= 0)

            {

                //Debug.Log("<b><size=14>Current health is <color=green>" + health + "</color></size></b>");
                //Debug.Log("<b><size=14><color=maroon>You are DEAD.</color></size></b>");
                animator.SetBool("Death", true);
                ThirdPersonController thirdPersonController = GetComponent<ThirdPersonController>();
                thirdPersonController.movementEnabled = false;
                gameManager.GameOver();

            }
            gameManager.UpdateHealth(health);
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

        gameManager.UpdateMana(mana);
    }


    private void SpellUsed()
    {

        if (mana > 0)
        {
            //Debug.Log("Spell Used");
            DecrementMana(25);
            //Debug.Log(mana);
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






