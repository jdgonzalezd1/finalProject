using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaManagement : MonoBehaviour
{
    public float stamina = 0;

    private bool isRecovering = false;

    public float staminaRecoveryRate = 5.0f;

    private GameManager gameManager;

    private ThirdPersonController thirdPersonController;

    // Start is called before the first frame update
    void Awake()
    {
        stamina = 100;

        gameManager = FindAnyObjectByType<GameManager>();


        thirdPersonController = GetComponent<ThirdPersonController>();

        if (thirdPersonController != null)
        {
            thirdPersonController.DodgeUsedEvent += DodgeUsed;
        }

    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void DodgeUsed()
    {
        if (stamina > 0)
        {
            DecrementStamina(35); // Decrementa la stamina cuando se esquiva.
            if (!isRecovering)
            {
                StartCoroutine(StartStaminaRecovery()); // Inicia la recuperación de la stamina.
            }
        }
    }


    private IEnumerator StartStaminaRecovery()
    {
        isRecovering = true;
        while (stamina < 100)
        {
            yield return new WaitForSeconds(2f);
            stamina += staminaRecoveryRate;

            if (stamina >= 35)
            {
                thirdPersonController.canDodge = true;
            }

            if (stamina > 100)
            {
                stamina = 100;
            }
            
        }
        isRecovering = false;
    }


    private void DecrementStamina(int amount)
    {
        stamina -= amount;
        if (stamina < 0)
        {
            stamina = 0;
        }
        if (stamina < 35)
        {
            thirdPersonController.canDodge = false;
        }
    }


    private void OnDestroy()
    {
        if (thirdPersonController != null)
        {
            thirdPersonController.DodgeUsedEvent -= DodgeUsed;
        }
    }
}
