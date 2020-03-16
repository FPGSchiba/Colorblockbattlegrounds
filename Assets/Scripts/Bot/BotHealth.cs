using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BotHealth : MonoBehaviour
{
    [Header("Variablen")]
    public float health;
    public float MaxHealth;

    [Header("Referenzen")]
    public GameObject Healthbar;
    public Slider slider;
    public GameObject Enemy;
    GameManager manager;
    SchussGeschwindigkeit schuss;
    [SerializeField]
    EnemyAI Ai;
    [SerializeField]
    EnemyLibrary Library;

    void Start()
    {
        Healthbar.SetActive(false);
        health = MaxHealth;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        slider.value = Library.CalculateHealthforBar(health, MaxHealth);
    }

    public void Respawn()
    {
        Healthbar.SetActive(false);
        health = MaxHealth;
        slider.value = Library.CalculateHealthforBar(health, MaxHealth);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "cube" && col.gameObject.layer == 9)
        {
            schuss = col.gameObject.GetComponent<SchussGeschwindigkeit>();
            Library.OnSchussEnter(schuss.SchussSpeed(), manager.isEnemy(schuss.shooter, Enemy), this.GetComponent<BotHealth>());
        }
    }

    void Update()
    {
        slider.value = Library.CalculateHealthforBar(health, MaxHealth);

        if(health < MaxHealth)
        {
            Healthbar.SetActive(true);
        }

        if(health <= 0)
        {
            Library.Dead(manager, Enemy);
        }

        if(health > MaxHealth)
        {
            health = MaxHealth;
        }
    }

    public void SetHealth(float damage)
    {
        health = health - damage;

        NavMeshAgent agent = Enemy.GetComponent<NavMeshAgent>();
        agent.SetDestination(schuss.shooter.transform.position);
    }
}
