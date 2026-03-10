using UnityEngine;

public class EnemyHealth : MonoBehaviour
{


    [Header("Health System Configuration")]
    [SerializeField] int health; //vida actual enemigo
    [SerializeField] int maxHealth; //vida maxima enemigo


    [Header("Feedback Configuration")]
    [SerializeField] Material damageMat; //ref al materual que da feedback de dañado
    [SerializeField] MeshRenderer enemyRend; //ref al renderer al modelo del enemigo
    [SerializeField] GameObject deathVFX; //ref a las particulas de muerte
    Material baseMat; //ref al material base del modelo del enemigo

    private void Awake()
    {
        health = maxHealth; //cuando s egenera el enemigo, su vida actual se carga a la maxima
        baseMat = enemyRend.material; //se almacena el material base del modelo del enemigo
    }

    void Update()
    {
        if (health <= 0)
        {
            health = 0; //la vida no puede bajar de 0
            deathVFX.SetActive(true); //encedemos las particulas de muerte
            deathVFX.transform.position = transform.position; //podemos el vfx en la posicion actual
            gameObject.SetActive(false); // se paga el objeto = "muerto"
        }
    }

    public void TakeDamage(int damage)
    { 
        health -= damage; //quitar tanta vida como valor de daño viene de fuera
        enemyRend.material = damageMat; //se cambia temporalmente el materal base por el material dañado
        Invoke(nameof(ResetEnemyMat), 0.1f); //llmaar al reseteo de material
    }

    private void ResetEnemyMat()
    {
        enemyRend.material = baseMat; //cambiar el material del modelo al material base


    }


}



