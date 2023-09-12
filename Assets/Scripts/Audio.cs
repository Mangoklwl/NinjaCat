using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource audio;



    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
}
