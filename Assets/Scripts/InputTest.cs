using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{

    private PlayerInput _playerInput;
    private StarterAssetsInputs _input;

    public ObjectPooler objectPooler;
    public Transform spawnPoint;
    public float shootForce = 10f;

    public Transform positionReference;
    public Transform rotationReference;

    public float instantiationDelay = 2f;

    private bool canInstantiate = true;

    public delegate void SpellUsedDElegate();
    public event SpellUsedDElegate SpellUsedEvent;




    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _playerInput = GetComponent<PlayerInput>();
        
    }


    //public void OnCast()
    //{

        //if (canInstantiate)
        //{
            //StartCoroutine(InstantiateCubeWithDelay());
        //}

    //}



        private IEnumerator InstantiateCubeWithDelay()
        {

                //Debug.Log("Works!");
                canInstantiate = false;
                GameObject projectile = objectPooler.GetPooledObject();
            if (projectile != null)
            {
                spawnPoint.position = positionReference.position;               

                projectile.transform.position = spawnPoint.position;
                
                projectile.SetActive(true);

                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                rb.velocity = rotationReference.forward * shootForce;

                if (SpellUsedEvent != null)
                {
                    SpellUsedEvent();
                }
        }
            yield return new WaitForSeconds(instantiationDelay);

        canInstantiate = true;

        }

   



}
  