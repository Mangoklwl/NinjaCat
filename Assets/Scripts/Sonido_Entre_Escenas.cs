//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sonido_Entre_Escenas : MonoBehaviour
{
    private Sonido_Entre_Escenas instance;
   
    public Sonido_Entre_Escenas Instance { 
        
        get 
        { 
            
            return instance; 
        
        } 
    
    }

    private void OnEnable()
    {
        Sonido_Entre_Escenas[] sound = FindObjectsOfType<Sonido_Entre_Escenas>();   
        if(sound.Length > 1 )
        {
            Destroy(gameObject);
        }

    }



    private void Awake()
    {
        if (SceneManager.sceneCount>1)
        {
            Destroy(gameObject);
        }

        if(instance!=null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}
