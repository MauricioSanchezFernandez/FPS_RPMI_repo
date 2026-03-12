using Unity.Mathematics;
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
    [SerializeField] GameObject proyectile; //ref a la bala fisica que dispara al enemigo
    [SerializeField] Transform shootpoint; //posicion desde la que se disparo la bala
    [SerializeField] float shootSpeedY; //fuerza de disparo hacia arriba (catapulta)
    [SerializeField] float shootSpeedZ = 10f; //fuerza de disparo hacia delante (siempre se necesita)
    bool alreadyAttacked; // si es verdadero no stakqea ataques y entra espera entre ataques


    [Header("States & Detection")]
    [SerializeField] float signhtRange = 8f; //radio del detector de persecucion
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


    private void Awake()
    {
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        lastPosition = transform.position;
        lastCheckTime = Time.time;
    }

    void Start()
    {
        
    }


    void Update()
    {
        EnemyStateUpdater();
    }

    void EnemyStateUpdater()
    {
        //Metodo que se encarga de gestionar el cambio de estados del enemigo


        //1 - cambio de estado de los bools
        Collider[] hits = Physics.OverlapSphere(transform.position, signhtRange, targetLayer);
        targetInSightRange = hits.Length > 0;
        //segundo si estan en vision detectamos si ademas estan en ataque
        if (targetInSightRange)
        { 
            float distance = Vector3.Distance(transform.position, target.position);
            targetInAttacktRange = distance <= attackRange;
        }
        else 
        {
            targetInAttacktRange = false;
        }



        //2 - cambio de estados segun booleanos
        if (!targetInSightRange && !targetInAttacktRange)
        {
            Patroling();
        }

        else if (targetInSightRange && !targetInAttacktRange)
        {

            ChaseTarget();
        }

        else if (targetInSightRange && targetInAttacktRange)
        {
            AttackTarget();
        }
    }

    void Patroling()
    {
        Debug.Log("Enemigo en estado patrulla");
    }

    void ChaseTarget()
    {
        //Accion que le dice al agente que persiga al target 
        agent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        //accion que contiene la logica de ataque
        //1- hacer que el agente se quede quieto (perseguirse a asi mismo)
        agent.SetDestination(transform.position);

        //2 aplicar una rotacion suavizada para que mire al target antes de atacar
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, agent.angularSpeed * Time.deltaTime);
        }

        //3 Se ataca solo si no se esta atacando
        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(proyectile, shootpoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * shootSpeedZ, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    { 
        alreadyAttacked = false;
    
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying) return; //Si estamos jugando en build no se ejecuta el resto del codigo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, signhtRange);

    }

}
