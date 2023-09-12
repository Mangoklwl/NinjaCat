using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    private ParticleSystem cutEffect;
    public GameObject botiquin;

    private void Awake()
    {
        cutEffect = GetComponentInChildren<ParticleSystem>();
       
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            cutEffect.Play();
            GetComponent<Collider>().enabled = false;
            FindObjectOfType<GameManager>().Vida();
           
            botiquin.SetActive(false);
                
            Destroy(gameObject, 1.2f);
          

        }
    }
    }
