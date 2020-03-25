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
    [SerializeField]
    public Image fill;

    void Start()
    {
        health = MaxHealth;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        slider.value = Library.CalculateHealthforBar(health, MaxHealth);
    }

    public void Respawn()
    {
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

        if(health <= 0)
        {
            Library.Dead(manager, Enemy, schuss.shooter);
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

    public void SetColor(string color)
    {
        if(color == "red")
        {
            fill.color = new Color(255, 0, 0);
            fill.GraphicUpdateComplete();
        }
        else
        {
            fill.color = new Color(0, 0, 255);
            fill.GraphicUpdateComplete();
        }
    }
}
