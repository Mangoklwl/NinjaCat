//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reiniciar : MonoBehaviour
{
    [SerializeField]
    private Button volverInicio;
    [SerializeField]
    private Button volverAjugar;

    public Text puntuacion;
    //void Awake()
    //{
    //    puntuacion.text = GameManager.score.ToString();
    //    volverInicio.onClick.AddListener(volverInicio_onClick);
    //    volverAjugar.onClick.AddListener(volverAjugar_onClick);
    //}

    public void Start()
    {
        if(SceneManager.GetActiveScene().name == "HistoriaPts")
        {

            //puntuacion.text = GameManager.score.ToString();
            puntuacion.text = PlayerPrefs.GetInt("puntos").ToString();

            volverInicio.onClick.AddListener(volverInicio_onClick);//al boton añadele el listener y a eso añades la funcion 
            volverAjugar.onClick.AddListener(volverAjugar_onClick);
        } 
    }

    void volverInicio_onClick()
    {
        SceneManager.LoadScene("Assets/Scenes/Menu.unity");
    }

    void volverAjugar_onClick()
    {
        //SceneManager.LoadScene(GameManager.sceneName);
        SceneManager.LoadScene(PlayerPrefs.GetString("escena"));
    }

}
