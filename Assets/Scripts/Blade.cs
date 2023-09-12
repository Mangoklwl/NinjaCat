using UnityEngine;

public class Blade : MonoBehaviour
{
    public Vector3 direction { get; private set; }
    private Camera mainCamera;

    private Collider sliceCollider;
    private TrailRenderer sliceTrail;

    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;
    
    private bool slicing;

    private void Awake()
    {
        mainCamera = Camera.main;
        sliceCollider = GetComponent<Collider>();
        sliceTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlice();
    }
    private void OnDisable()
    {
        StopSlice();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))//si pulsas el boton derecho del raton
        {
            StartSlice();
        }
        else if(Input.GetMouseButtonUp(0))//si dejas de pulsar el btn derecho del raton
        {
            StopSlice();
        }
        else if(slicing) 
        {
            ContinueSlice();
        }
    }

    private void StartSlice()
    {
        Vector3 Position = mainCamera.ScreenToWorldPoint(Input.mousePosition);//sacando coordenadas del raton desde la camara
        Position.z = 0f;

        transform.position = Position;


        slicing = true;
        sliceCollider.enabled = true;
        sliceTrail.enabled= true;
        sliceTrail.Clear();
    }

    private void StopSlice()
    {
        slicing = false;
        sliceCollider.enabled = false;
        sliceTrail.enabled = false;
    }

    private void ContinueSlice()
    {
        Vector3 newPosition= mainCamera.ScreenToWorldPoint(Input.mousePosition);//sacando coordenadas del raton desde la camara
        newPosition.z = 0f;// que posicion z(profundidad) sea siempre 0(

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        sliceCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;


    }
}
