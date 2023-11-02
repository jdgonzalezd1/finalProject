using System.Collections;
using UnityEngine;

public class CubeCollisionHandler : MonoBehaviour
{
    public float deactivateDelay = 2f;

    private void OnEnable()
    {
        StartCoroutine(DeactivateAfterDelay());
    }

    private void OnCollisionEnter(Collision collision)
    {
        // You can add any additional logic here, such as checking for specific tags or layers
        gameObject.SetActive(false);

        if (collision.gameObject.CompareTag("Target") || collision.gameObject.CompareTag("Enemy"))

        {
            collision.gameObject.SetActive(false);
        }
    }

    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(deactivateDelay);

        // Deactivate the cube if it hasn't collided with another object after the specified delay
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
}
