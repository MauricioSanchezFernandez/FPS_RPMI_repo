using UnityEngine;
using UnityEngine.InputSystem;

public class GunSystem : MonoBehaviour
{

    #region General Variables
    [Header("General References")]
    [SerializeField] Camera fpsCam; //ref si disparamos desde el centro de la camara
    //[SerializeField] Transform shootPoint; //ref si queremos disparar punta del cañon
    [SerializeField] LayerMask impactLayer; //layer con la que el raycast interactua
    RaycastHit hit; //almacen de informacion de los objetos puede impactar


    [Header("Weapon Parameters")]
    [SerializeField] int damage = 10; //daño del arma por bala
    [SerializeField] float range = 100f; //distancia de disparo / rango de distancia range= lomngitud del raycast
    [SerializeField] float spread = 0f; //radio de dispersion en el arma (como una escopeta etc)
    [SerializeField] float shootingCooldown = 0.2f; //tiempo entre disparos 
    [SerializeField] float reloadTime = 1.5f; //tiempo recarga 
    [SerializeField] bool allowButtonHold = false; //disparo dependiendo si dejo apretado o no-> por click (falso), mantener (verdadero)

    [Header("Bullet Management")]
    [SerializeField] int ammoSize = 30; //cantidad balas por cargador
    [SerializeField] int bulletsPerTap = 1; //cantidad de balas disparadas por disparo
    [SerializeField] int bulletsLeft; //cantidad de balas que nos queda en el cargador

    [Header("Feedback References")]
    [SerializeField] GameObject impactEffect; //ref al VFX de impacto de bala

    [Header("Dev - Gun State Bools")]
    [SerializeField] bool shooting; //indica si estamos disparando
    [SerializeField] bool canShoot; //indica si podemos disparar en x momento del juego
    [SerializeField] bool reloading; // indica si estamos en proceso de recarga



    #endregion



    private void Awake()
    {
        bulletsLeft = ammoSize; //al iniciar partida, tenemos el cargador lleno
        canShoot = true; //al iniciar partida, se puede disparar
    }

     
    void Update()
    {
        
    }


    void Shoot()  
    {
        //metodo mas importante
        // se define el disparo por raycast = UTILIZABLE POR CUALQUIER MECANICA

        //almacenar la direccion de disparo y modificarla en caso de haber spread
        Vector3 direction = fpsCam .transform.forward; //se lanza rayo hacia delante de la camara

        //añadir dispersion aleatoria segun valor de spread

        direction.x += Random.Range(-spread, spread);
        direction.y += Random.Range(-spread, spread);

        //DECLARACION DE RAYCAST
        // if (Physics.Raycast(fpsCam.transform.position, direction, out hit, Mathf.Infinity, impactLayer)) //raycastinfinito
        //de donde sale, hacia donde va el rayo, cual es el almacen en que se almacena el valor de impacto, lo largo que es el raycast, a que capa de collider afecta este ray
        // if (Physics.Raycast(origen del rayo, direccion, almacen de la info del impacto, longitud del rayo, layer de impacto)
        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range, impactLayer)) 
        {

            //AQUI PUEDO CODEAR TODOS LOS EFECTOS QUE QUIERO PARA INTERCACION
            Debug.Log(hit.collider.name); //nombre de objeto que dice que ha impactado

        }

    }



    #region Input Methods

    public void OnShoot(InputAction.CallbackContext context)
    {
        
    }

    public void OnReload(InputAction.CallbackContext context)
    {

    }

    #endregion
}
