using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    

    private void OnTriggerEnte(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            
            GetComponent<Collider>().enabled = false;
            FindObjectOfType<GameManager>().Explode();
            FindObjectOfType<GameManager>().SpawnBomb(transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
   
}   }
