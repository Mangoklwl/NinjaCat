using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;
    public GameObject[] PowerUp;

    public GameObject bombPrefab;

    [Range(0f, 1f)] public float bombChance = 0.05f;
    [Range(0f, 1f)] public float powerUpChance = 0.05f;
    //[Range(0f, 1f)] public float powerUpChance = 1f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle= -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 5f;

    private string sceneName;
    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
        sceneName = SceneManager.GetActiveScene().name;//con esto sabe 

    }
    private void OnEnable()
    {
        // CUANDO SE HABILITA SPAWNEO 5 FRUTAS
        // Y luego lo deshabilito
        StartCoroutine(Spawn());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);// espera dos segundos



        while (enabled) //mientras este habilitado el checkbox del scrip
        {   
            GameObject prefab= fruitPrefabs[Random.Range(0,fruitPrefabs.Length)];// coge un objeto aleatorio de fruiPrefabs
          
            GameObject prefabpoweUp = PowerUp[Random.Range(0, PowerUp.Length)];

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));


            //if (sceneName.Contains("ModoHistoria"))
            //{
            //    prefabpoweUp = PowerUp[1];
            //}
            //else if (sceneName.Contains("ModoLibre"))
            //{
            //    prefabpoweUp = PowerUp[0];
            //}

            if (Random.value < powerUpChance)
            {
                //Debug.Log("SE TIENE QUE SPAWNEAR UNA BOMBA");
                prefab = prefabpoweUp;

            }
            if (Random.value < bombChance)
            {
                prefab = bombPrefab;
            }

            Vector3 position = new Vector3();//crea un objeto vector con 3 posiciones
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);//establece posicion aleatoria entre en maximo y minimo del spawnarea
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);//establece posicion aleatoria entre en maximo y minimo del spawnarea
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);//establece posicion aleatoria entre en maximo y minimo del spawnarea

            //Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle,maxAngle));//que rote en el  eje z en minablge y maxangle de forma aleatoria
            //(direccion en la que lanza la fruta, derecha  izquierda..)

            GameObject fruit = Instantiate(prefab, position, rotation);//clonas el objeto especificandole su posicion y rotacion
            Destroy(fruit, maxLifetime);//destruye el objeto pasado un tiempo(maxLifetime)


            float force=Random.Range(minForce,maxForce);//la fuerza es un valor aleatorio entre minforce y maxforce
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);// Añade una fuerza al componente que se encarga de las físicas (RigidBody).
            // La fuerza se aplica diciendole el vector hacia el que lo va a hacer y el tipo de fuerza que és

          yield return new WaitForSeconds(Random.Range(minSpawnDelay,maxSpawnDelay));
        }
    }
}
