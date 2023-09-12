using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
//using UnityEditor.Build.Content;
//using Unity.Services.Authentication;
//using System.Threading;

public class GameManager : MonoBehaviour
{

    public Text scoreText;
    public Text scoreToWinText;
    public Image fadeImage;
    public GameObject[] vidas;
    

    public Text timerText;
    public int scoreToWin;
    public float timer;
    public int nivel;

    [SerializeField] private GameObject efectoExplosion;
    private Blade blade;
    private Spawner spawner;

    public int score;
    public static string sceneName;
    private float totalTimePoweup = 3f;
    private  int vida = 2;
    private  string ptsScene;
   
    private float timeStamp;
    //private float tiempoexplosion = 3f;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
        sceneName = SceneManager.GetActiveScene().name;//con esto sabe 
        ptsScene = "Assets/Scenes/" + "HistoriaPts.unity";
        Debug.Log("ESTE NIVEL ES EL: " + nivel);

       
    }

    public void Update()
    {
        if(timeStamp <= Time.time)
        {
            spawner.maxSpawnDelay = 1;
            spawner.bombChance = 0.1f;
            spawner.powerUpChance = 0.05f;
        }

        if (sceneName.Contains("ModoHistoria"))
        {
        
            timer -= Time.deltaTime;
            timerText.text = "" + timer.ToString("f0");


            if (timer <= 0)
            {
                blade.enabled = false;
                spawner.enabled = false;

                //Debug.Log("HAS PERDIDO");
                StartCoroutine(ExplodeSequence());
            }
            else if (score >= scoreToWin)
            {
                blade.enabled = false;
                spawner.enabled = false;

                //has ganado;
                //Debug.Log("HAS ganado");
                StartCoroutine(ExplodeSequence());
            }

        }
        else if (sceneName.Contains("ModoLibre"))
        {

        }
            
    }

    public void Congelar()
    {
        timeStamp = Time.time + totalTimePoweup;

        //Debug.Log("POWER UP DE CONGELAR");
       
        spawner.maxSpawnDelay = 0;
        spawner.bombChance = 0;
        spawner.powerUpChance = 0;
        
      
    }

    public void SetScoreText(string value)
    {
        if (sceneName.Contains("ModoHistoria"))
        {
            scoreText.text = value + " / " + scoreToWin.ToString();

        }
        else
        {
            scoreText.text = value;
        }
         if (sceneName.Contains("ModoLibre"))
        {
            scoreText.text = value;
        }
    }
    private void Start()
    {
        NewGame();
        //Debug.Log(sceneName);

    }
    private void NewGame()
    {
        Time.timeScale = 1f;
        ClearScene();

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;

        SetScoreText(score.ToString());

    }

    private void OnDisable()
    {

        if (sceneName.Contains("ModoHistoria"))
        {
            //Debug.Log("SE HA DESTRUIDO");
            PlayerPrefs.SetInt("puntos", score);
            PlayerPrefs.SetString("escena", sceneName);

            Debug.Log("SE VA A GUARDAR...");
            Debug.Log("Puntos: " +  score);
            Debug.Log("Nivel: " + nivel);
            SaveManager.Guardar(score, nivel);

            //DataToSave datos = SaveManager.Cargar();

            //if (datos != null)
            //{
            //    if (score > datos.puntosPorNivel[nivel - 1])
            //    {
            //        SaveManager.Guardar(score, nivel);
            //    }
            //}
            //else
            //{
            //    SaveManager.Guardar(score, nivel);
            //}
        }
        else if (sceneName.Contains("ModoLibre"))
        {
            // TO-DO GUARDAR PARA EL MODO LIBRE
            //Debug.Log("SE HA DESTRUIDO");
            PlayerPrefs.SetInt("puntos", score);
            PlayerPrefs.SetString("escena", sceneName);

            //DataToSave datos = SaveManager.Cargar();

            //if (datos != null)
            //{
            //    SaveManager.Guardar2(score);
            //}
           
        }
        

    }


    public void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
       
    }
    public void IncreaseScore(int points)
    {
        score += points;
        SetScoreText(score.ToString());
    }

    public void SpawnBomb(Vector3 bombpos, Quaternion identity)
    {
        
        bombpos.z -= 2;
        GameObject exp = Instantiate(efectoExplosion, bombpos, identity);
        float lifetime = 1.0f;
        Destroy(exp, lifetime);

    }

  
    public void Vida()
    {
        if (sceneName == "ModoLibre")
        {
            //Debug.Log("OBTIENE UNA VIDA");
            if(vida == 1)
            {
                vida++;
                vidas[0].SetActive(true);  
                
            }

        }
    }

    public void Explode()
    {
        if (sceneName == "ModoLibre")
        {
            // SOLO LO HACE SI ES MODO LIBRE
            vida--;

            if (vida == 0)
            {
                Destroy(vidas[1].gameObject);
                blade.enabled = false;
                spawner.enabled = false;
                StartCoroutine(ExplodeSequence());

            }
            else
            {
                if (vida == 1)
                {
                    
                    //Destroy(vidas[0].gameObject);
                    vidas[0].SetActive(false);
                }
            }
        }
        else if(sceneName.Contains( "ModoHistoria"))
        {
            //// SOLO LO HACE SI ES MODO JUEGO 1
        
           score -= 10;
        }
    }

    private IEnumerator ExplodeSequence() 
    {

        float elapsed = 0f;
        float duration = 0.5f;
      
        // Fade to white
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        //NewGame();

        elapsed = 0f;

        // Fade back in
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
        SceneManager.LoadScene(ptsScene);

        //NewGame();

        //elapsed = 0f;

        //while (elapsed < duration)
        //{
        //    float t = Mathf.Clamp01(elapsed / duration);
        //    fadeImage.color = Color.Lerp(Color.white, Color.clear, t);


        //    elapsed += Time.unscaledDeltaTime;

        //    yield return null;
        //}
        //yield return new WaitForSecondsRealtime(1f);
    }

}
