using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambiarEscena : MonoBehaviour
{

    [SerializeField]
    private Button nivel1;
    [SerializeField] 
    private Button nivel2;
    [SerializeField] 
    private Button nivel3;
    [SerializeField]
    private Button nivel4;
    [SerializeField]
    private Button nivel5;


    private string nivelAlQueIr ="";

    private int ptsSuperarNvl1 = 40;
    private int ptsSuperarNvl2 = 50;
    private int ptsSuperarNvl3 = 60;
    private int ptsSuperarNvl4 = 100;
    private int ptsSuperarNvl5 = 150;



    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MapaDelModoHistoria")
        {
            Debug.Log("ESTAMOS DENTRO DEL MAPA");
            Debug.Log("CARGAMOS DATOS...");
            DataToSave datos = SaveManager.Cargar();
            //int[] datos = SaveManager.Cargar();

            nivel2.enabled = false;
            nivel3.enabled = false;
            nivel4.enabled = false;
            nivel5.enabled = false;
            CheckScores(datos);
            EnlazarNiveles();
        }
       
    }

    private void EnlazarNiveles()
    {

        nivel1.onClick.AddListener(delegate { nivel_onClick("Assets/Scenes/ModoHistoria 1.unity"); });
        nivel2.onClick.AddListener(delegate { nivel_onClick("Assets/Scenes/ModoHistoria 2.unity"); });
        nivel3.onClick.AddListener(delegate { nivel_onClick("Assets/Scenes/ModoHistoria 3.unity"); });
        nivel4.onClick.AddListener(delegate { nivel_onClick("Assets/Scenes/ModoHistoria 4.unity"); });
        nivel5.onClick.AddListener(delegate { nivel_onClick("Assets/Scenes/ModoHistoria 5.unity"); });

    }

    void nivel_onClick(string nivel)
    {
        SceneManager.LoadScene(nivel);
    }

    private void CheckScores(DataToSave datos)
    {
        //if (datos != null)
        //{
        //    int i = 0;
        //    foreach (var dato in datos.puntosPorNivel)
        //    {
        //        int nivel = i + 1;
        //        Debug.Log("Nivel: " + nivel + "Puntos: " + dato);
        //        i++;
        //    }
        //}

        if(datos != null)
        {
            // si la puntación es mayor o igual que la se pide para superar el nivel 1 entonces desbloquea el nivel
            Debug.Log("==========================================");
            Debug.Log("Puntos superar nivel 1: " + ptsSuperarNvl1 + "Puntos obtenidos: " + datos.puntosPorNivel[0]);
            Debug.Log("Puntos superar nivel 2: " + ptsSuperarNvl2 + "Puntos obtenidos: " + datos.puntosPorNivel[1]);
            Debug.Log("Puntos superar nivel 3: " + ptsSuperarNvl3 + "Puntos obtenidos: " + datos.puntosPorNivel[2]);
            Debug.Log("Puntos superar nivel 4: " + ptsSuperarNvl4 + "Puntos obtenidos: " + datos.puntosPorNivel[3]);
            Debug.Log("==========================================");

            if (datos.puntosPorNivel[0] >= ptsSuperarNvl1) { nivel2.enabled = true; }
            if (datos.puntosPorNivel[1] >= ptsSuperarNvl2) { nivel3.enabled = true; }
            if (datos.puntosPorNivel[2] >= ptsSuperarNvl3) { nivel4.enabled=true; }
            if (datos.puntosPorNivel[3] >= ptsSuperarNvl4) { nivel5.enabled=true; }

        }


    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
   
}
