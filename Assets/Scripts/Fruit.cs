using UnityEngine;
using UnityEngine.SceneManagement;

public class Fruit : MonoBehaviour
{

    public GameObject whole;
    public GameObject sliced;
    public GameObject horizontal;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceEffect;

    public int points = 1;

  

    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceEffect = GetComponentInChildren<ParticleSystem>();
    }

  
    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        FindObjectOfType<GameManager>().IncreaseScore(points);


        fruitCollider.enabled = false;
        whole.SetActive(false);

        //se comprueba si la dirección es horizontal o vertical (del corte)
        // dependiendo del corte se activa el horizontal o el vertical.



        juiceEffect.Play();

        //Debug.Log("Dirección: " + direction);
        // se gira un poco el modelo para que coincida con la línea de corte.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        if( Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            sliced.SetActive(true);
            // CORTE VERTICAL
            //Debug.Log("CORTE VERTICAL");
            // hacia arriba -90
            if(angle > 0 && angle <=180)
            {
                sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);

            }
            //hacia abajo +90
            else
            {
                sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);

            }

            Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody slice in slices)
            {
                slice.velocity = fruitRigidbody.velocity;
                slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
            }
        }
        else
        {
            horizontal.SetActive(true);
            // CORTE HORIZONTAL
            //Debug.Log("CORTE HORIZONTAL");
            // Corta a la derecha
            if (angle <= 90 && angle >= -90)
            {
                horizontal.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            }
            // Corta a la izquierda
            else
            {
                horizontal.transform.rotation = Quaternion.Euler(0f, 0f, angle-180);
            }



            Rigidbody[] slices = horizontal.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody slice in slices)
            {
                slice.velocity = fruitRigidbody.velocity;
                slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade=other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position,blade.sliceForce);    
        }
    }
}
