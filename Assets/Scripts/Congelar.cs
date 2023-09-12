using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Congelar : MonoBehaviour
{

    private ParticleSystem cutEffect;
    public GameObject congelar;

    private void Awake()
    {
        cutEffect = GetComponentInChildren<ParticleSystem>();
        //cutEffect = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            cutEffect.Play();
            GetComponent<Collider>().enabled = false;
            FindObjectOfType<GameManager>().Congelar();
         
            congelar.SetActive(false);

            Destroy(gameObject, 1.2f);
     

        }
    }
}
