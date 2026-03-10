using UnityEngine;
using UnityEngine.AI; //libreria  decomponente NavMesh
public class EnemyAIBase : MonoBehaviour
{

    #region General Variables
    [Header("AI Configuration")]
    [SerializeField] NavMeshAgent agent; //ref al cerebro dek agente
    [SerializeField] Transform target; //ref al target a perseguir (variable)
    [SerializeField] LayerMask targetLayer; //Define layer de target (deteccion)
    [SerializeField] LayerMask groundLayer; //Define layer de suelo, evita ir a zonas sin suelo

    [Header("Patroling Stats")]
    [SerializeField] float walkPointRange = 10f; //radio maximo para determinar puntos a perseguir
    Vector3 walkPoint; //posicion del punto randoma  perseguir
    bool walkPointSet; //hay punto a persgeuri generado?,si es falso, se genera 1

    [Header("Attacking Stats")]
    [SerializeField] float timeBetweenAttacks = 1f; //cooldown entre ataques
    [SerializeField] GameObject projectile; //ref a la bala fisica que dispara al enemigo
    [SerializeField] Transform shootpoint; //posicion desde la que se disparo la bala
    [SerializeField] float shootSpeedY; //fuerza de disparo hacia arriba (catapulta)
    [SerializeField] float shootSpeedZ = 10f; //fuerza de disparo hacia delante (siempre se necesita)
    bool alreadyAttacked; // si es verdadero no stakqea ataques y entra espera entre ataques


    [Header("States & Detection")]
    [SerializeField] float signtRange = 8f; //radio del detector de persecucion
    [SerializeField] float attackRange = 2f; //radio del detector del ataque
    [SerializeField] bool targetInSightRange; //determina si e sverdadero que podemos perseguir al target
    [SerializeField] bool targetInAttacktRange; //determina si e sverdadero que podemos atacar al target


    [Header("Stuck Detection")]
    [SerializeField] float stuckCheckTime = 2f; //tiempo que el agente espera estando quiero antes de darse cuenta de que en stuck
    [SerializeField] float stuckThreshold = 0.1f; //margen de deteccion en stuck
    [SerializeField] float maxStuckDuration = 0.3f; //tiempo maxiimo de estar en stuck

    float stuckTimer; // reloj que cuenta el timepo de estar en stuck
    float lastCheckTime; // tiempo de chekeo en stuck
    Vector3 lastPosition; //Posicion del iltimo walkpoint perseguido
    #endregion


    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
